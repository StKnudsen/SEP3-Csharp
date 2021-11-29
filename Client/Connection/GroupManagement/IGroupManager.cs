using System.Threading.Tasks;
using SharedLibrary.Models;

namespace Client.Connection.GroupManagement
{
    public interface IGroupManager
    {
        Task<string> CreateGroupAsync(User groupOwner);
        Task<Group> GetGroupFromIdAsync(string groupId);
    }
}