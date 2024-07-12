using Microsoft.EntityFrameworkCore;
using ProductManagement.Infrastructure.Data_Context;
using ProductManagement.Infrastructure.Persistence.Interfaces;
using ProductManagement.Infrastructure.Persistence.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add builder.Services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// MySQL DbContext
builder.Services.AddDbContext<MySqlDataDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySQLConnection"), new MySqlServerVersion(new Version(8, 0, 21)), b => b.MigrationsAssembly("ProductManagement.Infrastructure")));
            
// PostgreSQL DbContext
builder.Services.AddDbContext<PostreSqlDataDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection"), b => b.MigrationsAssembly("ProductManagement.Infrastructure")));

// Dapper Contexts
builder.Services.AddSingleton<MySqlDataContext>();
builder.Services.AddSingleton<PostreSqlDataContext>();

//Repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};



app.Run();

