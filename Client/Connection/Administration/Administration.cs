using System.Collections.Generic;
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

            await HubConnection.InvokeAsync<string>("addIngredient", ingredientName, _foodGroupId);
        }
    }
}