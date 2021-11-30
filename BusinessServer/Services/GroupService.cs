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

        public async Task<bool> AddUserToGroupAsync(User user, string groupId)
        {
            try
            {
                Group group = ActiveGroups.Find(g => g.Id.Equals(groupId));
                group.Users.Add(user);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Gruppe ikke fundet");
            }
        }

        public async Task<string> CreateNewGroupAsync(User groupOwner)
        {
            try
            {
                string groupId;
                do
                {
                    groupId = RandomString(6);
                } while (ActiveGroups.Any(g => g.Id.Equals(groupId)));
            
                Group newGroup = new Group(groupOwner, groupId);
                ActiveGroups.Add(newGroup);
                Console.WriteLine(ActiveGroups.Count);
                return groupId;
            }
            catch (Exception e)
            {
                throw new Exception("Opret gruppe sejler i GroupService");
            }
        }

        public async Task<Group> GetGroupFromId(string groupId)
        {
            Console.WriteLine(ActiveGroups.Count);
            return ActiveGroups.Find(g => g.Id.Equals(groupId));
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