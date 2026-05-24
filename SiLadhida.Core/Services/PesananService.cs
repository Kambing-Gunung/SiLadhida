using SiLadhida.Core.Enums;
using SiLadhida.Core.Entities;
using SiLadhida.Core.StateMachines;

namespace SiLadhida.Core.Services
{
    public class PesananService
    {
        private readonly PesananStateMachine _machine;

        public PesananService()
        {
            _machine = new PesananStateMachine();
        }

        public bool IsValidTransition(StateOrder current, StateTrigger trigger)
        {
            return _machine.CanTransition(current, trigger);
        }

        public StateOrder GetInitialStatus()
        {
            return StateOrder.MenungguPembayaran;
        }

        public StateOrder GetNextState(StateOrder current, StateTrigger trigger)
        {
            return _machine.GetNextState(current, trigger);
        }

        public decimal HitungTotal(List<OrderItem> items)
        {
            decimal total = 0;

            foreach (var item in items)
            {
                total += item.Harga * item.Quantity;
            }

            return total;
        }
    }
}