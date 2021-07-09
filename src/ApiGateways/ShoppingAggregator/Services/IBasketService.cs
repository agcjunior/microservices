using ShoppingAggregator.Models;
using System.Threading.Tasks;

namespace ShoppingAggregator.Services
{
    public interface IBasketService
    {
        Task<BasketModel> GetBasket(string userName);
    }
}
