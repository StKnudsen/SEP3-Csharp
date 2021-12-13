using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;

namespace Client.Connection.DNNR
{
    public class SharedDNNR : ISharedDNNR
    {
        private readonly string uriDnnrHub = "https://localhost:5001/dnnrhub";
        private HubConnection HubConnection;

        public SharedDNNR() { }
        
        public async Task<Dictionary<int, string>> GetFoodgroupListAsync()
        {
            if (HubConnection is null)
            {
                HubConnection = new HubConnectionBuilder().WithUrl(uriDnnrHub).Build();
                await HubConnection.StartAsync();
            }

            return await HubConnection.InvokeAsync<Dictionary<int, string>>
                ("GetFoodgroupListAsync");
        }
        
        public async Task<Dictionary<int, string>> GetIngredientListAsync()
        {
            if (HubConnection is null)
            {
                HubConnection = new HubConnectionBuilder().WithUrl(uriDnnrHub).Build();
                await HubConnection.StartAsync();
            }

            return await HubConnection.InvokeAsync<Dictionary<int, string>>
                ("GetIngredientListAsync");
        }
    }
}