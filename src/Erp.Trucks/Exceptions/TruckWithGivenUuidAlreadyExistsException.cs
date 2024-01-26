namespace Erp.Trucks.Exceptions;

public class TruckWithGivenUuidAlreadyExistsException(Guid uuid)
    : TruckValidationException($"Truck with provided uuid {uuid} already exists");