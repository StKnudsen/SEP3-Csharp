using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessServer.Network;
using BusinessServer.Network.AdminDataLink;
using BusinessServer.Network.DNNRDataLink;
using BusinessServer.Services.DNNRService;
using SharedLibrary.Models;
using SharedLibrary.Models.Recipe;
using SharedLibrary.Models.Restaurateur;

namespace BusinessServer.Services.Admin
{
    public class AdminService : IAdminService
    {
        
        private Dictionary<int, string> recipeList;
        private Dictionary<int, string> foodGroupList;
        private Dictionary<int, string> ingredientList;
        private Dictionary<int, string> unitList;
        private Dictionary<int, string> userList;
        private List<Restaurant> restaurantList;
        private List<Address> addressList;
        

        private readonly IAdminDataLink AdminDataLink;
        private readonly IDNNRService DnnrService;

        public AdminService()
        {
            AdminDataLink = new AdminDataLink();
            DnnrService = new DNNRService.DNNRService();
            unitList = GetUnitListAsync().Result;
            recipeList = GetRecipeListAsync().Result;
            restaurantList = GetRestaurantListAsync().Result;
            addressList = GetAddressListAsync().Result;
            ingredientList = DnnrService.GetIngredientListAsync().Result;
            foodGroupList = DnnrService.GetFoodgroupListAsync().Result;
            userList = GetUsersAndRestaurateurListAsync().Result;
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

        public async Task<bool> AddFoodGroupAsync(string foodGroupName)
        {
            return await AdminDataLink.AddFoodGroupAsync(foodGroupName);
        }

        public async Task<bool> AddRecipeAsync(Recipe recipe)
        {
            if (recipeList.ContainsValue(recipe.Name.ToLower()))
            {
                throw new Exception("Opskrift findes allerede i databasen");
            }
            
            await AdminDataLink.AddRecipeAsync(recipe);
            await GetRecipeListAsync();
            return true;
        }

        public async Task<bool> AddRestaurantAsync(Restaurant restaurant)
        {
            Console.WriteLine("AdminService i AddRestaurantAsync");

            if (restaurantList.Contains(restaurant))
            {
                throw new Exception("Restaurant findes allerede i databasen");
            }
            
            //TODO: Det her check virker ikke. Pointen er, at en restaurant ikke kan oprettes på en addresse, der allerede eksisterer i databasen.
            if (addressList.Contains(restaurant.Address))
            {
                throw new Exception("Den angivne addresse er allerede brugt i databasen");
            }

            await AdminDataLink.AddRestaurantAsync(restaurant);
            await GetRestaurantListAsync();
            await GetAddressListAsync();
            return true;
        }

        public async Task<Dictionary<int, string>> GetIngredientListAsync()
        {
            return ingredientList = await DnnrService.GetIngredientListAsync();
        }

        private async Task<Dictionary<int,string>> GetRecipeListAsync()
        {
            return recipeList = await AdminDataLink.GetRecipeListAsync();
        }


        public async Task<Dictionary<int, string>> GetUnitListAsync()
        {
            return unitList = await AdminDataLink.GetUnitListAsync();
        }

        public async Task<List<Restaurant>> GetRestaurantListAsync()
        {
            return restaurantList = await AdminDataLink.GetRestaurantListAsync();
        }   
        public async Task<List<Address>> GetAddressListAsync()
        {
            return addressList = await AdminDataLink.GetAddressListAsync();
        }

        public async Task<Address> GetAddressByIdAsync(int addressId)
        {
            return await AdminDataLink.GetAddressByIdAsync(addressId);
        }

        public async Task<Dictionary<int, string>> GetUsersAndRestaurateurListAsync()
        {
            return userList = await AdminDataLink.GetUsersAndRestaurateurListAsync();
        }
    }
}