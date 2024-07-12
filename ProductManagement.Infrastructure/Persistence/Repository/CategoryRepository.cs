using Dapper;
using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.Data_Context;
using ProductManagement.Infrastructure.Persistence.Interfaces;

namespace ProductManagement.Infrastructure.Persistence.Repository;

 public class CategoryRepository : ICategoryRepository
    {
        private readonly MySqlDataContext _mySqlDataContext;
        private readonly PostreSqlDataContext _postgreSqlDataContext;

        public CategoryRepository(MySqlDataContext mySqlDataContext, PostreSqlDataContext postgreSqlDataContext)
        {
            _mySqlDataContext = mySqlDataContext;
            _postgreSqlDataContext = postgreSqlDataContext;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var query = "SELECT * FROM Categories WHERE Id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var mySqlConnection = _mySqlDataContext.CreateConnectionMySql())
            {
                var value = await mySqlConnection.QueryFirstOrDefaultAsync<Category>(query, parameters);
                return value;
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var query = "SELECT * FROM Categories";

            // MySQL
            using (var mySqlConnection = _mySqlDataContext.CreateConnectionMySql())
            {
                var values = await mySqlConnection.QueryAsync<Category>(query);
                return values;
            }
        }

        public async Task AddCategoryAsync(Category category)
        {
            var query = "INSERT INTO Categories (Name) VALUES (@name)";
            var query2 = "INSERT INTO public.\"Categories\" (\"Name\") VALUES (@name)";
            var parameters = new DynamicParameters();
            parameters.Add("@name", category.Name);

            using (var mySqlConnection = _mySqlDataContext.CreateConnectionMySql())
            {
                await mySqlConnection.ExecuteAsync(query, parameters);
            }

            
            using (var postreSqlConnection=_postgreSqlDataContext.CreateConnectionPostreSql())
            {
                await postreSqlConnection.ExecuteAsync(query2, parameters);
            }
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            var query = "UPDATE Categories SET Name = @name WHERE Id = @id";
            var query2 = "UPDATE public.\"Categories\" SET \"Name\"=@name WHERE \"Id\" =@id";
            var parameters = new DynamicParameters();
            parameters.Add("@name", category.Name);
            parameters.Add("@id", category.Id);

            using (var mySqlConnection = _mySqlDataContext.CreateConnectionMySql())
            {
                await mySqlConnection.ExecuteAsync(query, parameters);
            }
            using (var postreSqlConnection=_postgreSqlDataContext.CreateConnectionPostreSql())
            {
                await postreSqlConnection.ExecuteAsync(query2, parameters);
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var query = "DELETE FROM Categories WHERE Id = @id";
            var query2 = "DELETE FROM public.\"Categories\" WHERE \"Id\" = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var mySqlConnection = _mySqlDataContext.CreateConnectionMySql())
            {
                await mySqlConnection.ExecuteAsync(query, parameters);
            }
            using (var postreSqlConnection=_postgreSqlDataContext.CreateConnectionPostreSql())
            {
                await postreSqlConnection.ExecuteAsync(query2, parameters);
            }
        }
    }