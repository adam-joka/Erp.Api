using Erp.Trucks.DataAccess;
using Erp.Trucks.DataTransfer;
using Erp.Trucks.Entities;
using Erp.Trucks.Enums;
using Erp.Trucks.Exceptions;
using Erp.Trucks.StatusLogic;
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
        Truck truckEntity = await TryGetTruckEntityAsync(uuid);

        return new TruckDto
        {
            Uuid = truckEntity.Uuid,
            Name = truckEntity.Name,
            Code = truckEntity.Code,
            Description = truckEntity.Description,
            Status = truckEntity.Status
        };
    }

    public async Task<Guid> AddTruckAsync(CreateTruckDto truck)
    {
        // uncomment this when real db is used
        // await using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();
        
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
            Status = TruckStatus.OutOfService
        };

        await _dbContext.Trucks.AddAsync(truckEntity);
        
        await _dbContext.SaveChangesAsync();
        
        // uncomment this when real db is used
        //await transaction.CommitAsync();

        return truck.Uuid;
    }

    public async Task<Guid> UpdateTruckAsync(UpdateTruckDto truck)
    {
        // uncomment this when real db is used
        // await using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();
        
        Truck truckEntity = await TryGetTruckEntityAsync(truck.Uuid);

        truckEntity.Name = truck.Name;
        truckEntity.Code = truck.Code;
        truckEntity.Description = truck.Description;

        await _dbContext.SaveChangesAsync();
        
        // uncomment this when real db is used
        // await transaction.CommitAsync();

        return truck.Uuid;
    }

    public async Task DeleteTruckAsync(Guid uuid)
    {
        Truck truckEntity = await TryGetTruckEntityAsync(uuid);
        
        _dbContext.Trucks.Remove(truckEntity);
        
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task PutOutOfServiceAsync (Guid uuid)
    {
        Truck truckEntity = await TryGetTruckEntityAsync(uuid);
        
        var truckStatusState = new TruckStatusState(truckEntity.Status);

        GuardAgainstInvalidState(() => truckStatusState.PutOutOfService(), uuid, TruckStatus.OutOfService);
      
        truckEntity.Status = truckStatusState.Status;
        
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task StartLoadingAsync(Guid uuid)
    {
        Truck truckEntity = await TryGetTruckEntityAsync(uuid);
        
        var truckStatusState = new TruckStatusState(truckEntity.Status);

        GuardAgainstInvalidState(() => truckStatusState.StartLoading(), uuid, TruckStatus.Loading);

        truckEntity.Status = truckStatusState.Status;
        
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task PutToJobAsync(Guid uuid)
    {
        Truck truckEntity = await TryGetTruckEntityAsync(uuid);
        
        var truckStatusState = new TruckStatusState(truckEntity.Status);

        GuardAgainstInvalidState(() => truckStatusState.PutToJob(), uuid, TruckStatus.ToJob);

        truckEntity.Status = truckStatusState.Status;
        
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task GoToJobAsync(Guid uuid)
    {
        Truck truckEntity = await TryGetTruckEntityAsync(uuid);
        
        var truckStatusState = new TruckStatusState(truckEntity.Status);

        GuardAgainstInvalidState(() => truckStatusState.GoToJob(), uuid, TruckStatus.AtJob);

        truckEntity.Status = truckStatusState.Status;
        
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task Return(Guid uuid)
    {
        Truck truckEntity = await TryGetTruckEntityAsync(uuid);
        
        var truckStatusState = new TruckStatusState(truckEntity.Status);

        GuardAgainstInvalidState(() => truckStatusState.Return(), uuid, TruckStatus.Returning);

        truckEntity.Status = truckStatusState.Status;
        
        await _dbContext.SaveChangesAsync();
    }
    
    private void GuardAgainstInvalidState(Action action, Guid uuid, TruckStatus status)
    {
        try
        {
            action();
        }
        catch (InvalidOperationException)
        {
            throw new TruckStatusIsNotAllowedException(uuid, status);
        }
    }
    
    
    private async Task<Truck> TryGetTruckEntityAsync(Guid uuid)
    {
        Truck? truckEntity = await _dbContext.Trucks.FirstOrDefaultAsync(t => t.Uuid == uuid);
        if (truckEntity == null)
        {
            throw new TruckWithGivenUuidNotFoundException(uuid);
        }

        return truckEntity;
    }
}