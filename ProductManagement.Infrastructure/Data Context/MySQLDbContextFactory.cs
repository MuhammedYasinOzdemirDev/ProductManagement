using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ProductManagement.Infrastructure.Data_Context;

public class MySQLDbContextFactory : IDesignTimeDbContextFactory<MySqlDataDbContext>
{
    public MySqlDataDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<MySqlDataDbContext>();
        var connectionString = configuration.GetConnectionString("MySQLConnection");
        builder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)));

        return new MySqlDataDbContext(builder.Options);
    }
}