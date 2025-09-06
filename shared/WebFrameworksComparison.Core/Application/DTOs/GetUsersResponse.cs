namespace WebFrameworksComparison.Core.Application.DTOs;

public class GetUsersResponse
{
    public IEnumerable<UserListDto> Users { get; set; } = new List<UserListDto>();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}
