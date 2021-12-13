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
        private readonly string uri = "http://localhost:8080";

        public async Task<RegisteredUser> GetUserAsync(string username)
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{uri}/user/{username}");

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
            HttpResponseMessage response = await client.GetAsync($"{uri}/user/guestColoursAnimals");

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
                await client.GetAsync($"{uri}/user/guestUser?colourId={ColourId}&animalId={AnimalId}");

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
        }
    }
}