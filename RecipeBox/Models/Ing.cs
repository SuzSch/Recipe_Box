using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBox.Models
{
    public class Ing 
    {
        public int IngId { get; set;}
        public string Name { get; set;}
        public List<RecipeIng> JoinEntities { get; }
    }
}