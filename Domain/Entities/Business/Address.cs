namespace Domain.Entities.Business
{
    public class Address : BaseEntities.BaseEntity
    {
        public string City { get; set; } = null!;
        public string Area { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string? Notes { get; set; }
    }
}
