using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SupplyManager.Business;
using SupplyManager.Data;
using SupplyManager.Model;
using SupplyManager.Model.DTO;

namespace IntegrationTest.Infra;

public class IntegrationTestsFixture<TProgram> : IDisposable where TProgram : class
{
    private CustomWebApplicationFactory Factory { get; }
    private HttpClient Client { get; set; }
    
    public ProductService ProductService => Factory.Services.GetRequiredService<ProductService>();
    public ShopService ShopService => Factory.Services.GetRequiredService<ShopService>();
    
    public IntegrationTestsFixture()
    {
        Factory = new CustomWebApplicationFactory();
        Client = Factory.CreateClient();
        CleanUpDbData();
    }
    
    public void Dispose()
    {
        RollbackDbMigrations();
        Factory.Dispose();
        Client.Dispose();
    }
    
    public void RollbackDbMigrations()
    {
        using (var scope = Factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate("0");
        }
    }

    public void CleanUpDbData()
    {
        using (var scope = Factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var tableNames = dbContext.Model.GetEntityTypes()
                .Select(t => t.GetTableName())
                .Where(name => !string.IsNullOrEmpty(name))
                .Distinct();

            foreach (var tableName in tableNames)
            {
                dbContext.Database.ExecuteSqlRaw($"DELETE FROM \"{tableName}\"");
            }
        }
    }

    public Task<HttpResponseMessage> GetProductsAsync()
    {
        return Client.GetAsync("/Product");
    }
    
    public Task<HttpResponseMessage> SaveProductAsync(AddProductRequestDto request)
    {
        return Client.PostAsJsonAsync("/Product", request);
    }

    public async Task<Product> AddProductAsync(AddProductRequestDto request)
    {
        return await ProductService.SaveNewProductAsync(request);
    }
    
    public async Task<ProductStock> UpdateProductStockAsync(UpdateProductStockRequestDto request)
    {
        return await ProductService.UpdateProductStockAsync(request);
    }
    
    
    public Task<HttpResponseMessage> ShopRequestAsync(ShoppingCartRequestDto request)
    {
        return Client.PostAsJsonAsync("/Shop", request);
    }
}