using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Client.Connection.UserManager
{
    public class UserManager : IUserManager
    {
        private readonly string uriUserHub = "https://localhost:5001/userhub";
        private HubConnection HubConnection;

        public UserManager(){ }

        public async Task<Dictionary<int, string>> GetAllergyFoodGroupListAsync(int userId)
        {
            await GetHubConnectionAsync();
            
            if (userId == 0)
            {
                throw new Exception("Line siger: \"Get f#*ked\" part 1 af 4");
            }
            
            return await HubConnection.InvokeAsync<Dictionary<int, string>>("getAllergyFoodGroupListAsync", userId);
        }

        public async Task<Dictionary<int, string>> GetAllergyIngredientListAsync(int userId)
        {
            await GetHubConnectionAsync();
            
            if (userId == 0)
            {
                throw new Exception("Line siger: \"Get f#*ked\" part 2 af 4");
            }
            
            return await HubConnection.InvokeAsync<Dictionary<int, string>>("getAllergyIngredientListAsync", userId);
        }

        public async Task<bool> SetUserAllergyFoodGroupAsync(int userId, int foodGroupId)
        {
            await GetHubConnectionAsync();
            
            if (userId == 0 || foodGroupId == 0)
            {
                throw new Exception("Line siger: \"Get f#*ked\" part 3 af 4");
            }

            return await HubConnection.InvokeAsync<bool>("setUserAllergyFoodGroupAsync", userId, foodGroupId);
        }

        public async Task<bool> SetUserAllergyIngredientAsync(int userId, int ingredient)
        {
            await GetHubConnectionAsync();
            
            if (userId == 0 || ingredient == 0)
            {
                throw new Exception("Line siger: \"Get f#*ked\" part 4 af 4");
            }

            return await HubConnection.InvokeAsync<bool>("SetUserAllergyIngredientAsync", userId, ingredient);
        }

        private async Task GetHubConnectionAsync()
        {
            if (HubConnection is null)
            {
                HubConnection = new HubConnectionBuilder().WithUrl(uriUserHub).Build();
                await HubConnection.StartAsync();
            }
        }
    }
}