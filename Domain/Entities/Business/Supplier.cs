using Domain.Entities.User;

namespace Domain.Entities.Business
{
    public class Supplier : BaseEntities.BaseEntity
    {
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        public string StoreName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int AddressId { get; set; }
        public Address Address { get; set; } = null!;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
