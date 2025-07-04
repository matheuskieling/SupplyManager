using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SupplyManager.Data;
using SupplyManager.Model.DTO;

namespace IntegrationTest.Infra;

public class IntegrationTestsFixture<TProgram> : IDisposable where TProgram : class
{
    private CustomWebApplicationFactory Factory { get; }
    private HttpClient Client { get; set; }
    
    public IntegrationTestsFixture()
    {
        Factory = new CustomWebApplicationFactory();
        Client = Factory.CreateClient();
    }
    
    public void Dispose()
    {
        CleanUpDb();
        Factory.Dispose();
        Client.Dispose();
    }
    
    public void CleanUpDb()
    {
        using (var scope = Factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate("0");
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

}