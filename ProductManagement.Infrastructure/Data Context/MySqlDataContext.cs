using System.Data;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace ProductManagement.Infrastructure.Data_Context;

public class MySqlDataContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public MySqlDataContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("MySQLConnection");
    }

    public IDbConnection CreateConnectionMySql()
    {
        return new MySqlConnection(_connectionString);
    }
    
}