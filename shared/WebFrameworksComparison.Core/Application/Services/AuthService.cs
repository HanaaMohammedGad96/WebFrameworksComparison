using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
namespace WebFrameworksComparison.Core.Application.Services;

public class AuthService(IUnitOfWork unitOfWork, IMapper mapper, IAuditService auditService, IHttpContextAccessor httpContextAccessor) : IAuthService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IAuditService _auditService = auditService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginRequest)
    {
        var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Email == loginRequest.Email);
        if (user == null || !VerifyPassword(loginRequest.Password, user.PasswordHash ?? string.Empty))
        {
            await _auditService.LogAsync("User", "0", AuditAction.Login, null, null, null, "Failed login attempt", null, null);
            return null;
        }
        if (user.Status != UserStatus.Active)
        {
            throw new UnauthorizedException("Account is not active");
        }
        // Generate JWT token
        var claims = new List<Claim>
        {
            new(Shared.Constants.ClaimTypes.UserId, user.Id.ToString()),
            new(Shared.Constants.ClaimTypes.Email, user.Email),
            new(Shared.Constants.ClaimTypes.Role, user.Role.ToString()),
            new(Shared.Constants.ClaimTypes.FirstName, user.FirstName),
            new(Shared.Constants.ClaimTypes.LastName, user.LastName),
            new(System.Security.Claims.ClaimTypes.Name, user.FullName)
        };
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Shared.Constants.JwtConstants.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: Shared.Constants.JwtConstants.Issuer,
            audience: Shared.Constants.JwtConstants.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Shared.Constants.JwtConstants.ExpirationMinutes),
            signingCredentials: creds
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        await _auditService.LogAsync("User", user.Id.ToString(), AuditAction.Login, user.Id.ToString(), null, null, "Successful login", null, null);
        return new LoginResponseDto
        {
            Token = tokenString,
            ExpiresAt = token.ValidTo,
            User = _mapper.Map<UserDto>(user)
        };
    }

    public Task<LoginResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequest)
    {
        // Refresh token logic removed. Use built-in JWT features or implement as needed.
        return Task.FromResult<LoginResponseDto?>(null);
    }

    public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        if (user == null)
        {
            throw new NotFoundException("User", userId);
        }
        if (!VerifyPassword(changePasswordDto.CurrentPassword, user.PasswordHash ?? string.Empty))
        {
            throw new WebFrameworksComparison.Core.Shared.Exceptions.ValidationException("Current password is incorrect");
        }
        if (changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword)
        {
            throw new WebFrameworksComparison.Core.Shared.Exceptions.ValidationException("New password and confirm password do not match");
        }
        user.PasswordHash = HashPassword(changePasswordDto.NewPassword);
        user.UpdatedAt = DateTime.UtcNow;
        await _unitOfWork.Users.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();
        await _auditService.LogAsync("User", userId.ToString(), AuditAction.Update, userId.ToString(), null, null, "Password changed", null, null);
        return true;
    }

    public Task<bool> LogoutAsync(string token)
    {
        // Logout logic handled by ASP.NET Core pipeline. Implement as needed.
        return Task.FromResult(true);
    }

    public Task<bool> ValidateTokenAsync(string token)
    {
        // Use ASP.NET Core built-in JWT validation.
        return Task.FromResult(true);
    }

    public Task<string?> GetUserIdFromTokenAsync(string token)
    {
    var user = _httpContextAccessor.HttpContext?.User;
    var userId = user?.FindFirst(WebFrameworksComparison.Core.Shared.Constants.ClaimTypes.UserId)?.Value;
    return Task.FromResult(userId);
    }

    private string HashPassword(string password)
    {
        byte[] salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        return Convert.ToBase64String(salt) + ":" + hashed;
    }

    private bool VerifyPassword(string password, string storedHash)
    {
        try
        {
            var parts = storedHash.Split(':');
            if (parts.Length != 2) return false;

            var salt = Convert.FromBase64String(parts[0]);
            var hash = parts[1];

            var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashedPassword == hash;
        }
        catch
        {
            return false;
        }
    }
}
