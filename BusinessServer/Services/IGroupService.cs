using System.Threading.Tasks;
using SharedLibrary.Models;

namespace BusinessServer.Services
{
    public interface IGroupService
    {
        Task<bool> AddUserToGroupAsync(User user, string groupId);
        Task<string> CreateNewGroupAsync(User groupOwner);
        Task<Group> GetGroupFromId(string groupId);
    }
}