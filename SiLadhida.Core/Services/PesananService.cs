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

        public bool IsValidTransition(Status current, Status next)
        {
            return _machine.CanTransition(current, next);
        }

        public Status GetInitialStatus()
        {
            return Status.PesananTelahDibayar;
        }

        public int HitungTotal(List<OrderItem> items)
        {
            int total = 0;

            foreach (var item in items)
            {
                total += item.Harga * item.Quantity;
            }

            return total;
        }
    }
}