namespace Erp.Trucks.Exceptions;

public class TruckWithGivenUuidAlreadyExistsException : Exception
{
    public TruckWithGivenUuidAlreadyExistsException(Guid uuid) : base(
        $"Truck with provided uuid {uuid} already exists")
    {

    }
}