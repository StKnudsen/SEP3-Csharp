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
            ingredientList = GetIngredientListAsync().Result;
            GetFoodgroupListAsync();
        }

        public async Task AddIngredientAsync(string ingredientName, int _foodGroupId)
        {
            if (ingredientList.ContainsValue(ingredientName.ToLower()))
            {
                throw new Exception("Ingrediens findes allerede i databasen");
            }
            Console.WriteLine("Hi from AdminService: " + ingredientName + _foodGroupId);
            await AdminDataLink.AddIngredientAsync(ingredientName, _foodGroupId);
            await GetIngredientListAsync();
        }

        public async Task<Dictionary<int, string>> GetIngredientListAsync()
        {
          return ingredientList = await AdminDataLink.GetIngredientListAsync();
        }

        public async Task<Dictionary<int, string>> GetFoodgroupListAsync()
        {
           return foodGroupList = await AdminDataLink.GetFoodgroupListAsync();
        }
    }
}