using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedLibrary.Models;

namespace BusinessServer.Services
{
    public class GroupService : IGroupService
    {
        private List<Group> ActiveGroups;

        public GroupService()
        {
            ActiveGroups = new List<Group>();
        }

        public async Task AddUserToGroupAsync(User user, string groupId)
        {
            try
            {
                Group group = ActiveGroups.Find(g => g.Id.Equals(groupId));
                group.Users.Add(user);
            }
            catch (Exception e)
            {
                throw new Exception("Gruppe ikke fundet");
            }
        }

        public async Task CreateNewGroupAsync(User groupOwner)
        {
            string groupId;
            do
            {
                groupId = RandomString(6);
            } while (ActiveGroups.Any(g => g.Id.Equals(groupId)));
            
            Group newGroup = new Group(groupOwner, groupId);
            ActiveGroups.Add(newGroup);
        }

        private static Random random = new();

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}