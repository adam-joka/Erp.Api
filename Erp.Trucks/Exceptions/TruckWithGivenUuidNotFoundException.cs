namespace Erp.Trucks.Exceptions;

public class TruckWithGivenUuidNotFoundException : Exception
{
    public TruckWithGivenUuidNotFoundException(Guid uuid) : base($"Truck with given uuid {uuid} not found")
    {
        
    }
}