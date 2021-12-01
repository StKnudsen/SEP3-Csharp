using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;

namespace Client.Connection.Administration
{
    public class Administration : IAdministration
    {
        private readonly IJSRuntime JsRuntime;
        private readonly string uriAdminhub = "https://localhost:5001/adminhub";
        private HubConnection HubConnection;

        public Administration(IJSRuntime jsRuntime)
        {
            JsRuntime = jsRuntime;
        }

        public async Task AddIngredientAsync(string ingredientName, int _foodGroupId)
        {
            if (HubConnection is null)
            {
                HubConnection = new HubConnectionBuilder().WithUrl(uriAdminhub).Build();
                await HubConnection.StartAsync();
            }

            Console.WriteLine("Hi from Administration: " + ingredientName + _foodGroupId);
            
            await HubConnection.InvokeAsync<Task>
                ("AddIngredientAsync", ingredientName, _foodGroupId);
        }

        public async Task<Dictionary<int, string>> getFoodgroupListAsync()
        {
            if (HubConnection is null)
            {
                HubConnection = new HubConnectionBuilder().WithUrl(uriAdminhub).Build();
                await HubConnection.StartAsync();
            }

            return await HubConnection.InvokeAsync<Dictionary<int, string>>
                ("getFoodgroupListAsync");
        }
    }
}