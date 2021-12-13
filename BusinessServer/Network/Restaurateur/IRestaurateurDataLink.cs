using System.Collections.Generic;
using System.Threading.Tasks;
using SharedLibrary.Models;
using SharedLibrary.Models.Restaurateur;

namespace BusinessServer.Network.Restaurateur
{
    public interface IRestaurateurDataLink
    {
        Task<bool> AddDishAsync(Dish dish);
        Task<List<Dish>> GetDishListAsync(int restaurantId);
        Task<List<Restaurant>> GetRestaurantsFromRestaurateurIdAsync(int restaurateurId);
    }
}