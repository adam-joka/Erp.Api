using Erp.Trucks.DataAccess;
using Erp.Trucks.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Erp.Trucks;

public static class Extensions
{
    public static void AddTrucksModule(this IServiceCollection services)
    {
        services.AddDbContext<TrucksDbContext>(options => options.UseInMemoryDatabase("Trucks"));

        services.AddScoped<TruckService>();
    }
    
    public static void UseTrucksModuleEndpoints(this WebApplication app)
    {
        app.MapTrucksEndpoints();
    }
}