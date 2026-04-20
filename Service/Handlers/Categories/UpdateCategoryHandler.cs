using Domain.Entities.Business;
using Domain.Exceptions;
using Domain.Features.Categories.Commands;
using Domain.Interfaces;
using MediatR;

namespace Service.Handlers.Categories
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, SimpleResult<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<SimpleResult<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Repository<Category>()
           .GetByIdAsync(request.Id);

            if (category == null)
                return SimpleResult<bool>.Failure(
                    ErrorCode.ResourceNotFound, "Category not found");

            // 2. تأكد مش بتحط نفسه كـ Parent
            if (request.ParentId.HasValue && request.ParentId == request.Id)
                return SimpleResult<bool>.Failure(
                    ErrorCode.BusinessRuleViolation, "Category cannot be its own parent");

            // 3. تأكد إن الـ Parent موجود
            if (request.ParentId.HasValue)
            {
                var parent = await _unitOfWork.Repository<Category>()
                    .GetByIdAsync(request.ParentId.Value);

                if (parent == null)
                    return SimpleResult<bool>.Failure(
                        ErrorCode.ResourceNotFound, "Parent category not found");
            }

            // 4. Update
            category.Update(request.Name, request.ParentId);

            await _unitOfWork.Repository<Category>().UpdateAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return SimpleResult<bool>.Success(true);
        }
    }
}
