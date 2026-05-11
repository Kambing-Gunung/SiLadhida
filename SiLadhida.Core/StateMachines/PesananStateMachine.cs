using SiLadhida.Core.Enums;

namespace SiLadhida.Core.StateMachines
{
    public class PesananStateMachine
    {
        private readonly Dictionary<Status, List<Status>> _transitions;

        public PesananStateMachine()
        {
            _transitions = new Dictionary<Status, List<Status>>
            {
                {
                    Status.PesananTelahDibayar,
                    new List<Status>
                    {
                        Status.PesananDisiapkan
                    }
                },

                {
                    Status.PesananDisiapkan,
                    new List<Status>
                    {
                        Status.SiapDiambil
                    }
                },

                {
                    Status.SiapDiambil,
                    new List<Status>
                    {
                        Status.PesananSelesai
                    }
                },

                {
                    Status.PesananSelesai,
                    new List<Status>()
                }
            };
        }

        public bool CanTransition(Status current, Status next)
        {
            if (!_transitions.ContainsKey(current))
                return false;

            return _transitions[current].Contains(next);
        }
    }
}