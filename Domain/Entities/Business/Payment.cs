using Domain.Enums.Business;

namespace Domain.Entities.Business
{
    public class Payment : BaseEntities.BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
