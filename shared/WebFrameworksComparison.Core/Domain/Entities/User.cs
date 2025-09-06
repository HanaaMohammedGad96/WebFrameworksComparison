namespace WebFrameworksComparison.Core.Domain.Entities;

public class User : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(255)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [MaxLength(20)]
    public string? PhoneNumber { get; set; }
    
    public DateTime? DateOfBirth { get; set; }
    
    public UserStatus Status { get; set; } = UserStatus.Active;
    
    public UserRole Role { get; set; } = UserRole.User;
    
    [MaxLength(500)]
    public string? Address { get; set; }
    
    public string? PasswordHash { get; set; }
    
    public string FullName => $"{FirstName} {LastName}";

    // Auth tokens
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
    
    // Navigation properties
    public virtual ICollection<UserProfile> Profiles { get; set; } = new List<UserProfile>();
    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
}