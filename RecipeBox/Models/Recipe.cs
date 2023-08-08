using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBox.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        [Required(ErrorMessage = "A name is required")]
        public string RecipeName { get; set; }
        [Required(ErrorMessage = "Directions are required")]
        public string Directions { get; set; }
        [Required(ErrorMessage = "A Rating is required")]
        public int Rating { get; set; }
        public List<RecipeIng> JoinEntities { get; }
        public ApplicationUser User { get; set; }

    }
}