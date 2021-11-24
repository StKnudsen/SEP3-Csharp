using System.Threading.Tasks;
using SharedLibrary.Models;

namespace BusinessServer.Services
{
    public interface IGroupService
    {
        Task AddUserToGroupAsync(User user, string groupId);
        Task CreateNewGroupAsync(User groupOwner);
    }
}