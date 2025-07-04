using Microsoft.EntityFrameworkCore;
using SupplyManager.Data;

namespace SupplyManager.Infrastructure;

public static class Migrator
{
    public static void Migrate(this IServiceCollection services)
    {
        using var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
    }
}