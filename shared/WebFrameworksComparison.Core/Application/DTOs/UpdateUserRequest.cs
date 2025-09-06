namespace WebFrameworksComparison.Core.Application.DTOs;
public class UpdateUserRequest
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public UserRole Role { get; set; }
    public string? Address { get; set; }
}
