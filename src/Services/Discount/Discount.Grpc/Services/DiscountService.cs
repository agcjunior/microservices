using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Discount.Grpc.Protos.DiscountProtoService;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoServiceBase
    {
        private readonly IDiscountRepository repository;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public DiscountService(IDiscountRepository repository, ILogger logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public override async Task<CuponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var cupon = await repository.GetDiscount(request.ProductName);

            if (cupon == null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount for {request.ProductName} not found"));

            logger.LogInformation($"Discount is retrieved for ProductName {request.ProductName}");
            var cuponModel = mapper.Map<CuponModel>(cupon);
            return cuponModel;
        }

        public override async Task<CuponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var cupon = mapper.Map<Coupon>(request.Coupon);
            await repository.CreateDiscount(cupon);
            logger.LogInformation("Discount created");
            var cuponModel = mapper.Map<CuponModel>(cupon);
            return cuponModel;
        }

        public override async Task<CuponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var cupon = mapper.Map<Coupon>(request.Coupon);
            await repository.UpdateDiscount(cupon);
            logger.LogInformation("Discount updated");
            var cuponModel = mapper.Map<CuponModel>(cupon);
            return cuponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await repository.DeleteDiscount(request.ProductName);
            var response = new DeleteDiscountResponse { Success = deleted };
            return response;
        }
    }
}
