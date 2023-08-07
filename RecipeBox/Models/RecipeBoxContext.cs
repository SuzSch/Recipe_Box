using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RecipeBox.Models
{
    public class RecipeBoxContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ing> Ing { get; set; }
        public DbSet<RecipeIng> RecipeIngs { get; set; }
        public RecipeBoxContext(DbContextOptions options) : base(options) { }
    }
}