using Erp.Trucks.Entities;
using Microsoft.EntityFrameworkCore;

namespace Erp.Trucks.DataAccess;

public class TrucksDbContext : DbContext
{
    public required DbSet<Truck> Trucks { get; set; }
}