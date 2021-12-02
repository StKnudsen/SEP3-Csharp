using System.Collections.Generic;

namespace SharedLibrary.Models
{
    public class Recipe
    {
        public string Name { get; set; }
        public List<RecipeIngredient> RecipeIngredient { get; set; }
    }
}