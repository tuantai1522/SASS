namespace SASS.Chassis.Security.TokenGeneration;

public interface ITokenProvider
{
    string CreateAccessToken(Guid userId, string email);
    (string token, long expiredAt) CreateRefreshToken();
}