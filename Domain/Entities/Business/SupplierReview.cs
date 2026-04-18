using Domain.Enums.Business;

namespace Domain.Entities.Business
{
    public class SupplierReview : BaseEntities.BaseEntity
    {
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;
        public int RetailerId { get; set; }
        public Retailer Retailer { get; set; } = null!;
        public Rating Rating { get; set; } // 1-5
        public string? Comment { get; set; }
    }
}
