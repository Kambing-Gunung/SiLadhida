namespace SiLadhida.Core.Enums
{
    public enum StateOrder
    {
        MenungguPembayaran, 
        SedangDipanggang, 
        SiapDiambil, 
        Selesai,
        Dibatalkan       
    }

    public enum StateTrigger
    {
        PembayaranDikonfirmasi,
        WaktuPembayaranHabis,
        SelesaiDipanggang,
        KueDiambilPelanggan,
        DibatalkanPelanggan
    }
}