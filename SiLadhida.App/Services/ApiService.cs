using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SiLadhida.App.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace SiLadhida.App.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5135/");
        }

        public async Task<List<Produk>> GetProdukAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Produk>>(
                "http://localhost:5135/api/products"
            );

            return result ?? new List<Produk>();
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Order>>(
                "http://localhost:5135/api/orders"
            );

            return result ?? new List<Order>();
        }

        public async Task CreateOrderAsync(CreateOrderRequest request)
        {
            await _httpClient.PostAsJsonAsync(
                "http://localhost:5135/api/orders",
                request
            );
        }

    }
}