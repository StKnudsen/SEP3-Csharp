using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessServer.Models;
using Microsoft.AspNetCore.Http;
using SharedLibrary.Models;

namespace BusinessServer.Network.AdminDataLink
{
    public class AdminDataLink : IAdminDataLink
    {
        private readonly string uri = "http://localhost:8080";
        
        public async Task<bool> AddIngredientAsync(string ingredientName, int foodGroupId)
        {
            CustomPair ingredientAndFoodgroup = new CustomPair()
            {
                Key = foodGroupId,
                Value = ingredientName
            };
            
            using HttpClient client = new HttpClient();
            string ingredientNameJson = JsonSerializer.Serialize(ingredientAndFoodgroup, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            HttpContent content = new StringContent(
                ingredientNameJson, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync(
                $"{uri}/ingredients" , content);
            
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Kunne ikke tilføje ingrediens");
            }

            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<Dictionary<int, string>> GetFoodgroupListAsync()
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{uri}/foodgroups");

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
    }
}