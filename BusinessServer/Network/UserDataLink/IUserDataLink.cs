using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessServer.Models;
using SharedLibrary.Models;
using SharedLibrary.Models.User;

namespace BusinessServer.Network.UserDataLink
{
    public interface IUserDataLink
    {
        public Task<RegisteredUser> GetUserAsync(string username);
        public Task<ColourAnimalCount> GetColourAnimalCountAsync();
        public Task<GuestUser> GetGuestUserAsync(int ColourId, int AnimalId);
        public Task<bool> CreateUserAsync(RegisteredUser user);

        //  For allergy registration
        Task<Dictionary<int, string>> getAllergyFoodGroupListAsync(int userId);
        Task<Dictionary<int, string>> getAllergyIngredientListAsync(int userId);
        Task<bool> SetUserAllergyFoodGroupAsync(int userId, int foodGroupId);
        Task<bool> SetUserAllergyIngredientAsync(int userId, int ingredient);
    }
}