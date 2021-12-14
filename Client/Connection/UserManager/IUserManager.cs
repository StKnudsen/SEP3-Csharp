using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Connection.UserManager
{
    public interface IUserManager
    {
        //  For allergy registration
        Task<Dictionary<int, string>> GetAllergyFoodGroupListAsync(int userId);
        Task<Dictionary<int, string>> GetAllergyIngredientListAsync(int userId);
        Task<bool> SetUserAllergyFoodGroupAsync(int userId, int foodGroupId);
        Task<bool> SetUserAllergyIngredientAsync(int userId, int ingredient);
    }
}