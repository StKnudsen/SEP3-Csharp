using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessServer.Models;
using SharedLibrary.Models;

namespace BusinessServer.Services
{
    public interface IUserService
    {
        Task<bool> ValidateUserAsync(string username, string password);
        Task<GuestUser> GetGuestUserAsync();
        Task<RegisteredUser> GetUserAsync(string username);
        
        //  For allergy registration
        Task<Dictionary<int, string>> getAllergyFoodGroupListAsync(int userId);
        Task<Dictionary<int, string>> getAllergyIngredientListAsync(int userId);
        Task<bool> SetUserAllergyFoodGroupAsync(int userId, int foodGroupId);
        Task<bool> SetUserAllergyIngredientAsync(int userId, int ingredient);
    }
}