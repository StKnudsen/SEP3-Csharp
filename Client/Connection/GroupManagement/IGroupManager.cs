﻿using System.Threading.Tasks;
using Client.Pages;
using SharedLibrary.Models;

namespace Client.Connection.GroupManagement
{
    public interface IGroupManager
    {
        Task<string> CreateGroupAsync(User groupOwner);
        Task<Group> GetGroupFromIdAsync(string groupId);
        Task<bool> JoinGroupAsync(User user, string groupId);
        Task RegisterGroupPage(Groups page);
        Task SetSwipeType(string groupId, string type);
        Task CastVote(string groupId, int id);
        Task RegisterSwipePage(Swipe page);
        Task DoneSwiping(string groupId);
        Task StopSwipeAsync(string groupId);
    }
}