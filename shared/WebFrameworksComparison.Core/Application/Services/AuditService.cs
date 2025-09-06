namespace WebFrameworksComparison.Core.Application.Services;

public class AuditService : IAuditService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AuditService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task LogAsync(string entityName, string entityId, AuditAction action, string? userId = null,
        string? oldValues = null, string? newValues = null, string? description = null,
        string? ipAddress = null, string? userAgent = null)
    {
        var auditLog = new AuditLog
        {
            EntityName = entityName,
            EntityId = entityId,
            Action = action,
            UserId = int.TryParse(userId, out var id) ? id : null,
            OldValues = oldValues,
            NewValues = newValues,
            Description = description,
            IpAddress = ipAddress,
            UserAgent = userAgent,
            CreatedBy = userId ?? "System",
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.AuditLogs.AddAsync(auditLog);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<AuditLogDto>> GetAuditLogsAsync(
     int pageNumber = 1,
     int pageSize = 10,
     string? entityName = null,
     string? userId = null,
     AuditAction? action = null)
    {
        if (pageNumber <= 0) pageNumber = 1;
        if (pageSize <= 0) pageSize = 10;

        int? id = null;
        if (!string.IsNullOrWhiteSpace(userId) && int.TryParse(userId, out var parsedId))
        {
            id = parsedId;
        }

        var auditLogs = await _unitOfWork.AuditLogs.GetPagedAsync(
            pageNumber,
            pageSize,
            log =>
                (string.IsNullOrEmpty(entityName) || log.EntityName == entityName) &&
                (!id.HasValue || log.UserId == id.Value) &&
                (!action.HasValue || log.Action == action.Value)
        );

        return _mapper.Map<IEnumerable<AuditLogDto>>(auditLogs);
    }

}
