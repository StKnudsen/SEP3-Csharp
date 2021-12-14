using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessServer.Models;
using SharedLibrary.Models;

namespace BusinessServer.Network.UserDataLink
{
    public class UserDataLink : IUserDataLink
    {
        private const string URI = "http://localhost:8080";

        public async Task<RegisteredUser> GetUserAsync(string username)
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{URI}/user/{username}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"StatusCode: {response.StatusCode}");
            }

            string userAsString = await response.Content.ReadAsStringAsync();
            RegisteredUser user = JsonSerializer.Deserialize<RegisteredUser>(userAsString, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return user;
        }

        public async Task<ColourAnimalCount> GetColourAnimalCountAsync()
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{URI}/user/guestColoursAnimals");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"StatusCode: {response.StatusCode}");
            }

            string countAsString = await response.Content.ReadAsStringAsync();
            ColourAnimalCount count = JsonSerializer.Deserialize<ColourAnimalCount>(countAsString,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            return count;
        }

        public async Task<GuestUser> GetGuestUserAsync(int ColourId, int AnimalId)
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response =
                await client.GetAsync($"{URI}/user/guestUser?colourId={ColourId}&animalId={AnimalId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"StatusCode: {response.StatusCode}");
            }

            string guestAsJson = await response.Content.ReadAsStringAsync();
            GuestUser guest = JsonSerializer.Deserialize<GuestUser>(guestAsJson, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return guest;
        }

        public async Task<bool> CreateUserAsync(RegisteredUser user)
        {
            using HttpClient client = new HttpClient();

            string userAsJson = JsonSerializer.Serialize(user, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            HttpContent content = new StringContent(
                userAsJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{uri}/user", content);
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Kunne ikke oprette bruger");
            }

            return response.IsSuccessStatusCode;

        public async Task<Dictionary<int, string>> getAllergyFoodGroupListAsync(int userId)
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response =
                await client.GetAsync($"{URI}/allergy/{userId}/foodgroup");
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"StatusCode: {response.StatusCode}");
            }

            string responseAsString = await response.Content.ReadAsStringAsync();

            Dictionary<int, string> foodGroupAllergiesList = JsonSerializer.Deserialize<Dictionary<int, string>>(
                responseAsString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            return foodGroupAllergiesList;
        }

        public async Task<Dictionary<int, string>> getAllergyIngredientListAsync(int userId)
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response =
                await client.GetAsync($"{URI}/allergy/{userId}/ingredient");
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"StatusCode: {response.StatusCode}");
            }

            string responseAsString = await response.Content.ReadAsStringAsync();

            Dictionary<int, string> allergyIngredientList = JsonSerializer.Deserialize<Dictionary<int, string>>(
                responseAsString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            return allergyIngredientList;
        }

        public async Task<bool> SetUserAllergyFoodGroupAsync(int userId, int foodGroupId)
        {
            using HttpClient client = new HttpClient();
            
            HttpResponseMessage responseMessage = await client.PostAsync(
                $"{URI}/allergy/{userId}/foodgroup?id={foodGroupId}", null!
                );

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Fejl i oprettelse af fødevaregruppe allergi");
            }

            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<bool> SetUserAllergyIngredientAsync(int userId, int ingredientId)
        {
            using HttpClient client = new HttpClient();

            HttpResponseMessage responseMessage = await client.PostAsync(
                $"{URI}/allergy/{userId}/ingredient?id={ingredientId}", null!
                );
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Fejl i oprettelse af ingrediens allergi");
            }

            return responseMessage.IsSuccessStatusCode;
        }
    }
}