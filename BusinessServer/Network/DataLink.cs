using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessServer.Services;
using SharedLibrary.Models;


namespace BusinessServer.Network
{
    public class DataLink : IDataLink
    {
        public async Task<bool> ValidateUser(User user)
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"http://localhost:8080/user/{user.Username}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"StatusCode: {response.StatusCode}");
            }

            string controlAsString = await response.Content.ReadAsStringAsync();
            User controlUser = JsonSerializer.Deserialize<User>(controlAsString, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            using Md5Check comparator = new Md5Check();

            return comparator.Compare(user.Password, controlUser?.Password);
        }
    }
}