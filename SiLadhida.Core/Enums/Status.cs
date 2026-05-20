namespace SiLadhida.Core.Enums
{
    public enum StateOrder
    {
        MenungguPembayaran, 
        SiapDiambil, 
        Selesai,
        Dibatalkan       
    }

    public enum StateTrigger
    {
        PembayaranDikonfirmasi,
        WaktuPembayaranHabis,
        KueDiambilPelanggan,
        DibatalkanPelanggan
    }
}