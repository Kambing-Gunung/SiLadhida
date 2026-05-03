using System;
using System.Collections.Generic;
using System.Text;

namespace SiLadhida.Core
{
    public enum  Status
    {
        PesananTelahDibayar,
        PesananDisiapkan,
        SiapDiambil,
        PesananSelesai
    }

    public class Pesanan
    {
        public int IdPesanan;
        public string NamaKue;
        public string NamaPemesan;
        public Status StatusSekarang;

        public Pesanan
            ( int id,
            string namaKue,
            string namaPemesan)

        {
            this.IdPesanan = id;
            this.NamaKue = namaKue;
            this.NamaPemesan = namaPemesan;
            this.StatusSekarang = Status.PesananTelahDibayar;
        }
        
    }

    public class NodePesanan
    {
        public Pesanan Data;
        public NodePesanan prev;
        public NodePesanan Next;

        public NodePesanan(Pesanan data)
        {
            this.Data = data;
            this.prev = null;
            this.Next = null;
        }
    }

    public class  StatusPesanan
    {
        private NodePesanan head;
        private NodePesanan tail;

        public StatusPesanan()
        {
            this.head = null;
            this.tail = null;
        }

        public void PesananBaru(int id, string namaKue, string namaPemesan)
        {
            Pesanan pesananBaru = new Pesanan(id, namaKue, namaPemesan);
            NodePesanan newNode = new NodePesanan(pesananBaru);

            if(this.head == null)
            {
                this.head = newNode;
                this.tail = newNode;
            }
            else
            {
                this.tail.Next = newNode;
                newNode.prev = this.tail;
                this.tail = newNode;
            }

            Console.WriteLine("Pesanan baru telah ditambahkam: " + namaKue + " oleh " + namaPemesan);
        }
        private NodePesanan CariNodePesanan(int id)
        {
            NodePesanan current = this.head;
            while (current != null)
            {
                if (current.Data.IdPesanan == id)
                {
                    return current;
                }
                current = current.Next;
            }
            return null;
        }
        public void UpdateStatusPesanan(int id, Status statusSekarang)
        {
            NodePesanan Cari = CariNodePesanan(id);
            if (Cari == null)
            {
                Console.WriteLine("ID Pesanan " + id.ToString() + "tidak ditemukan");
                return;
            }

            Status statusLama = Cari.Data.StatusSekarang;
            bool transisiValid = false;

            if (statusLama == Status.PesananTelahDibayar && statusSekarang == Status.PesananDisiapkan)
            {
                transisiValid = true;
            }
            else if (statusLama == Status.PesananDisiapkan && statusSekarang == Status.SiapDiambil)
            {
                transisiValid = true;
            }
            else if (statusLama == Status.SiapDiambil && statusSekarang == Status.PesananSelesai)
            {
                transisiValid = true;
            }
            if (transisiValid == true)
            {
                Cari.Data.StatusSekarang = statusSekarang;
                Console.WriteLine("Status pesanan " + Cari.Data.NamaKue + " berubah dari " + statusLama.ToString() + " menjadi " + statusSekarang.ToString() + ".");
            }
            else
            {
                Console.WriteLine("Gagal, Tidak bisa merubah status dari " + statusLama.ToString() + " langsung menjadi " + statusSekarang.ToString() + ".");
            }
        }
        public void DaftarPesanan()
        {
            if (this.head == null)
            {
                Console.WriteLine("Belum ada pesanan.");
                return;
            }

            Console.WriteLine(" Daftar Pesanan ");
            NodePesanan current = this.head;
            while (current != null)
            {
                Console.WriteLine("ID: " + current.Data.IdPesanan.ToString() + current.Data.NamaKue + "Status: " + current.Data.StatusSekarang.ToString());
                current = current.Next;
            }
        }
    }
}

