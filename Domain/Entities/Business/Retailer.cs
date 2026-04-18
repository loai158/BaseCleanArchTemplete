using Domain.Entities.User;

namespace Domain.Entities.Business
{
    public class Retailer : BaseEntities.BaseEntity
    {
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        public string ShopName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int AddressId { get; set; }
        public Address Address { get; set; } = null!;
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
