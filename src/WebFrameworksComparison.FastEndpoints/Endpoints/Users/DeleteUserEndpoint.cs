namespace WebFrameworksComparison.FastEndpoints.Endpoints.Users;

public class DeleteUserEndpoint : Endpoint<DeleteUserRequest>
{
    private readonly Core.Application.Interfaces.IUserService _userService;

    public DeleteUserEndpoint(Core.Application.Interfaces.IUserService userService)
    {
        _userService = userService;
    }

    public override void Configure()
    {
        Delete("/api/users/{id}");
        Roles("Admin", "SuperAdmin");
        Summary(s =>
        {
            s.Summary = "Delete a user";
            s.Description = "Deletes a user from the system";
            s.Response(204, "User deleted successfully");
            s.Response(404, "User not found");
            s.Response(401, "Unauthorized");
            s.Response(403, "Forbidden");
        });
    }

    public override async Task HandleAsync(DeleteUserRequest req, CancellationToken ct)
    {
        // Get current user ID from JWT token
        var currentUserId = User.FindFirst("user_id")?.Value ?? "System";
        
        var deleted = await _userService.DeleteAsync(req.Id, currentUserId);
        
        if (!deleted)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendNoContentAsync(ct);
    }
}

public class DeleteUserRequest
{
    public int Id { get; set; }
}
