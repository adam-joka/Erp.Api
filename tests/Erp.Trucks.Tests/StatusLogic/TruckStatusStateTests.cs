using Erp.Trucks.Enums;
using Erp.Trucks.StatusLogic;

namespace Erp.Trucks.Tests.StatusLogic;

public class TruckStatusStateTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Given_Loading_state_it_should_be_possible_to_move_to_OutOfService_state()
    {
        var truckStatusState = new TruckStatusState();
        truckStatusState.Load();
        truckStatusState.PutOutOfService();
        
        Assert.That(truckStatusState.Status, Is.EqualTo(TruckStatus.OutOfService));
        
    }
}