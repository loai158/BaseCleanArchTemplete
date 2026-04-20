using Domain.Exceptions;
using Domain.Features.Categories.Responses;
using MediatR;

namespace Domain.Features.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<SimpleResult<CreateCategoryResponse>>
    {
        public string Name { get; set; } = null!;
        public int? ParentId { get; set; }
    }
}
