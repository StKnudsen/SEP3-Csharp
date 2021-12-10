using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessServer.Services.Restaurateur;
using Microsoft.AspNetCore.SignalR;
using SharedLibrary.Models;
using SharedLibrary.Models.Restaurateur;

namespace BusinessServer.Hubs
{
    public class RestaurateurHub : Hub
    {
        private readonly IRestaurateurService restaurateurService;


        public RestaurateurHub()
        {
            restaurateurService = new RestaurateurService();
        }

        public async Task<bool> AddDishAsync(Dish dish,  int restaurantId)
        {
            try
            {
                Console.WriteLine("Nåede til RestaurateurHub");

                return await restaurateurService.AddDishAsync(dish,restaurantId);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }

        public async Task<List<Dish>> GetDishListAsync(int restaurantId)
        {
            try
            {
                return await restaurateurService.GetDishListAsync(restaurantId);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }
        
        public async Task<List<Restaurant>> GetRestaurantsFromRestaurateurIdAsync(int restaurateurId)
        {
            try
            {
                return await restaurateurService.GetRestaurantsFromRestaurateurIdAsync(restaurateurId);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }

    }
}