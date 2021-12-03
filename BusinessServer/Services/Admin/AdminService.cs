using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessServer.Network;
using BusinessServer.Network.AdminDataLink;
using SharedLibrary.Models;

namespace BusinessServer.Services.Admin
{
    public class AdminService : IAdminService
    {
        private Dictionary<int, string> ingredientList;
        private Dictionary<int, string> foodGroupList;
        private Dictionary<int, string> unitList;
        private Dictionary<int, string> recipeList;

        private readonly IAdminDataLink AdminDataLink;

        public AdminService()
        {
            AdminDataLink = new AdminDataLink();
            ingredientList = GetIngredientListAsync().Result;
            foodGroupList = GetFoodgroupListAsync().Result;
            unitList = GetUnitListAsync().Result;
            recipeList = GetRecipeListAsync().Result;
        }

        public async Task<bool> AddIngredientAsync(string ingredientName, int _foodGroupId)
        {
            if (ingredientList.ContainsValue(ingredientName.ToLower()))
            {
                throw new Exception("Ingrediens findes allerede i databasen");
            }

            await AdminDataLink.AddIngredientAsync(ingredientName, _foodGroupId);
            await GetIngredientListAsync();
            return true;
        }

        public async Task<bool> AddRecipeAsync(Recipe recipe)
        {
            if (recipeList.ContainsValue(recipe.Name.ToLower()))
            {
                throw new Exception("Opskrift findes allerede i databasen");
            }

            Console.WriteLine("AdminService er nået til AddRecipeAsync " + recipe.RecipeIngredient.Count);
            
            await AdminDataLink.AddRecipeAsync(recipe);
            await GetRecipeListAsync();
            return true;
        }

        public async Task<Dictionary<int, string>> GetIngredientListAsync()
        {
            return ingredientList = await AdminDataLink.GetIngredientListAsync();
        }

        private async Task<Dictionary<int,string>> GetRecipeListAsync()
        {
            return recipeList = await AdminDataLink.GetRecipeListAsync();
        }

        public async Task<Dictionary<int, string>> GetFoodgroupListAsync()
        {
            return foodGroupList = await AdminDataLink.getFoodgroupList();
        }

        public async Task<Dictionary<int, string>> GetUnitListAsync()
        {
            return unitList = await AdminDataLink.GetUnitListAsync();
        }

      
    }
}