using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SharedLibrary.Models;
using SharedLibrary.Models.Restaurateur;

namespace BusinessServer.Network.Restaurateur
{
    public class RestaurateurDataLink : IRestaurateurDataLink
    {
        private readonly string uri = "http://localhost:8080";

        public async Task<bool> AddDishAsync(Dish dish)
        {
            Console.WriteLine("Nåede til RestaurateurDataLink");
            
            using HttpClient client = new HttpClient();
            string dishJson = JsonSerializer.Serialize(dish, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            HttpContent content = new StringContent(
                dishJson, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync(
                $"{uri}/adddish" , content);
            
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Kunne ikke tilføje ret");
            }

            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<List<Dish>> GetDishListAsync(int restaurantId)
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{uri}/dishes/{restaurantId}");
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"StatusCode: {response.StatusCode}");
            }
            
            string listAsJson = await response.Content.ReadAsStringAsync();
            List<Dish> dishList = JsonSerializer.Deserialize<List<Dish>>(listAsJson, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return dishList;
        }

        public async Task<List<Restaurant>> GetRestaurantsFromRestaurateurIdAsync(int restaurateurId)
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{uri}/restaurants/{restaurateurId}");
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"StatusCode: {response.StatusCode}");
            }
            
            string listAsJson = await response.Content.ReadAsStringAsync();
            List<Restaurant> restaurantList = JsonSerializer.Deserialize<List<Restaurant>>(listAsJson, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return restaurantList;
        }
    }
}