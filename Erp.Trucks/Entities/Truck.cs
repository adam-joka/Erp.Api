namespace Erp.Trucks.Entities;

public class Truck
{
    public Guid Uuid { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public Guid StatusUuid { get; set; }
    public required TruckStatus Status { get; set; }
}