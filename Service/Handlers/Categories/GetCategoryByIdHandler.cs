using Domain.Entities.Business;
using Domain.Exceptions;
using Domain.Features.Categories.Queries;
using Domain.Features.Categories.Responses;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Service.Handlers.Categories
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, SimpleResult<CategoryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetCategoryByIdHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<SimpleResult<CategoryResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Repository<Category>()
        .Get(
            predicate: c => c.Id == request.Id,
            include: q => q
                .Include(c => c.Parent)
                .Include(c => c.Children),
            tracked: false
        )
        .FirstOrDefaultAsync(cancellationToken);

            if (category == null)
                return SimpleResult<CategoryResponse>.Failure(
                    ErrorCode.ResourceNotFound, "Category not found");

            return SimpleResult<CategoryResponse>.Success(new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                ParentId = category.ParentId,
                ParentName = category.Parent?.Name,
                Children = category.Children.Select(c => new CategoryResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    ParentId = c.ParentId
                }).ToList()
            });
        }
    }
}
