using System.Collections.Generic;

namespace SharedLibrary.Models.Recipe
{
    public class Recipe
    {
        public string Name { get; set; }
        public List<RecipeIngredient> RecipeIngredient { get; set; }

        public Recipe()
        {
            RecipeIngredient = new List<RecipeIngredient>();
        }
    }
}