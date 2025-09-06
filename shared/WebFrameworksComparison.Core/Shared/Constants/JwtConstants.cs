namespace WebFrameworksComparison.Core.Shared.Constants;

public static class JwtConstants
{
    public const string SecretKey = "YourSuperSecretKeyThatIsAtLeast32CharactersLong!";
    public const string Issuer = "WebFrameworksComparison";
    public const string Audience = "WebFrameworksComparison.Users";
    public const int ExpirationMinutes = 60;
    public const int RefreshTokenExpirationDays = 7;
}

public static class ClaimTypes
{
    public const string UserId = "user_id";
    public const string Email = "email";
    public const string Role = "role";
    public const string FirstName = "first_name";
    public const string LastName = "last_name";
}
