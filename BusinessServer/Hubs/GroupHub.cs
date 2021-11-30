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
            return await GroupService.CreateNewGroupAsync(groupOwner);
        }

        public async Task<Group> GetGroupFromIdAsync(string groupId)
        {
            Group groupFromId = await GroupService.GetGroupFromId(groupId);

            return groupFromId;
        }
    }
}