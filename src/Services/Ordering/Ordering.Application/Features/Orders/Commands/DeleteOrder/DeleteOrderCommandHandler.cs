using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrederCommand>
    {
        private readonly IOrderRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<DeleteOrderCommandHandler> logger;

        public DeleteOrderCommandHandler(IOrderRepository repository, IMapper mapper, ILogger<DeleteOrderCommandHandler> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<Unit> Handle(DeleteOrederCommand request, CancellationToken cancellationToken)
        {
            var orderToDelete = await repository.GetByIdAsync(request.Id);

            if (orderToDelete == null)
            {
                throw new NotFoundException(nameof(Order), request.Id);
            }
            await repository.DeleteAsync(orderToDelete);
            logger.LogInformation($"Order {orderToDelete.Id} is deleted");
            return Unit.Value;
        }
    }
}
