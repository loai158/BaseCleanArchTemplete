using Domain.Entities.Business;
using Domain.Exceptions;
using Domain.Features.Categories.Commands;
using Domain.Interfaces;
using MediatR;

namespace Service.Handlers.Categories
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, SimpleResult<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<SimpleResult<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Repository<Category>()
            .GetByIdAsync(request.Id);

            if (category == null)
                return SimpleResult<bool>.Failure(
                    ErrorCode.ResourceNotFound, "Category not found");

            // تأكد مفيش Children
            if (category.Children.Any())
                return SimpleResult<bool>.Failure(
                    ErrorCode.BusinessRuleViolation, "Cannot delete category that has subcategories");

            // تأكد مفيش Products
            if (category.Products.Any())
                return SimpleResult<bool>.Failure(
                    ErrorCode.BusinessRuleViolation, "Cannot delete category that has products");

            await _unitOfWork.Repository<Category>().DeleteAsync(category.Id);
            await _unitOfWork.SaveChangesAsync();

            return SimpleResult<bool>.Success(true);
        }
    }
}
