using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBox.Models
{
    public class RecipeIng 
    {
        public int RecipeIngId {get; set;}
        public int RecipeId { get; set;}
        public Recipe Recipe { get; set;}
        public int IngId { get; set;}
        public Ing Ing { get; set; }
    }
}