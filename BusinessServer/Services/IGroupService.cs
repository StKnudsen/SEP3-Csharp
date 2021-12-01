using System.Threading.Tasks;
using SharedLibrary.Models;

namespace BusinessServer.Services
{
    public interface IGroupService
    {
        Task<bool> AddUserToGroupAsync(User user, string groupId);
        Task<string> CreateNewGroupAsync(User groupOwner);
        Group GetGroupFromId(string groupId);
    }
}