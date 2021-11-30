using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessServer.Network;
using BusinessServer.Network.AdminDataLink;

namespace BusinessServer.Services.Admin
{
    public class AdminService : IAdminService
    {
        private Dictionary<int, string> ingredientList;
        private Dictionary<int, string> foodGroupList;

        private readonly IAdminDataLink AdminDataLink;

        public AdminService()
        {
            AdminDataLink = new AdminDataLink();
            GetIngredientListAsync();
            GetFoodgroupListAsync();
        }

        public async Task AddIngredientAsync(string ingredientName, int _foodGroupId)
        {
            if (ingredientList.ContainsValue(ingredientName.ToLower()))
            {
                throw new Exception("Ingrediens findes allerede i databasen");
            }

            await AdminDataLink.AddIngredientAsync(ingredientName, _foodGroupId);
            await GetIngredientListAsync();
        }

        public async Task GetIngredientListAsync()
        {
           ingredientList = await AdminDataLink.GetIngredientListAsync();
        }

        public async Task GetFoodgroupListAsync()
        {
           foodGroupList = await AdminDataLink.GetFoodgroupListAsync();
        }
    }
}