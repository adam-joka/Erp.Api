namespace Erp.Trucks.DataTransfer;

public class TruckDto
{
    public required Guid Uuid { get; init; }
    
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Status { get; set; }
}