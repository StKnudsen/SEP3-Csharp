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
                $"{uri}/addingredients" , content);
            
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Kunne ikke tilføje ingrediens");
            }

            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<bool> AddRecipeAsync(Recipe recipe)
        {
            
            using HttpClient client = new HttpClient();
            string recipeJson = JsonSerializer.Serialize(recipe, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            HttpContent content = new StringContent(
                recipeJson, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync(
                $"{uri}/addrecipe" , content);
            
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Kunne ikke tilføje opskrift");
            }

            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<bool> AddRestaurantAsync(Restaurant restaurant)
        {
            Console.WriteLine("AdminDataLink i AddRestaurantAsync");
            
            using HttpClient client = new HttpClient();
            string restaurantJson = JsonSerializer.Serialize(restaurant, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            HttpContent content = new StringContent(restaurantJson, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync(
                $"{uri}/addrestaurant" , content);

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Kunne ikke tilføje restaurant");
            }

            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<Dictionary<int, string>> GetUnitListAsync()
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{uri}/units");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"StatusCode: {response.StatusCode}");
            }

            string responseAsString = await response.Content.ReadAsStringAsync();
            Dictionary<int, string> UnitsList = 
                JsonSerializer.Deserialize<Dictionary<int, string>>(responseAsString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            return UnitsList;
        }

        public async Task<Dictionary<int, string>> GetRecipeListAsync()
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{uri}/recipes");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"StatusCode: {response.StatusCode}");
            }

            string responseAsString = await response.Content.ReadAsStringAsync();
            Dictionary<int, string> recipesList = 
                JsonSerializer.Deserialize<Dictionary<int, string>>(responseAsString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            return recipesList;
        }

        public async Task<List<Restaurant>> GetRestaurantListAsync()
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{uri}/restaurants");
            
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

        public async Task<List<Address>> GetAddressListAsync()
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{uri}/address");
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"StatusCode: {response.StatusCode}");
            }
            
            string listAsJson = await response.Content.ReadAsStringAsync();
            List<Address> addressList = JsonSerializer.Deserialize<List<Address>>(listAsJson, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return addressList;
        }

        public async Task<Address> GetAddressByIdAsync(int addressId)
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{uri}/address/{addressId}");
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"StatusCode: {response.StatusCode}");
            }
            
            string addressAsString = await response.Content.ReadAsStringAsync();
            Address address = JsonSerializer.Deserialize<Address>(addressAsString, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return address;
        }
    }
}