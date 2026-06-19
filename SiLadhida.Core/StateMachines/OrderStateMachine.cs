using SiLadhida.Core.Enums;

namespace SiLadhida.Core.StateMachines
{
    public static class OrderStateMachine
    {
        private static readonly Dictionary<(StateOrder, StateTrigger), StateOrder> _transitions = new()
        {
            { (StateOrder.MenungguPembayaran, StateTrigger.PembayaranDikonfirmasi), StateOrder.SiapDiambil },
            { (StateOrder.MenungguPembayaran, StateTrigger.DibatalkanPelanggan), StateOrder.Dibatalkan },
            { (StateOrder.SiapDiambil, StateTrigger.KueDiambilPelanggan), StateOrder.Selesai }
        };

        public static StateOrder GetNext(StateOrder current, StateTrigger trigger)
        {
            if (!_transitions.TryGetValue((current, trigger), out var next))
                throw new InvalidOperationException("Transisi tidak valid");

            return next;
        }

        public static bool CanTransition(StateOrder current, StateTrigger trigger)
            => _transitions.ContainsKey((current, trigger));
    }
}