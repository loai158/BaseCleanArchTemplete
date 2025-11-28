namespace Domain.DTOs.User
{
    public class AuthResponse
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsAuthenticated { get; set; }
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public List<string>? Roles { get; set; }
        public DateTime ExpiresOn { get; set; }
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
