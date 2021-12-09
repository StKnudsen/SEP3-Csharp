using System.Collections.Generic;

namespace SharedLibrary.Models
{
    public class Dish
    {
        public string Name { get; set; }
        public List<DishIngredient> DishIngredient { get; set; }

        public Dish()
        {
            DishIngredient = new List<DishIngredient>();
        }
    }
}