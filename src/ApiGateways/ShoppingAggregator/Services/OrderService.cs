using ShoppingAggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShoppingAggregator.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient client;

        public OrderService(HttpClient client)
        {
            this.client = client;
        }

        public Task<IEnumerable<OrderResponseModel>> GetOrderByUserName(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
