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

            // Menjamin jumlah elemen bertambah tepat satu
            if (daftarPesanan.Count != initialCount + 1)
                throw new Exception("Gagal : Item gagal ditambahkan ke list.");
        }

        public List<T> PrintDaftarPesanan()
        {
            // Menjamin tidak mengembalikan null meskipun kosong
            return daftarPesanan ?? new List<T>();
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
