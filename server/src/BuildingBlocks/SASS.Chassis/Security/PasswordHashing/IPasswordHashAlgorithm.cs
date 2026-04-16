namespace SASS.Chassis.Security.PasswordHashing;

public interface IPasswordHashAlgorithm
{
    string Hash(string password);
    bool Verify(string password, string hash);
}
