using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Infrastructure.Data_Context;

public class PostreSqlDataDbContext:DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    public PostreSqlDataDbContext(DbContextOptions<PostreSqlDataDbContext> options):base(options)
    {
      
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>().ToTable("Products");
        modelBuilder.Entity<Category>().ToTable("Categories");
    }
}