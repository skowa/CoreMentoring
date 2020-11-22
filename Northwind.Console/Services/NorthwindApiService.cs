using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Northwind.Console.Interfaces.Services;
using Northwind.Web.Models;

namespace Northwind.Console.Services
{
    public class NorthwindApiService : INorthwindApiService
    {
        private const string CategoriesPath = "categories";
        private const string ProductsPath = "products";

        private readonly HttpClient _httpClient;

        public NorthwindApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await GetAsync<Category>(CategoriesPath);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await GetAsync<Product>(ProductsPath);
        }

        private async Task<IEnumerable<T>> GetAsync<T>(string path)
        {
            IEnumerable<T> items = null;

            var response = await _httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                items = await response.Content.ReadAsAsync<IEnumerable<T>>();
            }

            return items;
        }
    }
}
