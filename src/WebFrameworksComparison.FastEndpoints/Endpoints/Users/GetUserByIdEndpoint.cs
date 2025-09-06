namespace WebFrameworksComparison.FastEndpoints.Endpoints.Users;

public class GetUserByIdEndpoint : Endpoint<GetUserByIdRequest, UserDto>
{
    private readonly Core.Application.Interfaces.IUserService _userService;

    public GetUserByIdEndpoint(Core.Application.Interfaces.IUserService userService)
    {
        _userService = userService;
    }

    public override void Configure()
    {
        Get("/api/users/{id}");
        Roles("Admin", "SuperAdmin", "User");
        Summary(s =>
        {
            s.Summary = "Get user by ID";
            s.Description = "Retrieves a specific user by their ID";
            s.Response<UserDto>(200, "Successfully retrieved user");
            s.Response(404, "User not found");
            s.Response(401, "Unauthorized");
            s.Response(403, "Forbidden");
        });
    }

    public override async Task HandleAsync(GetUserByIdRequest req, CancellationToken ct)
    {
        // Check if user can access this resource
        var currentUserId = User.FindFirst("user_id")?.Value;
        var currentUserRole = User.FindFirst("role")?.Value;
        
        // Users can only access their own data unless they are admin
        if (currentUserRole != "Admin" && currentUserRole != "SuperAdmin" && 
            currentUserId != req.Id.ToString())
        {
            await SendForbiddenAsync(ct);
            return;
        }

        var user = await _userService.GetByIdAsync(req.Id);
        
        if (user == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(user, ct);
    }
}

public class GetUserByIdRequest
{
    public int Id { get; set; }
}