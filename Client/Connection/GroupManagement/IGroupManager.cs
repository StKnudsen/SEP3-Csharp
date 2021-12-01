using System.Threading.Tasks;
using Client.Pages;
using SharedLibrary.Models;

namespace Client.Connection.GroupManagement
{
    public interface IGroupManager
    {
        Task<string> CreateGroupAsync(User groupOwner);
        Task<Group> GetGroupFromIdAsync(string groupId);
        Task<bool> JoinGroupAsync(User user, string groupId);
        Task RegisterPage(Groups page);
    }
}