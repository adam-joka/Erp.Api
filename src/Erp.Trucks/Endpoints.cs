using Erp.Trucks.DataTransfer;
using Erp.Trucks.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Erp.Trucks;

public static class Endpoints
{
    public static void MapTrucksEndpoints(this WebApplication app)
    {
        app.MapGet("/trucks", async ([FromServices] TruckService truckService) =>
        {
            var trucks = await truckService.GetTrucksAsync();
            return Results.Ok(trucks);
        });
        
        app.MapGet("/trucks/{uuid}", async (Guid uuid, [FromServices] TruckService truckService) =>
        {
            var trucks = await truckService.GetTruckAsync(uuid);
            return Results.Ok(trucks);
        });
        
        
        app.MapPost("/trucks", async ([FromBody] TruckDto truck , [FromServices] TruckService truckService) =>
        {
            Guid uuid = await truckService.AddTruckAsync(truck);
            return Results.Ok(uuid);
        });
        
        
        app.MapPut("/trucks", async ([FromBody] TruckDto truck , [FromServices] TruckService truckService) =>
        {
            Guid uuid = await truckService.UpdateTruckAsync(truck);
            return Results.Ok(uuid);
        });
    }
}