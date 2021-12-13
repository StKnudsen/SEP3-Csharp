using System.Collections.Generic;
using System.Threading.Tasks;
using SharedLibrary.Models;
using SharedLibrary.Models.User;

namespace BusinessServer.Services.GroupService
{
    public interface IGroupService
    {
        Task<bool> AddUserToGroupAsync(User user, string groupId);
        Task<string> CreateNewGroupAsync(User groupOwner);
        Group GetGroupFromId(string groupId);
        Task SetSwipeTypeAsync(string groupId, string type);
        Task<bool> CastVoteAsync(string groupId, int id);
        Task<bool> DoneSwipingAsync(string groupId);
        Task<IList<CustomPair>> StopSwipeAsync(string groupId);
    }
}