using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.User
{
    public class ApplicationUser : IdentityUser
    {
        public UserDetails? UserDetails { get; set; }
    }
}
