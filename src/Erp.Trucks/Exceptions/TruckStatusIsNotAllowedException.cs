using Erp.Trucks.Enums;

namespace Erp.Trucks.Exceptions;

public class TruckStatusIsNotAllowedException : Exception
{
    public TruckStatusIsNotAllowedException(Guid uuid, TruckStatus status) : base($"Truck {uuid} status {status} is not allowed")
    {
        
    }
}