using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository repository;
        private readonly IMapper mapper;        
        private readonly ILogger<UpdateOrderCommandHandler> logger;

        public UpdateOrderCommandHandler(IOrderRepository repository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToUpdate = await repository.GetByIdAsync(request.Id);

            if (orderToUpdate == null)
            {
                throw new NotFoundException(nameof(Order), request.Id);
            }

            mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommand), typeof(Order));
            await repository.UpdateAsync(orderToUpdate);
            logger.LogInformation($"Order {orderToUpdate.Id} is updated");
            return Unit.Value;
        }
    }
}
