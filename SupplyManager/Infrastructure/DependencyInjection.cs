using Microsoft.EntityFrameworkCore;
using SupplyManager.Business;
using SupplyManager.Data;
using SupplyManager.Data.Repositories;

namespace SupplyManager.Infrastructure;

public static class DependencyInjection
{
    public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("Default connection string not found");
            opt.UseNpgsql(connectionString);
        });
        services.AddScoped<ProductService>();
        services.AddScoped<ProductRepository>();
    }
}