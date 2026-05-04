using SiLadhida.Core.Enums;
using SiLadhida.Core.Entities;

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