using System.Collections.Generic;
using System.Threading.Tasks;
using SharedLibrary.Models;
using SharedLibrary.Models.Restaurateur;

namespace BusinessServer.Services.Restaurateur
{
    public interface IRestaurateurService
    {
        public Task<bool> AddDishAsync(Dish dish);
        public Task<List<Dish>> GetDishListAsync(int restaurantId);
        public Task<List<Restaurant>> GetRestaurantsFromRestaurateurIdAsync(int restaurateurId);
    }
}