using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessServer.Models;
using SharedLibrary.Models;
using SharedLibrary.Models.User;

namespace BusinessServer.Network.Group
{
    public interface IGroupDataLink
    {
        Task<List<CustomPair>> GetShuffledRecipesAsync( string ingredientAllergies, string foodGroupAllergies);
        Task<List<CustomPair>> GetShuffledRestaurantsAsync();
    }
}