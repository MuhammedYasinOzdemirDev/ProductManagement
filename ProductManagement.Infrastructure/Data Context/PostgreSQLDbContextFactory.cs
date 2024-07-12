using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ProductManagement.Infrastructure.Data_Context;

public class PostgreSQLDbContextFactory : IDesignTimeDbContextFactory<PostreSqlDataDbContext>
{
    public PostreSqlDataDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<PostreSqlDataDbContext>();
        var connectionString = configuration.GetConnectionString("PostgreSQLConnection");
        builder.UseNpgsql(connectionString);

        return new PostreSqlDataDbContext(builder.Options);
    }
}