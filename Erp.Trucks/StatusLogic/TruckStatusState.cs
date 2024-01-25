using Erp.Trucks.Enums;
using Stateless;

namespace Erp.Trucks.StatusLogic;

public class TruckStatusState
{
    enum Trigger { PutOutOfService, StartLoading, PutToJob, GoToJob, Return }

    private TruckStatus _status = TruckStatus.OutOfService;
    private readonly StateMachine<TruckStatus, Trigger> _machine;

    public TruckStatusState()
    {
        _machine = new StateMachine<TruckStatus, Trigger>(() => _status, s => _status = s);

        _machine.Configure(TruckStatus.OutOfService)
            .Permit(Trigger.StartLoading, TruckStatus.Loading)
            .Permit(Trigger.PutToJob, TruckStatus.ToJob)
            .Permit(Trigger.GoToJob, TruckStatus.AtJob)
            .Permit(Trigger.Return, TruckStatus.Returning);

        _machine.Configure(TruckStatus.Loading)
            .Permit(Trigger.PutOutOfService, TruckStatus.OutOfService)
            .Permit(Trigger.PutToJob, TruckStatus.ToJob);

        _machine.Configure(TruckStatus.ToJob)
            .Permit(Trigger.PutOutOfService, TruckStatus.OutOfService)
            .Permit(Trigger.GoToJob, TruckStatus.AtJob);
        
        _machine.Configure(TruckStatus.AtJob)
            .Permit(Trigger.PutOutOfService, TruckStatus.OutOfService)
            .Permit(Trigger.Return, TruckStatus.Returning);

    }

    public Task PutOutOfService() => _machine.FireAsync(Trigger.PutOutOfService);
    public Task PutOutOfService() => _machine.FireAsync(Trigger.PutOutOfService);
}