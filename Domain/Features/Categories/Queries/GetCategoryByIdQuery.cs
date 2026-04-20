using Domain.Exceptions;
using Domain.Features.Categories.Responses;
using MediatR;

namespace Domain.Features.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<SimpleResult<CategoryResponse>>
    {
        public int Id { get; set; }
    }
}
