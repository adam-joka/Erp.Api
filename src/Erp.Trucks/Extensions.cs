using Erp.Trucks.DataAccess;
using Erp.Trucks.DataTransfer;
using Erp.Trucks.Infrastructure.Middleware;
using Erp.Trucks.Services;
using Erp.Trucks.Validators;
using FluentValidation;
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
        services.AddScoped<IValidator<CreateTruckDto>, CreateTruckDtoValidator>();
        services.AddScoped<IValidator<UpdateTruckDto>, UpdateTruckDtoValidator>();
    }
    
    public static void UseTrucksModuleEndpoints(this WebApplication app)
    {
        app.UseMiddleware<GlobalExceptionMiddleware>();
        app.MapTrucksEndpoints();
    }
}