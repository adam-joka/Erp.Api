using Erp.Trucks.Enums;

namespace Erp.Trucks.Exceptions;

public class TruckStatusIsNotAllowedException(Guid uuid, TruckStatus status)
    : TruckValidationException($"Truck {uuid} status {status} is not allowed");