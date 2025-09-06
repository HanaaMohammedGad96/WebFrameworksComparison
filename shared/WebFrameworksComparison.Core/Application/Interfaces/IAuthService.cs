namespace WebFrameworksComparison.Core.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginRequest);
    Task<LoginResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequest);
    Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto);
    Task<bool> LogoutAsync(string token);
    Task<bool> ValidateTokenAsync(string token);
    Task<string?> GetUserIdFromTokenAsync(string token);
}
