using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Client.Connection.Administration
{
    public interface IAdministration
    {
        public Task AddIngredientAsync(string ingredientName, IList<FoodGroup> _foodGroups );
    }
}