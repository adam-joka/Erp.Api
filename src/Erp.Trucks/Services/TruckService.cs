using Erp.Trucks.DataAccess;
using Erp.Trucks.DataTransfer;
using Erp.Trucks.Entities;
using Erp.Trucks.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Erp.Trucks.Services;

public class TruckService
{
    private readonly TrucksDbContext _dbContext;

    public TruckService(TrucksDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<TruckDto>> GetTrucksAsync() =>
        _dbContext.Trucks.Select(t => new TruckDto
        {
            Uuid = t.Uuid,
            Name = t.Name,
            Code = t.Code,
            Description = t.Description,
            Status = t.Status
        }).ToListAsync();

    public async Task<TruckDto> GetTruckAsync(Guid uuid)
    {
        TruckDto? truck = await _dbContext.Trucks.Select(t => new TruckDto
        {
            Uuid = t.Uuid,
            Name = t.Name,
            Code = t.Code,
            Description = t.Description,
            Status = t.Status
        }).FirstOrDefaultAsync(t => t.Uuid == uuid);
        
        if (truck == null)
        {
            throw new TruckWithGivenUuidNotFoundException(uuid);
        }

        return truck;
    }

    public async Task<Guid> AddTruckAsync(TruckDto truck)
    {
        await using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();
        if(await _dbContext.Trucks.AnyAsync(t => t.Uuid == truck.Uuid))
        {
            throw new TruckWithGivenUuidAlreadyExistsException(truck.Uuid);
        }
        
        var truckEntity = new Truck
        {
            Uuid = truck.Uuid,
            Name = truck.Name,
            Code = truck.Code,
            Description = truck.Description,
            Status = truck.Status,
        };

        await _dbContext.Trucks.AddAsync(truckEntity);
        
        await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();

        return truck.Uuid;
    }

    public async Task<Guid> UpdateTruckAsync(TruckDto truck)
    {
        await using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();
        
        Truck? truckEntity = await _dbContext.Trucks.FirstOrDefaultAsync(t => t.Uuid == truck.Uuid);
        if (truckEntity == null)
        {
            throw new TruckWithGivenUuidNotFoundException(truck.Uuid);
        }

        truckEntity.Name = truck.Name;
        truckEntity.Code = truck.Code;
        truckEntity.Description = truck.Description;
        truckEntity.Status = truck.Status;

        await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();

        return truck.Uuid;
    }
}