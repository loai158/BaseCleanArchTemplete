
namespace Domain.Entities.User
{
    public class UserDetails : BaseEntity.BaseEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string UserName { get; private set; }
        public string UserId { get; private set; }
        public ApplicationUser? User { get; private set; }
        public UserDetails() { }
        public void CreateUserDetails(string firstName, string lastName, string userName, string userId)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
        }
    }
}
