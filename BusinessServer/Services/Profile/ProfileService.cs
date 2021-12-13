using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessServer.Services.Profile
{
    public class ProfileService : IProfileService
    {
        public Task<Dictionary<int, string>> GetUserAllergyIngredient(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Dictionary<int, string>> GetUserAllergyFoodGroup(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}