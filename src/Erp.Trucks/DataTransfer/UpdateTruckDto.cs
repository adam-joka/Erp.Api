namespace Erp.Trucks.DataTransfer;

public class UpdateTruckDto
{
    public required Guid Uuid { get; init; }
    
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}