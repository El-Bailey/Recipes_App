using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recipes_App.Models;

namespace Recipes_App.Data
{
    public class Recipes_AppContext : DbContext
    {
        public Recipes_AppContext(DbContextOptions<Recipes_AppContext> options)
            : base(options)
        {
        }

        public DbSet<Recipes_App.Models.RecipeViewModel> RecipeViewModel { get; set; }
    }
}