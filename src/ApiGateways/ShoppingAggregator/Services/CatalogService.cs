using ShoppingAggregator.Models;
using ShoppingAggregator.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShoppingAggregator.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient client;

        public CatalogService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            var reponse = await client.GetAsync("/api/v1/Catalog");
            return await reponse.ReadContentAs<List<CatalogModel>>();
        }

        public async Task<CatalogModel> GetCatalog(string id)
        {
            var reponse = await client.GetAsync($"/api/v1/Catalog/{id}");
            return await reponse.ReadContentAs<CatalogModel>();
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
        {
            var reponse = await client.GetAsync($"/api/v1/Catalog/GetProductByCategory/{category}");
            return await reponse.ReadContentAs<List<CatalogModel>>();
        }
    }
}
