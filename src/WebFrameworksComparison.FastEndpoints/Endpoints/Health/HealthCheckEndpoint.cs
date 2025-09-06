namespace WebFrameworksComparison.FastEndpoints.Endpoints.Health;

public class HealthCheckEndpoint : EndpointWithoutRequest<HealthCheckResponse>
{
    private readonly Core.Domain.Interfaces.IUnitOfWork _unitOfWork;

    public HealthCheckEndpoint(Core.Domain.Interfaces.IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public override void Configure()
    {
        Get("/api/health");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Health check";
            s.Description = "Returns the health status of the API and database";
            s.Response<HealthCheckResponse>(200, "API is healthy");
            s.Response(503, "API is unhealthy");
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var isHealthy = true;
        var databaseStatus = "Healthy";
        var errors = new List<string>();

        try
        {
            // Test database connection
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            isHealthy = false;
            databaseStatus = "Unhealthy";
            errors.Add($"Database error: {ex.Message}");
        }

        var response = new HealthCheckResponse
        {
            Status = isHealthy ? "Healthy" : "Unhealthy",
            Timestamp = DateTime.UtcNow,
            Framework = "FastEndpoints",
            Version = "5.23.0",
            Database = databaseStatus,
            Errors = errors
        };

        if (isHealthy)
        {
            await SendOkAsync(response, ct);
        }
        else
        {
            await SendAsync(response, 503, ct);
        }
    }
}

public class HealthCheckResponse
{
    public string Status { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string Framework { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string Database { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = new();
}