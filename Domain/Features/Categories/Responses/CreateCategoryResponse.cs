namespace Domain.Features.Categories.Responses
{
    public class CreateCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? ParentId { get; set; }
    }
}
