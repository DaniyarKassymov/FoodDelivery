using FoodDelivery.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Database;

public class DatabaseContext : IdentityDbContext<User>
{
    public required DbSet<Restaurant> Restaurants { get; set; }
    public required DbSet<Food> Foods { get; set; }
    public required DbSet<Basket> Baskets { get; set; }
    public required DbSet<Order> Orders { get; set; }
    
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Restaurant>()
            .HasMany(r => r.Foods)
            .WithOne(f => f.Restaurant)
            .HasForeignKey(r => r.RestaurantId);

        builder.Entity<Restaurant>()
            .HasMany(r => r.Baskets)
            .WithOne(b => b.Restaurant)
            .HasForeignKey(b => b.RestaurantId);

        builder.Entity<User>()
            .HasMany(u => u.Baskets)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId);

        builder.Entity<Basket>()
            .HasMany(b => b.Orders)
            .WithOne(o => o.Basket)
            .HasForeignKey(o => o.BasketId);

        builder.Entity<Food>()
            .HasMany(f => f.Orders)
            .WithOne(o => o.Food)
            .HasForeignKey(o => o.FoodId);
        
        base.OnModelCreating(builder);
    }
}