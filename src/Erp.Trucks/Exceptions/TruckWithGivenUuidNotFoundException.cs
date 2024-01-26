namespace Erp.Trucks.Exceptions;

public class TruckWithGivenUuidNotFoundException(Guid uuid)
    : TruckValidationException($"Truck with given uuid {uuid} not found");