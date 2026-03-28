namespace SASS.Chassis.Security.UserRetrieval;

public interface IUserProvider
{
    /// <summary>
    /// Get UserId from Jwt token.
    /// </summary>
    Guid UserId { get; }
}