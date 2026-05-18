using System;
using System.Collections.Generic;
using System.Text;

namespace SiLadhida.Core
{
    public class Keranjang<T> 
    {
        private List<T> daftarPesanan = new List<T>();

        public void TambahPesanan(T pesanan)
        {
            // Pesanan tidak boleh null
            if (pesanan == null)
                throw new ArgumentNullException(nameof(pesanan), "Gagal : Objek pesanan tidak boleh null.");

            int initialCount = daftarPesanan.Count;

            daftarPesanan.Add(pesanan);
        }

        public void ResetDaftarPesanan()
        {
            daftarPesanan.Clear();

            // Menjamin keranjang benar-benar bersih
            if (daftarPesanan.Count != 0)
                throw new Exception("Gagal : Keranjang gagal dikosongkan.");
        }
    }
}