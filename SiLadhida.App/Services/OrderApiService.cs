using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SiLadhida.App.Models;

namespace SiLadhida.App.Services;

public class OrderApiService : ApiService
{
    public async Task<List<Order>> GetOrdersAsync()
    {
        try
        {
            var response = await HttpClient.GetAsync(
                "api/orders"
            );

            Console.WriteLine(
                $"STATUS: {response.StatusCode}"
            );

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(
                    $"ERROR GET ORDERS: {response.StatusCode}"
                );

                return new List<Order>();
            }

            var result = await response.Content
                .ReadFromJsonAsync<
                    ApiResponse<List<Order>>
                >();

            Console.WriteLine(
                $"TOTAL DATA: {result?.Data?.Count}"
            );

            return result?.Data
                ?? new List<Order>();
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"SERVICE ERROR: {ex.Message}"
            );

            return new List<Order>();
        }
    }
}