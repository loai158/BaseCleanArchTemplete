namespace Domain.Exceptions
{
    public enum ErrorCode
    {
        // Authentication Errors
        InvalidCredentials = 1000,
        UserNotFound = 1001,
        AccountDeactivated = 1002,
        EmailAlreadyExists = 1003,
        PasswordMismatch = 1004,
        TokenExpired = 1005,
        InvalidToken = 1006,
        AccessDenied = 1007,
        UnAuthorized = 1008,

        // Validation Errors
        InvalidEmail = 2000,
        InvalidPassword = 2001,
        RequiredFieldMissing = 2002,
        InvalidFormat = 2003,
        AlreadyExist = 2004,
        ManagerSettings = 2005,
        Permission = 2006,

        // Business Logic Errors
        InsufficientPermissions = 3000,
        ResourceNotFound = 3001,
        OperationNotAllowed = 3002,
        BusinessRuleViolation = 3003,
        ExceedsLimit = 3004,
        Disabled = 3005,
        HasPendingDelete = 3017,
        NotIntialized = 3018,
        ReportNotIntialized = 3019,
        ContractsNotIntialized = 3020,

        // System Errors
        DatabaseError = 5000,
        ExternalServiceError = 5001,
        ConfigurationError = 5002,
        InternalServerError = 5003
    }
}
