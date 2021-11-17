using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SharedLibrary.Models;

namespace BusinessServer.Network
{
    public class DataLink : IDataLink
    {
        public async Task<User> GetUserAsync(string username)
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"http://localhost:8080/user/{username}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"StatusCode: {response.StatusCode}");
            }

            string userAsString = await response.Content.ReadAsStringAsync();
            User user = JsonSerializer.Deserialize<User>(userAsString, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return user;
        }
    }
}