# SiLadhida

Solusi `SiLadhida` adalah sebuah aplikasi .NET 8 yang terdiri dari:
- `SiLadhida.API`: backend REST API dengan ASP.NET Core, Entity Framework Core, dan JWT authentication.
- `SiLadhida.App`: aplikasi desktop UI menggunakan Avalonia.
- `SiLadhida.Core`: library domain bersama untuk entitas, enums, dan layanan umum.
- `SiLadhida.Tests`: proyek unit test.

## Struktur Proyek

### Root
- `SiLadhida.slnx`: solusi utama yang menggabungkan semua proyek.
- `README.md`: dokumentasi ringkas dan gambaran arsitektur.

### SiLadhida.API
- `Program.cs`: entry point aplikasi API.
- `appsettings.json` / `appsettings.Development.json`: konfigurasi aplikasi dan koneksi database.
- `Auth/`: model user dan layanan autentikasi.
- `Common/`: kelas utilitas untuk respons API umum.
- `Controllers/`: endpoint HTTP untuk autentikasi, produk, order, lookup, dan lain-lain.
- `Data/`: `AppDbContext` dan konfigurasi Entity Framework Core.
- `DTOs/`: objek transfer data untuk input dan output API.
- `Extensions/`: ekstensi untuk servis, middleware, dan otentikasi.
- `Mappings/`: profil AutoMapper untuk konversi antar model dan DTO.
- `Middleware/`: middleware custom, termasuk penanganan exception.
- `Repositories/`: implementasi dan interface repositori untuk akses data.
- `Services/`: logika bisnis dan layanan aplikasi.
- `Validators/`: validasi input menggunakan FluentValidation.
- `Migrations/`: migrasi database Entity Framework Core.

### SiLadhida.App
- `App.axaml` / `App.axaml.cs`: inisialisasi aplikasi Avalonia.
- `MainWindow.axaml` / `MainWindow.axaml.cs`: jendela utama aplikasi.
- `Assets/`: gambar, ikon, dan aset statis.
- `Components/`: komponen UI kembali pakai seperti kartu, sidebar, topbar.
- `Controls/`: kontrol khusus untuk UI.
- `Helpers/`: utilitas client-side dan helper UI.
- `Layouts/`: tata letak atau kontrol layout.
- `Models/`: model data lokal untuk UI dan mapping ke API.
- `Resources/`: sumber daya aplikasi seperti ikon atau file data.
- `Services/`: layanan komunikasi API dan logika panggilan HTTP.
- `Styles/`, `Themes/`: definisi tema, styling, dan resource Avalonia.
- `ViewModels/`: pola MVVM untuk data binding dan logika tampilan.
- `Views/`: halaman dan tampilan UI.

### SiLadhida.Core
- `SiLadhida.Core.csproj`: library shared untuk semua proyek.
- `Configs/`: konfigurasi umum yang dibagikan.
- `Entities/`: kelas entitas domain.
- `Enums/`: tipe enum bersama.
- `Services/`: layanan domain reusable.
- `StateMachines/`: logika status atau state machine jika digunakan.

### SiLadhida.Tests
- `SiLadhida.Tests.csproj`: proyek unit test.
- `UnitTest1.cs`: contoh tes unit.
- `Services/`: tes untuk layanan aplikasi.

## Cara Menjalankan

1. Buka solusi `SiLadhida.slnx` di Visual Studio atau JetBrains Rider.
2. Jalankan `SiLadhida.API` untuk memulai backend.
3. Jalankan `SiLadhida.App` untuk membuka aplikasi desktop.
4. Gunakan `dotnet test` pada proyek `SiLadhida.Tests` untuk menjalankan unit test.

## Catatan

- Output build diabaikan (`bin/` dan `obj/`).
- Struktur folder mengikuti pemisahan tanggung jawab antara API, UI, shared core, dan test.
