namespace WebFrameworksComparison.Core.Shared.Exceptions;

public class BusinessException : Exception
{
    public string ErrorCode { get; }
    public int StatusCode { get; }

    public BusinessException(string message, string errorCode = "BUSINESS_ERROR", int statusCode = 400) 
        : base(message)
    {
        ErrorCode = errorCode;
        StatusCode = statusCode;
    }

    public BusinessException(string message, Exception innerException, string errorCode = "BUSINESS_ERROR", int statusCode = 400) 
        : base(message, innerException)
    {
        ErrorCode = errorCode;
        StatusCode = statusCode;
    }
}

public class NotFoundException : BusinessException
{
    public NotFoundException(string entityName, object id) 
        : base($"{entityName} with ID {id} was not found.", "NOT_FOUND", 404)
    {
    }
}

public class ValidationException : BusinessException
{
    public ValidationException(string message) 
        : base(message, "VALIDATION_ERROR", 400)
    {
    }
}

public class UnauthorizedException : BusinessException
{
    public UnauthorizedException(string message = "Unauthorized access") 
        : base(message, "UNAUTHORIZED", 401)
    {
    }
}

public class ForbiddenException : BusinessException
{
    public ForbiddenException(string message = "Access forbidden") 
        : base(message, "FORBIDDEN", 403)
    {
    }
}
