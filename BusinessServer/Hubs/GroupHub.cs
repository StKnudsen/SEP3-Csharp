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

        public GroupHub(IGroupService groupService)
        {
            GroupService = groupService;
        }

        public async Task<string> CreateGroupAsync(User groupOwner)
        {
            string groupId = await GroupService.CreateNewGroupAsync(groupOwner);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
            return groupId;
        }

        public async Task<Group> GetGroupFromIdAsync(string groupId)
        {
            Group groupFromId = GroupService.GetGroupFromId(groupId);
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

        private Task UpdateGroupAsync(string groupId)
        {
            //Object[] group = {await GetGroupFromIdAsync(groupId)};
            return Clients.Group(groupId).SendAsync("UpdateGroup", GetGroupFromIdAsync(groupId));
        }
    }
}