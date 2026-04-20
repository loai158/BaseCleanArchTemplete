using Domain.Exceptions;
using Domain.Features.Categories.Responses;
using Domain.Pagination;
using MediatR;

namespace Domain.Features.Categories.Queries
{
    public class GetCategoriesQuery : PaginationQuery, IRequest<SimpleResult<PagedResult<CategoryResponse>>>
    {
    }
}
