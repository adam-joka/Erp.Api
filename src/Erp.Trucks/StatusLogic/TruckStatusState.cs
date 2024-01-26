using Erp.Trucks.Enums;
using Stateless;

namespace Erp.Trucks.StatusLogic;

public class TruckStatusState
{
    enum Trigger { PutOutOfService, Load, PutToJob, GoToJob, Return }

    private TruckStatus _status;
    private readonly StateMachine<TruckStatus, Trigger> _machine;

    public TruckStatusState(TruckStatus status)
    {
        _status = status;
        
        _machine = new StateMachine<TruckStatus, Trigger>(() => _status, s => _status = s);

        _machine.Configure(TruckStatus.OutOfService)
            .Permit(Trigger.Load, TruckStatus.Loading)
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
        
        _machine.Configure(TruckStatus.Returning)
            .Permit(Trigger.PutOutOfService, TruckStatus.OutOfService)
            .Permit(Trigger.Load, TruckStatus.Loading);
    }

    public void PutOutOfService()
    {
        if (_status != TruckStatus.OutOfService)
        {
            _machine.Fire(Trigger.PutOutOfService);
        }
    }

    
    public void StartLoading()
    {
        if (_status != TruckStatus.Loading)
        {
            _machine.Fire(Trigger.Load);
        }
    }

    public void PutToJob()
    {
        if (_status != TruckStatus.ToJob)
        {
            _machine.Fire(Trigger.PutToJob);
        }
    }

    public void GoToJob()
    {
        if (_status != TruckStatus.AtJob)
        {
            _machine.Fire(Trigger.GoToJob);
        }
    }

    public void Return()
    {
        if (_status != TruckStatus.Returning)
        {
            _machine.Fire(Trigger.Return);
        }
    }

    public TruckStatus Status => _status;
}