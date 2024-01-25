﻿using Erp.Trucks.Entities;
using Microsoft.EntityFrameworkCore;

namespace Erp.Trucks.DataAccess;

public class TrucksDbContext : DbContext
{
    public required DbSet<Truck> Trucks { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}