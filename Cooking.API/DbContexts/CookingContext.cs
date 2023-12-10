using Cooking.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cooking.API.DbContexts
{
    public class CookingContext : DbContext
    {
        public DbSet<Cookware> Cookwares { get; set; }

        public CookingContext(DbContextOptions<CookingContext> options) : base(options) 
        {
            
        }
    }
}
