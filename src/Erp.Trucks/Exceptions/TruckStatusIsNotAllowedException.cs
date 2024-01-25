namespace Erp.Trucks.Exceptions;

public class TruckStatusIsNotAllowedException : Exception
{
    public TruckStatusIsNotAllowedException() : base($"Truck status is not allowed")
    {
        
    }
}