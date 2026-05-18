using SiLadhida.Core.Configs;

namespace SiLadhida.Core.Services
{
    public class ProdukLookupService
    {
        public string? GetNamaProduk(string kode)
        {
            if (DataProdukConfig.KodeProduk.ContainsKey(kode))
            {
                return DataProdukConfig.KodeProduk[kode].Nama;
            }

            return null;
        }
    }
}