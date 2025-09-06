namespace WebFrameworksComparison.Core.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        return user == null ? null : _mapper.Map<UserDto>(user);
    }

    public async Task<IEnumerable<UserListDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
    {
        var users = await _unitOfWork.Users.GetPagedAsync(pageNumber, pageSize);
        return _mapper.Map<IEnumerable<UserListDto>>(users);
    }

    public async Task<UserDto> CreateAsync(CreateUserDto createUserDto, string createdBy)
    {
        // Check if user already exists
        var existingUser = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Email == createUserDto.Email);
        if (existingUser != null)
        {
            throw new BusinessException("User with this email already exists", "USER_EXISTS");
        }

        var user = _mapper.Map<User>(createUserDto);
        user.CreatedBy = createdBy;
        user.CreatedAt = DateTime.UtcNow;

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> UpdateAsync(int id, UpdateUserDto updateUserDto, string updatedBy)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        if (user == null)
        {
            throw new NotFoundException("User", id);
        }

        _mapper.Map(updateUserDto, user);
        user.UpdatedBy = updatedBy;
        user.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Users.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<UserDto>(user);
    }

    public async Task<bool> DeleteAsync(int id, string deletedBy)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        if (user == null)
        {
            throw new NotFoundException("User", id);
        }

        user.UpdatedBy = deletedBy;
        await _unitOfWork.Users.DeleteAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _unitOfWork.Users.ExistsAsync(u => u.Id == id);
    }

    public async Task<int> CountAsync()
    {
        return await _unitOfWork.Users.CountAsync();
    }

    public async Task<IEnumerable<UserListDto>> SearchAsync(string searchTerm, int pageNumber = 1, int pageSize = 10)
    {
        var users = await _unitOfWork.Users.GetPagedAsync(pageNumber, pageSize, 
            u => u.FirstName.Contains(searchTerm) || 
                 u.LastName.Contains(searchTerm) || 
                 u.Email.Contains(searchTerm));
        
        return _mapper.Map<IEnumerable<UserListDto>>(users);
    }
}
