namespace Domain.Features.Categories.Responses
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? ParentId { get; set; }
        public string? ParentName { get; set; }
        public List<CategoryResponse> Children { get; set; } = new();
    }
}
