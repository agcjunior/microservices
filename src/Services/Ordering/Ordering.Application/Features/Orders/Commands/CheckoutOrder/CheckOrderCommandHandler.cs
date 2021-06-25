using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository repository;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;
        private readonly ILogger<CheckOrderCommandHandler> logger;

        public CheckOrderCommandHandler(
            IOrderRepository repository,
            IMapper mapper,
            IEmailService emailService,
            ILogger<CheckOrderCommandHandler> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.emailService = emailService;
            this.logger = logger;
        }

       

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = mapper.Map<Order>(request);
            var orderCreated = await repository.AddAsync(orderEntity);
            logger.LogInformation($"Order {orderCreated.Id} is successfully created");

            await SendMail(orderCreated);
            return orderCreated.Id;

        }

        private async Task SendMail(Order orderCreated)
        {
            var email = new Email()
            {
                To = "antonio.jr@outlook.com",
                Subject = "Order created",
                Body = $"The order {orderCreated.Id} has been created"
            };

            try
            {
                await emailService.SendMail(email);
            }
            catch (Exception ex)
            {
                logger.LogError($"Order {orderCreated.Id} failed sending email: {ex.Message}");
            }
        }
    }
}
