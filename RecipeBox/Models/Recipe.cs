using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBox.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public string Directions { get; set; }
        public int Rating { get; set; }
        public List<RecipeIng> JoinEntities { get; }
        public ApplicationUser User { get; set; }

    }
}