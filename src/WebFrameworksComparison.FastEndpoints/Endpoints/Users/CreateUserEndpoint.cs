namespace WebFrameworksComparison.FastEndpoints.Endpoints.Users;

public class CreateUserEndpoint : Endpoint<CreateUserDto, UserDto>
{
    private readonly Core.Application.Interfaces.IUserService _userService;

    public CreateUserEndpoint(Core.Application.Interfaces.IUserService userService)
    {
        _userService = userService;
    }

    public override void Configure()
    {
        Post("/api/users");
        Roles("Admin", "SuperAdmin");
        Summary(s =>
        {
            s.Summary = "Create a new user";
            s.Description = "Creates a new user in the system";
            s.Response<UserDto>(201, "User created successfully");
            s.Response(400, "Validation error");
            s.Response(401, "Unauthorized");
            s.Response(403, "Forbidden");
        });
    }

    public override async Task HandleAsync(CreateUserDto req, CancellationToken ct)
    {
        // Get current user ID from JWT token
        var currentUserId = User.FindFirst("user_id")?.Value ?? "System";
        
        var user = await _userService.CreateAsync(req, currentUserId);
        await SendOkAsync(user, ct);
    }
}