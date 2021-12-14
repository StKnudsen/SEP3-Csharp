using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessServer.Services.GroupService;
using Microsoft.AspNetCore.SignalR;
using SharedLibrary.Models;
using SharedLibrary.Models.User;

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

        //FIXME ASYNC!!
        public async Task DoneSwiping(string groupId)
        {
            bool done = await GroupService.DoneSwipingAsync(groupId);

            if (done)
            {
                await Clients.Group(groupId).SendAsync("NoMatch");
            }
        }

        //FIXME ASYNC!!
        public async Task SetSwipeType(string groupId, string type)
        {
            await GroupService.SetSwipeTypeAsync(groupId, type);
            await Clients.Group(groupId).SendAsync("SwipeStart");
        }

        private async Task UpdateGroupAsync(string groupId)
        {
            await Clients.Group(groupId).SendAsync("UpdateGroup");
        }

        //FIXME ASYNC!!
        public async Task CastVote(string groupId, int id)
        {
            bool match = await GroupService.CastVoteAsync(groupId, id);

            if (match)
            {
                await Clients.Group(groupId).SendAsync("Match", id);
            }
        }

        public async Task StopSwipeAsync(string groupId)
        {
            IList<CustomPair> finishedVoteList =await GroupService.StopSwipeAsync(groupId);

            await Clients.Group(groupId).SendAsync("Stop", finishedVoteList);
        }
    }
}