using Erp.Trucks.Enums;

namespace Erp.Trucks.DataTransfer;

public class TruckDto
{
    public required Guid Uuid { get; init; }
    
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required TruckStatus Status { get; set; } = TruckStatus.OutOfService;
}