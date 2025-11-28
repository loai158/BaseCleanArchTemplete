namespace Domain.Exceptions
{
    public class SimpleResult<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public ErrorCode? ErrorCode { get; set; }
        public T? Data { get; set; }

        public static SimpleResult<T> Success(T data, string message = "Success") => new SimpleResult<T> { IsSuccess = true, Message = message, Data = data };
        public static SimpleResult<T> Failure(ErrorCode errorCode) => new SimpleResult<T> { IsSuccess = false, ErrorCode = errorCode, Message = ErrorMessages.GetMessage(errorCode) };
        public static SimpleResult<T> Failure(ErrorCode errorCode, string customMessage) => new SimpleResult<T> { IsSuccess = false, ErrorCode = errorCode, Message = customMessage };
    }
    public class SimpleResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public ErrorCode? ErrorCode { get; set; }

        public static SimpleResult Success(string message = "Success") => new SimpleResult { IsSuccess = true, Message = message };
        public static SimpleResult Failure(ErrorCode errorCode) => new SimpleResult { IsSuccess = false, ErrorCode = errorCode, Message = ErrorMessages.GetMessage(errorCode) };
        public static SimpleResult Failure(ErrorCode errorCode, string customMessage) => new SimpleResult { IsSuccess = false, ErrorCode = errorCode, Message = customMessage };
    }
}
