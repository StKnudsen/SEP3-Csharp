using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Connection.DNNR
{
    public interface ISharedDNNR
    {
        Task<Dictionary<int, string>> GetFoodgroupListAsync();
        Task<Dictionary<int, string>> GetIngredientListAsync();
    }
}