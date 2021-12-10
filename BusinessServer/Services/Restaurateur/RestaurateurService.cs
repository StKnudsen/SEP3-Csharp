using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessServer.Network.Restaurateur;
using SharedLibrary.Models;
using SharedLibrary.Models.Restaurateur;

namespace BusinessServer.Services.Restaurateur
{
    public class RestaurateurService : IRestaurateurService
    {

        private readonly IRestaurateurDataLink restaurateurDataLink;
        public RestaurateurService()
        {
            restaurateurDataLink = new RestaurateurDataLink();
        }

        public async Task<bool> AddDishAsync(Dish dish, int restaurantId)
        {
            Console.WriteLine("Nåede til RestaurateurService");
            await restaurateurDataLink.AddDishAsync(dish,restaurantId);
            await GetDishListAsync(dish.RestaurantId);
            return true;
        }

        public async Task<List<Dish>> GetDishListAsync(int restaurantId)
        {
            
            return await restaurateurDataLink.GetDishListAsync(restaurantId);
        }

        public async Task<List<Restaurant>> GetRestaurantsFromRestaurateurIdAsync(int restaurateurId)
        {
            return await restaurateurDataLink.GetRestaurantsFromRestaurateurIdAsync(restaurateurId);
        }
    }
}