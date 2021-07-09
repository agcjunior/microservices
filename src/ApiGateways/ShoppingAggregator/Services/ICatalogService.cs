using ShoppingAggregator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingAggregator.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogModel>> GetCatalog();
        Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category);
        Task<CatalogModel> GetCatalog(string id);

    }
}
