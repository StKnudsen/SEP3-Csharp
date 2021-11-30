using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessServer.Network;

namespace BusinessServer.Services.Admin
{
    public class AdminService : IAdminService
    {
        private Dictionary<int, string> ingredientList;
        private Dictionary<int, string> foodGroupList;

        private readonly IDataLink DataLink;

        public AdminService()
        {
            DataLink = new DataLink();
            GetIngredientListAsync();
            GetFoodgroupListAsync();
        }

        public async Task AddIngredientAsync(string ingredientName, int _foodGroupId)
        {
            if (ingredientList.ContainsValue(ingredientName.ToLower()))
            {
                throw new Exception("Ingrediens findes allerede i databasen");
            }

            await DataLink.AddIngredientAsync(ingredientName, _foodGroupId);
            await GetIngredientListAsync();
        }

        public async Task GetIngredientListAsync()
        {
            DataLink.GetIngredientListAsync();
        }

        public async Task GetFoodgroupListAsync()
        {
            DataLink.GetFoodgroupListAsync();
        }
    }
}