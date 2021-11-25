using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Client.Authentication;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using SharedLibrary.Models;


namespace Client.GroupManagement
{
    public class GroupManager
    {
        private readonly IJSRuntime JsRuntime;
        private readonly string uriGrouphub = "https://localhost:5001/grouphub";
        private HubConnection HubConnection;

        public GroupManager(IJSRuntime jsRuntime)
        {
            JsRuntime = jsRuntime;
        }

        public async Task CreateGroupAsync(User groupOwner)
        {
            HubConnection = new HubConnectionBuilder().WithUrl(uriGrouphub).Build();
            await HubConnection.StartAsync();
            if (groupOwner.Equals(null))
            {
                string userAsJson = await JsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
                if (!string.IsNullOrEmpty(userAsJson))
                {
                    groupOwner = JsonSerializer.Deserialize<User>(userAsJson);
                }
            }

            await HubConnection.InvokeAsync("CreateGroupAsync", groupOwner);
        }
    }
}