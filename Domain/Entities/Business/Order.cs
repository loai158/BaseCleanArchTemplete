using Domain.Enums.Business;

namespace Domain.Entities.Business
{
    public class Order : BaseEntities.BaseEntity
    {
        public int RetailerId { get; set; }
        public Retailer Retailer { get; set; } = null!;
        public OrderStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<OrderItem> Items { get; set; } = null!;
    }
}
