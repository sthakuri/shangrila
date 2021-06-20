using Microsoft.EntityFrameworkCore;
using shangrila.Models;

namespace shangrila.Data
{
    public class ShangrilaContext : DbContext
    {
        public ShangrilaContext(DbContextOptions<ShangrilaContext> options) : base(options)
        {
        }

        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<ServiceHour> ServiceHours { get; set; }
        public DbSet<DishMenu> DishMenus { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceHour>().ToTable("ServiceHour");
            modelBuilder.Entity<DishMenu>().ToTable("DishMenu");
            modelBuilder.Entity<Dish>().ToTable("Dish");
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}