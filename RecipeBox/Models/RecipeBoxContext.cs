using Microsoft.EntityFrameworkCore;

namespace RecipeBox.Models
{
    public class RecipeBoxContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ing> Ing { get; set; }
        public DbSet<RecipeIng> RecipeIngs { get; set; }
        public RecipeBoxContext(DbContextOptions options) : base(options) { }
    }
}