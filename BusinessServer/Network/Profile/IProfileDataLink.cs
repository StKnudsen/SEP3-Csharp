using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessServer.Network.Profile
{
    public interface IProfileDataLink
    {
        Task<Dictionary<int, string>> GetUserAllergyIngredientAsync(int userId);
        Task<Dictionary<int, string>> GetUserAllergyFoodGroupAsync(int userId);
    }
}