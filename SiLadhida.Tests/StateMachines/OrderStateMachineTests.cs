using SiLadhida.Core.Enums;
using SiLadhida.Core.StateMachines;

namespace SiLadhida.Tests.StateMachines;

public class OrderStateMachineTests
{
    [Fact]
    public void PaymentConfirmed_ShouldGoToSiapDiambil()
    {
        var next =
            OrderStateMachine.GetNext(
                StateOrder.MenungguPembayaran,
                StateTrigger.PembayaranDikonfirmasi);

        Assert.Equal(
            StateOrder.SiapDiambil,
            next);
    }

    [Fact]
    public void CustomerCancelled_ShouldGoToDibatalkan()
    {
        var next =
            OrderStateMachine.GetNext(
                StateOrder.MenungguPembayaran,
                StateTrigger.DibatalkanPelanggan);

        Assert.Equal(
            StateOrder.Dibatalkan,
            next);
    }

    [Fact]
    public void InvalidTransition_ShouldThrow()
    {
        Assert.Throws<InvalidOperationException>(
            () => OrderStateMachine.GetNext(
                StateOrder.Selesai,
                StateTrigger.PembayaranDikonfirmasi));
    }
}