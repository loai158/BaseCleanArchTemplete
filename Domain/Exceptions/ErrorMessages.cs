namespace Domain.Exceptions
{
    public static class ErrorMessages
    {
        public static string GetMessage(ErrorCode errorCode)
        {
            return _messages.GetValueOrDefault(errorCode, "Unknown error occurred");
        }
        private static readonly Dictionary<ErrorCode, string> _messages = new()
        {
            // Authentication Errors
            { ErrorCode.InvalidCredentials, "Invalid email or password" },
            { ErrorCode.UserNotFound, "User not found" },
            { ErrorCode.AccountDeactivated, "Account is deactivated" },
            { ErrorCode.EmailAlreadyExists, "Email already exists" },
            { ErrorCode.PasswordMismatch, "Passwords do not match" },
            { ErrorCode.TokenExpired, "Token has expired" },
            { ErrorCode.InvalidToken, "Invalid token" },
            { ErrorCode.AccessDenied, "Access denied" },
            
            // Validation Errors
            { ErrorCode.InvalidEmail, "Invalid email format" },
            { ErrorCode.InvalidPassword, "Invalid password format" },
            { ErrorCode.RequiredFieldMissing, "Required field is missing" },
            { ErrorCode.InvalidFormat, "Invalid format" },
            { ErrorCode.AlreadyExist, "Record already eixst with the same Id " },
            
            // Business Logic Errors
            { ErrorCode.InsufficientPermissions, "Insufficient permissions" },
            { ErrorCode.ResourceNotFound, "Resource not found" },
            { ErrorCode.OperationNotAllowed, "Operation not allowed" },
            { ErrorCode.BusinessRuleViolation, "Business rule violation" },
            { ErrorCode.Disabled, "Account Disalbed Connect Admin" },

            // System Errors
            { ErrorCode.DatabaseError, "Database error occurred" },
            { ErrorCode.ExternalServiceError, "External service error" },
            { ErrorCode.ConfigurationError, "Configuration error" },
            { ErrorCode.InternalServerError, "Internal server error" }
        };

    }
}
