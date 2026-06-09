using SiLadhida.Core.Configs;

namespace SiLadhida.Core.Services
{
    public class ProductLookupService
    {
        public string? GetProductNama(string kode)
        {
            if (DataProductConfig.KodeProduk.ContainsKey(kode))
            {
                return DataProductConfig.KodeProduk[kode].Nama;
            }

            return null;
        }
    }
}