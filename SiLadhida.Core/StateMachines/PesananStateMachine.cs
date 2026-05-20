


using SiLadhida.Core.Enums;

namespace SiLadhida.Core.StateMachines
{
    public class PesananStateMachine
    {
        private readonly Dictionary<(StateOrder, StateTrigger), StateOrder> _transitions;

        public PesananStateMachine()
        {

            _transitions = new Dictionary<(StateOrder, StateTrigger), StateOrder>
            {

                {
                    (StateOrder.MenungguPembayaran, StateTrigger.WaktuPembayaranHabis),
                    StateOrder.Dibatalkan
                },

                { (StateOrder.MenungguPembayaran, StateTrigger.DibatalkanPelanggan),
                    StateOrder.Dibatalkan
                },
                {
                    (StateOrder.MenungguPembayaran, StateTrigger.PembayaranDikonfirmasi),
                    StateOrder.SiapDiambil
                },

      

                { (StateOrder.SiapDiambil, StateTrigger.KueDiambilPelanggan),
                    StateOrder.Selesai }
            };
        }

        public bool CanTransition(StateOrder current, StateTrigger trigger)
        {
            return _transitions.ContainsKey((current, trigger));
        }

        public StateOrder GetNextState(StateOrder current, StateTrigger trigger)
        {
            if (_transitions.TryGetValue((current, trigger), out StateOrder nextState))
            {
                return nextState;
            }

            throw new InvalidOperationException($"Transisi tidak valid! ");
        }
    }
}