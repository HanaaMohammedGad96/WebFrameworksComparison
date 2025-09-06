namespace WebFrameworksComparison.FastEndpoints.Endpoints.Users;

public class GetUsersEndpoint : Endpoint<GetUsersRequest, GetUsersResponse>
{
    private readonly Core.Application.Interfaces.IUserService _userService;

    public GetUsersEndpoint(Core.Application.Interfaces.IUserService userService)
    {
        _userService = userService;
    }

    public override void Configure()
    {
        Get("/api/users");
        Roles("Admin", "SuperAdmin");
        Summary(s =>
        {
            s.Summary = "Get all users";
            s.Description = "Retrieves a paginated list of all users";
            s.Response<GetUsersResponse>(200, "Successfully retrieved users");
            s.Response(401, "Unauthorized");
            s.Response(403, "Forbidden");
        });
    }

    public override async Task HandleAsync(GetUsersRequest req, CancellationToken ct)
    {
        var users = await _userService.GetAllAsync(req.PageNumber, req.PageSize);
        var totalCount = await _userService.CountAsync();

        var response = new GetUsersResponse
        {
            Users = users,
            TotalCount = totalCount,
            PageNumber = req.PageNumber,
            PageSize = req.PageSize,
            TotalPages = (int)Math.Ceiling((double)totalCount / req.PageSize)
        };

        await SendOkAsync(response, ct);
    }
}

public class GetUsersRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetUsersResponse
{
    public IEnumerable<UserListDto> Users { get; set; } = new List<UserListDto>();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}