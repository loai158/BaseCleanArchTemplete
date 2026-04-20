using Domain.Entities.Business;
using Domain.Exceptions;
using Domain.Features.Categories.Commands;
using Domain.Features.Categories.Responses;
using Domain.Interfaces;
using MediatR;

namespace Service.Handlers.Categories
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, SimpleResult<CreateCategoryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<SimpleResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            // 1. لو فيه ParentId تأكد إنه موجود
            if (request.ParentId.HasValue)
            {
                var parent = await _unitOfWork.Repository<Category>()
                    .GetByIdAsync(request.ParentId.Value);

                if (parent == null)
                    return SimpleResult<CreateCategoryResponse>.Failure(
                        ErrorCode.ResourceNotFound, "Parent category not found");
            }
            // 2. تأكد إن الاسم مش موجود
            var existing = await _unitOfWork.Repository<Category>()
                .GetOneAsync(c => c.Name == request.Name && c.ParentId == request.ParentId);

            if (existing != null)
                return SimpleResult<CreateCategoryResponse>.Failure(
                    ErrorCode.BusinessRuleViolation, "Category with same name already exists");
            var category = new Category();
            category.Create(request.Name, request.ParentId);

            await _unitOfWork.Repository<Category>().AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return SimpleResult<CreateCategoryResponse>.Success(new CreateCategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                ParentId = category.ParentId
            });
        }
    }
}
