using System;

namespace SiLadhida.Core.Validators;

public static class OrderValidator
{
    public static void ValidateNamaPemesan(string nama)
    {
        if (string.IsNullOrWhiteSpace(nama))
            throw new ArgumentException("Nama pemesan tidak boleh kosong.");
    }
}