namespace WebFrameworksComparison.FastEndpoints.Endpoints.Users;

public class UpdateUserEndpoint (IUserService userService): Endpoint<UpdateUserRequest, UserDto>
{
    private readonly IUserService _userService= userService;

    public override void Configure()
    {
        Put("/api/users/{id}");
        Roles("Admin", "SuperAdmin");
        Summary(s =>
        {
            s.Summary = "Update an existing user";
            s.Description = "Updates an existing user in the system";
            s.Response<UserDto>(200, "User updated successfully");
            s.Response(404, "User not found");
            s.Response(400, "Validation error");
            s.Response(401, "Unauthorized");
            s.Response(403, "Forbidden");
        });
    }

    public override async Task HandleAsync(UpdateUserRequest req, CancellationToken ct)
    {
        // Get current user ID from JWT token
        var currentUserId = User.FindFirst("user_id")?.Value ?? "System";
        
        var updateDto = new UpdateUserDto
        {
            FirstName = req.FirstName,
            LastName = req.LastName,
            PhoneNumber = req.PhoneNumber,
            DateOfBirth = req.DateOfBirth,
            Role = req.Role,
            Address = req.Address
        };

        var user = await _userService.UpdateAsync(req.Id, updateDto, currentUserId);
        
        if (user == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(user, ct);
    }
}


