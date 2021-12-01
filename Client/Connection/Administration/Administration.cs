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

        public async Task<bool> AddIngredientAsync(string ingredientName, int _foodGroupId)
        {
            try
            {
                if (HubConnection is null)
                {
                    HubConnection = new HubConnectionBuilder().WithUrl(uriAdminhub).Build();
                    await HubConnection.StartAsync();
                }
                
                return  await HubConnection.InvokeAsync<bool>
                    ("AddIngredientAsync", ingredientName, _foodGroupId);
            }
            catch (Exception e)
            {
                throw(new Exception(e.Message));
            }
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