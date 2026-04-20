using Domain.Entities.Business;
using Domain.Exceptions;
using Domain.Features.Categories.Queries;
using Domain.Features.Categories.Responses;
using Domain.Interfaces;
using Domain.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Service.Handlers.Categories
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, SimpleResult<PagedResult<CategoryResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCategoriesHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<SimpleResult<PagedResult<CategoryResponse>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.Repository<Category>()
                .Get(
                    predicate: c => c.ParentId == null &&
                        (string.IsNullOrEmpty(request.Query) ||
                         c.Name.Contains(request.Query)),
                    include: q => q
                        .Include(c => c.Children)
                        .ThenInclude(c => c.Children),
                    tracked: false
                );

            // Total قبل الـ Pagination
            var totalCount = await query.CountAsync(cancellationToken);

            // Pagination
            var categories = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var response = categories.Select(
                 c => new CategoryResponse
                 {
                     Id = c.Id,
                     Name = c.Name,
                     ParentId = c.ParentId,
                     Children = c.Children.Select(
                         sc => new CategoryResponse
                         {
                             Id = sc.Id,
                             Name = sc.Name,
                             ParentId = sc.ParentId,
                             Children = sc.Children.Select(
                                 ssc => new CategoryResponse
                                 {
                                     Id = ssc.Id,
                                     Name = ssc.Name,
                                     ParentId = ssc.ParentId
                                 }
                             ).ToList()
                         }
                     ).ToList()
                 });

            return SimpleResult<PagedResult<CategoryResponse>>.Success(new PagedResult<CategoryResponse>
            {
                Data = response,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount
            });
        }

    }
}

