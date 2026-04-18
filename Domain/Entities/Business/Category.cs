namespace Domain.Entities.Business
{
    public class Category : BaseEntities.BaseEntity
    {
        public string Name { get; set; }

        public int? ParentId { get; set; }
        public Category? Parent { get; set; }

        public ICollection<Category> Children { get; set; } = new List<Category>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
