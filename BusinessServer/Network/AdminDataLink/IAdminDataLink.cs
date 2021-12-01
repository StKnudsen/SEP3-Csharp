using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessServer.Network.AdminDataLink
{
    public interface IAdminDataLink
    {
        Task<bool> AddIngredientAsync(string ingredientName, int foodGroupId);
        Task<Dictionary<int, string >> GetFoodgroupListAsync();
        Task<Dictionary<int, string >> GetIngredientListAsync();
    }
}