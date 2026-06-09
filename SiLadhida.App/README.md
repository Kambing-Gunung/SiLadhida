# SiLadhida.App

Aplikasi desktop UI untuk SiLadhida, dibangun dengan Avalonia dan pola MVVM.

Proyek ini menyajikan tampilan, interaksi, dan komunikasi dengan backend API.

## Konten Utama

- `App.axaml` / `App.axaml.cs`
  - Inisialisasi aplikasi Avalonia dan konfigurasi global.
- `MainWindow.axaml` / `MainWindow.axaml.cs`
  - Jendela utama aplikasi.
- `Assets/`
  - Gambar, ikon, font, dan aset statis lainnya.
- `Components/`
  - Komponen UI yang dapat dipakai ulang, seperti kartu, sidebar, dan topbar.
- `Controls/`
  - Kontrol custom yang mendukung tampilan atau input khusus.
- `Helpers/`
  - Utilitas client-side, formatter, atau helper UI.
- `Layouts/`
  - Struktur tata letak dan container khusus.
- `Models/`
  - Model data lokal untuk binding UI dan request/response API.
- `Resources/`
  - Resource Avalonia seperti style, templates, atau aset internal.
- `Services/`
  - Layanan untuk memanggil API, mengelola sesi, dan logika aplikasi.
- `Styles/` dan `Themes/`
  - Definisi tema, styling, dan resource visual aplikasi.
- `ViewModels/`
  - Logika presentasi dan binding data untuk tampilan.
- `Views/`
  - Halaman dan tampilan UI yang dipetakan ke `ViewModels`.

## Struktur Umum

Folder ini dirancang untuk memisahkan:
- UI dan view (`Views`, `Components`, `Controls`)
- state / presentasi (`ViewModels`)
- data model (`Models`)
- styling (`Styles`, `Themes`, `Resources`)
- akses layanan (`Services`)

Build outputs (`bin/` dan `obj/`) diabaikan melalui `.gitignore`.
