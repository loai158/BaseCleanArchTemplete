namespace Domain.Entities.Business
{
    public class OrderItem : BaseEntities.BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

}
