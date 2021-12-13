using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessServer.Services.Profile
{
    public interface IProfileService
    {
        public Task<Dictionary<int, string>> GetUserAllergyIngredient(int userId);
        public Task<Dictionary<int, string>> GetUserAllergyFoodGroup(int userId);
    }
}