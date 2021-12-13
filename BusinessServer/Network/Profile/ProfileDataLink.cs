using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessServer.Network.Profile
{
    public class ProfileDataLink : IProfileDataLink
    {
        public Task<Dictionary<int, string>> GetUserAllergyIngredientAsync(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Dictionary<int, string>> GetUserAllergyFoodGroupAsync(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}