using LumeeraCare.Models;
using Microsoft.EntityFrameworkCore;

namespace LumeeraCare.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Customer> Customers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Books" },
            new Category { Id = 2, Name = "Electronics" },
            new Category { Id = 3, Name = "Home" });

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Angular in Action", Price = 39.99m, CategoryId = 1 },
            new Product { Id = 2, Name = "Clean Code", Price = 42.50m, CategoryId = 1 },
            new Product { Id = 3, Name = "Wireless Headphones", Price = 89.00m, CategoryId = 2 },
            new Product { Id = 4, Name = "USB-C Hub", Price = 29.99m, CategoryId = 2 },
            new Product { Id = 5, Name = "Desk Lamp", Price = 24.95m, CategoryId = 3 });

        // Optional: enforce unique email for customers
        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.Email)
            .IsUnique();
    }
}
