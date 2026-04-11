using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SASS.Chassis.Security.TokenGeneration;
using SASS.Chat.Configurations;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Features.Auth.Google.AuthorizeGoogle;

internal sealed class AuthorizeGoogleCommandHandler(
    IHttpClientFactory httpClientFactory,
    IOptions<GoogleAuthOptions> options,
    ITokenProvider tokenProvider,
    ChatDbContext dbContext) : IRequestHandler<AuthorizeGoogleCommand, AuthorizeGoogleResponse>
{
    public async Task<AuthorizeGoogleResponse> Handle(AuthorizeGoogleCommand request, CancellationToken cancellationToken)
    {
        var settings = options.Value;
        var httpClient = httpClientFactory.CreateClient("GoogleAuth");

        var tokenResponse = await ExchangeCodeForTokenAsync(httpClient, settings, request.Code, cancellationToken);
        if (tokenResponse is null)
        {
            throw new InvalidOperationException("Unable to authorize with Google.");
        }

        var googleUser = await GetGoogleUserInfoAsync(httpClient, settings, tokenResponse.AccessToken, tokenResponse.IdToken, cancellationToken);
        if (googleUser is null || string.IsNullOrWhiteSpace(googleUser.Email))
        {
            throw new InvalidOperationException("Unable to extract email from Google profile.");
        }

        var normalizedEmail = googleUser.Email.Trim().ToLowerInvariant();
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == normalizedEmail, cancellationToken);

        if (user is null)
        {
            user = User.Create(normalizedEmail, null);
            await dbContext.Users.AddAsync(user, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        return new AuthorizeGoogleResponse(tokenProvider.Create(user.Id, user.Email));

    }

    private static async Task<GoogleTokenResponse?> ExchangeCodeForTokenAsync(
        HttpClient httpClient,
        GoogleAuthOptions settings,
        string code,
        CancellationToken cancellationToken
    )
    {
        var content = new FormUrlEncodedContent(
            new Dictionary<string, string>
            {
                ["code"] = code,
                ["client_id"] = settings.ClientId,
                ["client_secret"] = settings.ClientSecret,
                ["redirect_uri"] = settings.RedirectUri,
                ["grant_type"] = "authorization_code"
            }
        );

        using var tokenHttpResponse = await httpClient.PostAsync(settings.GoogleAuthTokenEndpoint, content, cancellationToken);
        if (!tokenHttpResponse.IsSuccessStatusCode)
        {
            return null;
        }

        return await tokenHttpResponse.Content.ReadFromJsonAsync<GoogleTokenResponse>(cancellationToken);
    }

    private static async Task<GoogleUserInfoResponse?> GetGoogleUserInfoAsync(
        HttpClient httpClient,
        GoogleAuthOptions settings,
        string accessToken,
        string idToken,
        CancellationToken cancellationToken
    )
    {
        var request = new HttpRequestMessage(HttpMethod.Get, settings.GoogleContactInfoEndpoint + accessToken);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", idToken);

        using var userInfoHttpResponse = await httpClient.SendAsync(request, cancellationToken);
        if (!userInfoHttpResponse.IsSuccessStatusCode)
        {
            return null;
        }

        return await userInfoHttpResponse.Content.ReadFromJsonAsync<GoogleUserInfoResponse>(cancellationToken);
    }

    private sealed class GoogleTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = null!;

        [JsonPropertyName("id_token")]
        public string IdToken { get; set; } = null!;
    }

    private sealed class GoogleUserInfoResponse
    {
        [JsonPropertyName("email")]
        public string Email { get; init; } = string.Empty;
    }
}
