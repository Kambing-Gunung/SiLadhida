using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SiLadhida.App.Models;
using System.Net.Http;

namespace SiLadhida.App.Services
{
    public class ApiService
    {
        private readonly HttpClient _client;

        public ApiService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:5135/");
        }

        public async Task<List<Order>> GetOrders()
        {
            var response = await _client.GetAsync("api/orders");

            if (!response.IsSuccessStatusCode)
                return new List<Order>();

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Order>>(json);
        }
    }
}