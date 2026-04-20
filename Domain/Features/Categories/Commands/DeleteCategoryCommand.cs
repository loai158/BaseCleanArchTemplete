using Domain.Exceptions;
using MediatR;

namespace Domain.Features.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest<SimpleResult<bool>>
    {
        public int Id { get; set; }

    }
}
