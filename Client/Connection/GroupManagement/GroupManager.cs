using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using SharedLibrary.Models;

namespace Client.Connection.GroupManagement
{
    public class GroupManager : IGroupManager
    {
        private readonly IJSRuntime JsRuntime;
        private readonly string uriGrouphub = "https://localhost:5001/grouphub";
        private HubConnection HubConnection;

        public GroupManager(IJSRuntime jsRuntime)
        {
            JsRuntime = jsRuntime;
            
            HubConnection = new HubConnectionBuilder().WithUrl(uriGrouphub).WithAutomaticReconnect().Build();

            HubConnection.On<Group>("UpdateGroup", (group) =>
            {
                Console.WriteLine("HURRA!!!! TØSERNE GIVER KAGE, TINA BAGER!!");
            });

            HubConnection.StartAsync();
        }

        public async Task<string> CreateGroupAsync(User groupOwner)
        {
            if (groupOwner.Equals(null))
            {
                string userAsJson = await JsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
                if (!string.IsNullOrEmpty(userAsJson))
                {
                    groupOwner = JsonSerializer.Deserialize<User>(userAsJson);
                }
            }

            return await HubConnection.InvokeAsync<string>("CreateGroupAsync", groupOwner);
        }

        public async Task<Group> GetGroupFromIdAsync(string groupId)
        {
            Group group = await HubConnection.InvokeAsync<Group>("GetGroupFromIdAsync", groupId);
            return group;
        }

        public async Task<bool> JoinGroupAsync(User user, string groupId)
        {
            bool response = await HubConnection.InvokeAsync<bool>("JoinGroupAsync", user, groupId);

            if (!response)
            {
                throw new Exception("FEJL! Kunne ikke tilføje bruger til gruppe.");
            }

            return true;
        }
        
    }
}