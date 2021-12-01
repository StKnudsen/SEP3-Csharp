using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Client.Connection.Administration
{
    public interface IAdministration
    {
        public Task AddIngredientAsync(string ingredientName, int _foodGroupId );
        Task<Dictionary<int, string>> getFoodgroupListAsync();
    }
}