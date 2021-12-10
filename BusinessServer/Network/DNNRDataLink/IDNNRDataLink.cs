using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessServer.Network.DNNRDataLink
{
    public interface IDNNRDataLink
    {
        Task<Dictionary<int, string>> GetFoodgroupListAsync();
        Task<Dictionary<int, string>> GetIngredientListAsync();
    }
}