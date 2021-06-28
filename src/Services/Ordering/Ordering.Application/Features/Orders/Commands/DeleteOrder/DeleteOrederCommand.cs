using MediatR;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrederCommand : IRequest
    {
        public int Id { get; set; }
    }
}
