namespace SASS.Chassis.Security.TokenGeneration;

public interface ITokenProvider
{
    string Create(Guid userId, string email);
}