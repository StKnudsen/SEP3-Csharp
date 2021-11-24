using System.Threading.Tasks;
using BusinessServer.Services;
using Microsoft.AspNetCore.SignalR;
using SharedLibrary.Models;

namespace BusinessServer.Hubs
{
    public class GroupHub :Hub
    {
        private readonly IGroupService GroupService;

        public GroupHub()
        {
            GroupService = new GroupService();
        }

        public async Task CreateGroupAsync(User groupOwner)
        {
            await GroupService.CreateNewGroupAsync(groupOwner);
        }
    }
}