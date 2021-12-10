using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessServer.Network.DNNRDataLink
{
    public class DNNRDataLink : IDNNRDataLink
    {
        private readonly string uri = "http://localhost:8080";
        
        public async Task<Dictionary<int, string>> GetIngredientListAsync()
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{uri}/ingredients");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"StatusCode: {response.StatusCode}");
            }

            string responseAsString = await response.Content.ReadAsStringAsync();
            Dictionary<int, string> IngredientsList = 
                JsonSerializer.Deserialize<Dictionary<int, string>>(responseAsString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            return IngredientsList;
        }
        
        public async Task<Dictionary<int, string>> GetFoodgroupListAsync()
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync
                ($"{uri}/foodgroups");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"StatusCode: {response.StatusCode}");
            }

            string responseAsString = await response.Content.ReadAsStringAsync();
            Dictionary<int, string> FoodgroupList = 
                JsonSerializer.Deserialize<Dictionary<int, string>>(responseAsString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            return FoodgroupList;
        }
    }
}