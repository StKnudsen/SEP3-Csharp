using System;
using System.Threading.Tasks;
using BusinessServer.Services;
using Microsoft.AspNetCore.SignalR;
using SharedLibrary.Models;

namespace BusinessServer.Hubs
{
    public class GroupHub : Hub
    {
        private readonly IGroupService GroupService;

        public GroupHub()
        {
            GroupService = new GroupService();
        }

        public async Task<string> CreateGroupAsync(User groupOwner)
        {
            string groupId = await GroupService.CreateNewGroupAsync(groupOwner);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
            return groupId;
        }

        public async Task<Group> GetGroupFromIdAsync(string groupId)
        {

            Group groupFromId = await GroupService.GetGroupFromId(groupId);

            return groupFromId;
        }

        public async Task<bool> JoinGroupAsync(User user, string groupId)
        {
            bool response = await GroupService.AddUserToGroupAsync(user, groupId);

            if (response)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
                await UpdateGroupAsync(groupId);
                return true;
            }
            
            return false;
        }

        public async Task SetSwipeType(string groupId, string type)
        {
            await GroupService.SetSwipeType(groupId, type);
            await Clients.Group(groupId).SendAsync("SwipeStart");
        }

        private async Task UpdateGroupAsync(string groupId)
        {
            await Clients.Group(groupId).SendAsync("UpdateGroup");
        }

        public async Task CastVote(string groupId, int id)
        {
            bool match = await GroupService.CastVote(groupId, id);

            if (match)
            {
                await Clients.Group(groupId).SendAsync("Match", id);
            }
        }
    }
}