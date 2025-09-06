namespace WebFrameworksComparison.Core.Application.Interfaces;

public interface IUserService
{
    Task<UserDto?> GetByIdAsync(int id);
    Task<IEnumerable<UserListDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
    Task<UserDto> CreateAsync(CreateUserDto createUserDto, string createdBy);
    Task<UserDto?> UpdateAsync(int id, UpdateUserDto updateUserDto, string updatedBy);
    Task<bool> DeleteAsync(int id, string deletedBy);
    Task<bool> ExistsAsync(int id);
    Task<int> CountAsync();
    Task<IEnumerable<UserListDto>> SearchAsync(string searchTerm, int pageNumber = 1, int pageSize = 10);
}
