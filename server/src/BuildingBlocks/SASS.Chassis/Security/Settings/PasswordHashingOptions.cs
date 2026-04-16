namespace SASS.Chassis.Security.Settings;

public sealed class PasswordHashingOptions
{
    public string CurrentAlgorithm { get; set; } = "pbkdf2-sha512";
}
