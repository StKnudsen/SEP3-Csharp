using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Connection.Profile
{
    public interface IProfile
    {
        Task<Dictionary<int, string>> GetUserAllergyFoodGroup(int userId);
        Task<Dictionary<int, string>> GetUserAllergyIngredient(int userId);
    }
}