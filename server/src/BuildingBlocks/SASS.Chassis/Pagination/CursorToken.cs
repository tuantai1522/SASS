using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;

namespace SASS.Chassis.Pagination;

public sealed record CursorToken(long CreatedAt, Guid Id)
{
    public static string Encode(CursorToken token)
    {
        var json = JsonSerializer.Serialize(token);
        return Base64UrlEncoder.Encode(Encoding.UTF8.GetBytes(json));
    }

    public static CursorToken? Decode(string? cursor)
    {
        if (string.IsNullOrWhiteSpace(cursor))
        {
            return null;
        }

        try
        {
            string json = Base64UrlEncoder.Decode(cursor);
            return JsonSerializer.Deserialize<CursorToken>(json);
        }
        catch
        {
            return null;
        }
    }
}
