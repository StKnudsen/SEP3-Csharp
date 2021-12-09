using System.Collections.Generic;
using System.Threading.Tasks;
using SharedLibrary.Models;

namespace BusinessServer.Services
{
    public interface IGroupService
    {
        Task<bool> AddUserToGroupAsync(User user, string groupId);
        Task<string> CreateNewGroupAsync(User groupOwner);
        Group GetGroupFromId(string groupId);
        Task SetSwipeType(string groupId, string type);
        Task<bool> CastVote(string groupId, int id);
        Task<bool> DoneSwiping(string groupId);
        Task<IList<CustomPair>> StopSwipeAsync(string groupId);
    }
}