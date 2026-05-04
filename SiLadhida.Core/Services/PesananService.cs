using SiLadhida.Core.Enums;

namespace SiLadhida.Core.Services
{
    public class PesananService
    {
        public bool IsValidTransition(Status current, Status next)
        {
            return (current, next) switch
            {
                (Status.PesananTelahDibayar, Status.PesananDisiapkan) => true,
                (Status.PesananDisiapkan, Status.SiapDiambil) => true,
                (Status.SiapDiambil, Status.PesananSelesai) => true,
                _ => false
            };
        }

        public Status GetInitialStatus()
        {
            return Status.PesananTelahDibayar;
        }
    }
}