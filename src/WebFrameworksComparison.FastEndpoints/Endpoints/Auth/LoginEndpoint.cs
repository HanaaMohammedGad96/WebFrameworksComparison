namespace WebFrameworksComparison.FastEndpoints.Endpoints.Auth;

public class LoginEndpoint(IAuthService authService) : Endpoint<LoginRequestDto, LoginResponseDto>
{
    private readonly IAuthService _authService = authService;

    public override void Configure()
    {
        Post("/api/auth/login");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "User login";
            s.Description = "Authenticate user and return JWT token";
            s.Response<LoginResponseDto>(200, "Login successful");
            s.Response(401, "Invalid credentials");
            s.Response(400, "Validation error");
        });
    }

    public override async Task HandleAsync(LoginRequestDto req, CancellationToken ct)
    {
        var result = await _authService.LoginAsync(req);
        
        if (result == null)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        await SendOkAsync(result, ct);
    }
}