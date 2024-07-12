using System.Data;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Npgsql;

namespace ProductManagement.Infrastructure.Data_Context;

public class PostreSqlDataContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public PostreSqlDataContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("PostgreSQLConnection");
    }

    public IDbConnection CreateConnectionMySql()
    {
        return new NpgsqlConnection(_connectionString);
    }

}