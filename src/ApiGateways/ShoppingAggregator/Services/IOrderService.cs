using ShoppingAggregator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingAggregator.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseModel>> GetOrderByUserName(string userName);
    }
}
