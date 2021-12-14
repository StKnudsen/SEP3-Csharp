using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessServer.Network.Group;
using BusinessServer.Services.UserService;
using Microsoft.VisualBasic;
using SharedLibrary.Models;
using SharedLibrary.Models.User;
using SharedLibrary.Util;

namespace BusinessServer.Services.GroupService
{
    public class GroupService : IGroupService
    {
        private readonly IGroupDataLink DataLink;
        private List<Group> ActiveGroups;
        private IGetUserInfo GetUserInfo;

        public GroupService()
        {
            ActiveGroups = new List<Group>();
            DataLink = new GroupDataLink();
            GetUserInfo = new UserService.UserService();
        }

        public async Task<bool> AddUserToGroupAsync(User user, string groupId)
        {
            try
            {
                Group group = ActiveGroups.Find(g => g.Id.Equals(groupId));
                if (group.SwipeType is null)
                {
                    group.Users.Add(user);
                    return true;
                }

                throw new Exception();
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
                return groupId;
            }
            catch (Exception e)
            {
                throw new Exception("Opret gruppe sejler i GroupService");
            }
        }

        public Group GetGroupFromId(string groupId)
        {
            Console.WriteLine(ActiveGroups.Count);
            Group groupFromId = ActiveGroups.Find(g => g.Id.Equals(groupId));
            return groupFromId;
        }

        public async Task SetSwipeTypeAsync(string groupId, string type)
        {
            Console.WriteLine("Sætter swipe type for gruppe: " + groupId);
            
            Group Group = GetGroupFromId(groupId);
            Group.SwipeType = type;
            List<int> ingredientAllergies = new List<int>();
            List<int> foodGroupAllergies = new List<int>();

            foreach (var user in Group.Users)
            {
                if (!user.Username.Contains(" "))
                {
                    RegisteredUser RegUser = await GetUserInfo.GetUserAsync(user.Username);
                    Dictionary<int, string> allergyDict =
                        await GetUserInfo.getAllergyIngredientListAsync(RegUser.UserId);
                    foreach (var allergy in allergyDict)
                    {
                        if (!ingredientAllergies.Contains(allergy.Key))
                        {
                            ingredientAllergies.Add(allergy.Key);
                        }
                    }

                    Dictionary<int, string> foodgroupDict = await GetUserInfo.getAllergyFoodGroupListAsync(RegUser.UserId);
                    foreach (var allergy in foodgroupDict)
                    {
                        if (!foodGroupAllergies.Contains(allergy.Key))
                        {
                            foodGroupAllergies.Add(allergy.Key);
                        }
                    }
                }
            }
            
            string ingredientAllergiesString = string.Join(", ", ingredientAllergies);
            if (ingredientAllergiesString.Length == 0)
            {
                ingredientAllergiesString = "0";
            }
            
            string foodGroupAllergiesString = string.Join(", ", foodGroupAllergies);
            if (foodGroupAllergiesString.Length == 0)
            {
                foodGroupAllergiesString = "0";
            }

            if (type.Equals(Util.RECIPE))
            {
                Group.SwipeObject = await DataLink.GetShuffledRecipesAsync(ingredientAllergiesString, foodGroupAllergiesString);
            }

            if (type.Equals(Util.RESTAURANT))
            {
                Group.SwipeObject = await DataLink.GetShuffledRestaurantsAsync();
            }
        }

        public async Task<bool> DoneSwipingAsync(string groupId)
        {
            Group group = ActiveGroups.Find(g => g.Id.Equals(groupId));
            group.WaitingUsers++;

            return group.Users.Count == group.WaitingUsers;
        }

        public async Task<IList<CustomPair>> StopSwipeAsync(string groupId)
        {
            Group group = ActiveGroups.Find(g => g.Id.Equals(groupId));

            IList<CustomPair> result = new List<CustomPair>();

            foreach (Vote vote in group.Votes)
            {
                result.Add(
                    new CustomPair()
                    {
                        Key = vote.Votes,
                        Value = SwipeResultTitle(group, vote.SwipeObjectId)
                    });
            }

            return result.OrderByDescending(pair => pair.Key).Take(5).ToList();
        }

        public async Task<bool> CastVoteAsync(string groupId, int id)
        {
            Group group = ActiveGroups.Find(g => g.Id.Equals(groupId));

            if (group.Votes.Count != 0)
            {
                foreach (Vote vote in group.Votes)
                {
                    if (vote.SwipeObjectId == id)
                    {
                        vote.Votes++;
                        if (vote.Votes == group.Users.Count)
                        {
                            return true;
                        }

                        return false;
                    }
                }
            }

            group.Votes.Add(new Vote(id));

            return false;
        }

        private static Random random = new();

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string SwipeResultTitle(SharedLibrary.Models.Group group, int id)
        {
            foreach (CustomPair pair in group.SwipeObject)
            {
                if (pair.Key == id)
                {
                    return pair.Value;
                }
            }

            return null;
        }
    }
}