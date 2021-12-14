using System.Threading.Tasks;
using Client.Pages;
using SharedLibrary.Models;
using SharedLibrary.Models.User;

namespace Client.Connection.GroupManagement
{
    public interface IGroupManager
    {
        Task<string> CreateGroupAsync(User groupOwner);
        Task<Group> GetGroupFromIdAsync(string groupId);
        Task<bool> JoinGroupAsync(User user, string groupId);
        Task RegisterGroupPageAsync(Groups page);
        Task SetSwipeTypeAsync(string groupId, string type);
        Task CastVoteAsync(string groupId, int id);
        Task RegisterSwipePageAsync(Swipe page);
        Task DoneSwipingAsync(string groupId);
        Task StopSwipeAsync(string groupId);
    }
}