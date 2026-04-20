using Domain.Exceptions;
using MediatR;

namespace Domain.Features.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest<SimpleResult<bool>>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? ParentId { get; set; }
    }
}
