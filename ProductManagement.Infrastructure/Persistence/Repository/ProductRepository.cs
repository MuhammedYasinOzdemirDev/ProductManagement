using Dapper;
using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.Data_Context;
using ProductManagement.Infrastructure.Persistence.Interfaces;

namespace ProductManagement.Infrastructure.Persistence.Repository;

public class ProductRepository:IProductRepository
{
    private readonly MySqlDataContext _mySqlDataContext;
    private readonly PostreSqlDataContext _postreSqlDataContext;

    public ProductRepository(MySqlDataContext mySqlDataContext,PostreSqlDataContext postreSqlDataContext)
    {
        _mySqlDataContext = mySqlDataContext;
        _postreSqlDataContext = postreSqlDataContext;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        var query = "Select * from Products Where Id=@id";
        var parameters = new DynamicParameters();
        parameters.Add("@id",id);
        using (var mysqlConnection=_mySqlDataContext.CreateConnectionMySql())
        {
          var value=  await mysqlConnection.QueryFirstOrDefaultAsync<Product>(query, parameters);
          return value;
        }
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        var query = "select * from Products";
        
        //MySql
        using (var mySqlConnection=_mySqlDataContext.CreateConnectionMySql())
        {
           var values=await mySqlConnection.QueryAsync<Product>(query);
           return values;
        }
        
        /*PostreSql
        using (var postreSqlConnection=_postreSqlDataContext.CreateConnectionPostreSql())
        {
            var values=await postreSqlConnection.QueryAsync<Product>(query);
            return values;
        }*/
    }

    public async Task AddProductAsync(Product product)
    {
        var query =
            "Insert Into Products (Name,Description,Price,StockQuantity,CategoryId) VALUES (@name,@description,@price,@stockQuantity,@categoryId)";
        var parameters = new DynamicParameters();
        parameters.Add("@name",product.Name);
        parameters.Add("@description",product.Description);
        parameters.Add("@price",product.Price);
        parameters.Add("@stockQuantity",product.StockQuantity);
        parameters.Add("@categoryId",product.CategoryId);

        using (var mysqlConnection=_mySqlDataContext.CreateConnectionMySql())
        {
          await  mysqlConnection.ExecuteAsync(query, parameters);
        }
    }

    public async Task UpdateProductAsync(Product product)
    {
        var query =
            "Update From Products SET Name=@name,Description=@description,Price=@price,StockQuantity=@stockQuantity,CategoryId=@categoryId Where Id=@id";
        var parameters = new DynamicParameters();
        parameters.Add("@name",product.Name);
        parameters.Add("@description",product.Description);
        parameters.Add("@price",product.Price);
        parameters.Add("@stockQuantity",product.StockQuantity);
        parameters.Add("@categoryId",product.CategoryId);
        parameters.Add("@id",product.Id);

        using (var mysqlConnection=_mySqlDataContext.CreateConnectionMySql())
        {
            await  mysqlConnection.ExecuteAsync(query, parameters);
        }
    }

    public async Task DeleteProductAsync(int id)
    {
        var query =
            "Delete From Products Where Id=@id";
        var parameters = new DynamicParameters();
      parameters.Add("@id",id);

        using (var mysqlConnection=_mySqlDataContext.CreateConnectionMySql())
        {
            await  mysqlConnection.ExecuteAsync(query, parameters);
        }
    }
}