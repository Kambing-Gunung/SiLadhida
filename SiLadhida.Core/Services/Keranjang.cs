using System;
using System.Collections.Generic;
using System.Text;

namespace SiLadhida.Core.Services
{
    public class Cart<T> 
    {
        private List<T> OrderList = new List<T>();

        public void AddOrder(T order)
        {
            // Pesanan tidak boleh null
            if (order == null)
                throw new ArgumentNullException(nameof(order), "Gagal : Objek pesanan tidak boleh null.");

            int initialCount = OrderList.Count;

            OrderList.Add(order);
        }

        public void ResetOrderList()
        {
            OrderList.Clear();

            // Menjamin keranjang benar-benar bersih
            if (OrderList.Count != 0)
                throw new Exception("Gagal : Keranjang gagal dikosongkan.");
        }
    }
}