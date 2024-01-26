using Erp.Trucks.DataTransfer;
using Erp.Trucks.Services;
using FluentValidation;
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
            List<TruckDto> trucks = await truckService.GetTrucksAsync();
            return Results.Ok(trucks);
        }).WithName("GetTrucks")
        .Produces<List<TruckDto>>();
        
        app.MapGet("/trucks/{uuid}", async (Guid uuid, [FromServices] TruckService truckService) =>
        {
            TruckDto trucks = await truckService.GetTruckAsync(uuid);
            return Results.Ok(trucks);
        }).WithName("GetTruckById")
        .Produces<TruckDto>()
        .Produces(StatusCodes.Status404NotFound);
        
        app.MapPost("/trucks", async (
                [FromBody] CreateTruckDto truck, 
                [FromServices] TruckService truckService,
                [FromServices] IValidator<CreateTruckDto> createTruckValidator) =>
        {
            var validationResult = await createTruckValidator.ValidateAsync(truck);
            if (!validationResult.IsValid) {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }
            
            Guid uuid = await truckService.AddTruckAsync(truck);
            return Results.Ok(uuid);
        }).WithName("AddTruck")
        .Produces<Guid>()
        .Produces(StatusCodes.Status404NotFound);
        
        app.MapPut("/trucks", async (
                [FromBody] UpdateTruckDto truck, 
                [FromServices] TruckService truckService,
                [FromServices] IValidator<UpdateTruckDto> updateTruckValidator) =>
        {
            var validationResult = await updateTruckValidator.ValidateAsync(truck);
            if (!validationResult.IsValid) {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }
            
            Guid uuid = await truckService.UpdateTruckAsync(truck);
            return Results.Ok(uuid);
        })
        .WithName("UpdateTruck")
        .Produces<Guid>()
        .Produces(StatusCodes.Status404NotFound);
        
        app.MapDelete("/trucks/{uuid}", async (Guid uuid, [FromServices] TruckService truckService) =>
        {
            await truckService.DeleteTruckAsync(uuid);
            return Results.Ok();
        })
        .WithName("DeleteTruck")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
        
        app.MapPut("/trucks/{uuid}/put-out-of-order", async (Guid uuid, [FromServices] TruckService truckService) =>
        {
            await truckService.PutOutOfServiceAsync(uuid);
            return Results.Ok();
        })
        .WithName("PutOutOfOrder")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
        
        app.MapPut("/trucks/{uuid}/start-loading", async (Guid uuid, [FromServices] TruckService truckService) =>
        {
            await truckService.StartLoadingAsync(uuid);
            return Results.Ok();
        })
        .WithName("StartLoading")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
        
        app.MapPut("/trucks/{uuid}/put-to-job", async (Guid uuid, [FromServices] TruckService truckService) =>
        {
            await truckService.PutToJobAsync(uuid);
            return Results.Ok();
        })
        .WithName("PutToJob")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
        
        app.MapPut("/trucks/{uuid}/go-to-job", async (Guid uuid, [FromServices] TruckService truckService) =>
        {
            await truckService.GoToJobAsync(uuid);
            return Results.Ok();
        })
        .WithName("GoToJob")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
        
        app.MapPut("/trucks/{uuid}/return", async (Guid uuid, [FromServices] TruckService truckService) =>
        {
            await truckService.Return(uuid);
            return Results.Ok();
        })
        .WithName("Return")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
    }
}