using System.Collections.Generic;
using System.Threading.Tasks;
using SharedLibrary.Models.User;

namespace BusinessServer.Services.UserService
{
    public interface IGetUserInfo
    {
        // Interface segregation på user-ting
        Task<RegisteredUser> GetUserAsync(string username);
        Task<Dictionary<int, string>> getAllergyFoodGroupListAsync(int userId);
        Task<Dictionary<int, string>> getAllergyIngredientListAsync(int userId);
    }
}