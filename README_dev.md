# ☕ SiLadhida

**SiLadhida** adalah aplikasi manajemen kedai/kafe berbasis **.NET & Avalonia UI** yang dirancang untuk mengelola produk, pesanan, dan transaksi secara terstruktur menggunakan pendekatan **layered architecture**.

---

## 🚀 Fitur Utama

* 📦 Manajemen Produk
* 🧾 Manajemen Pesanan (Order)
* 💳 Proses Pembayaran
* 📊 Dashboard Ringkasan
* 🧮 Riwayat Transaksi
* 🎯 Status Order berbasis State Machine

---

## 🧱 Arsitektur Project

Project ini menggunakan pendekatan **Layered Architecture** dengan pembagian sebagai berikut:

```
SiLadhida
│
├── SiLadhida.App           → UI (Avalonia)
├── SiLadhida.API           → Web API (Controller)
├── SiLadhida.Application   → Business Logic
├── SiLadhida.Core          → Domain & Entity
├── SiLadhida.Infrastructure → Data Access & External Service
```

---

## 🔄 Alur Data

```
UI (App)
   ↓
App Service (HTTP Client)
   ↓
API Controller
   ↓
Application Service
   ↓
Domain (Core)
   ↓
Database (Infrastructure)
```

---

## 🧠 Design Pattern yang Digunakan

* MVVM (Model-View-ViewModel)
* Service Layer Pattern
* State Machine Pattern (Order Status)
* Command Pattern (RelayCommand)
* Separation of Concerns

---

## ⚙️ Teknologi

* .NET 8
* Avalonia UI
* CommunityToolkit.Mvvm
* REST API
* JSON Serialization

---

## 📌 Status Order

Menggunakan State Machine:

```
MenungguPembayaran → SiapDiambil → Selesai
                 ↘ Dibatalkan
```

---

## 📷 Tampilan Utama

* Dashboard
* Order Management
* Product Management
* Transaction History

---

## 🛠️ Cara Menjalankan

### 1. Jalankan API

```bash
cd SiLadhida.API
dotnet run
```

### 2. Jalankan App

```bash
cd SiLadhida.App
dotnet run
```

---

## 📦 Struktur Folder (Ringkas)

```
Features/
    Order/
    Product/
    Transaction/
Components/
    Dialogs/
    Inputs/
Services/
Core/
```

---

## 🎯 Tujuan Project

Project ini dibuat untuk:

* Menerapkan konsep Clean Architecture
* Membangun aplikasi desktop modern
* Memahami pemisahan antara UI, Business Logic, dan Domain
* Menjadi portfolio aplikasi real-world

---

## 🚧 Pengembangan Selanjutnya

* 📈 Dashboard Chart
* 🔄 Real-time Update
* 🧠 State Management Improvement
* 📱 Cross-platform expansion

---

## 📄 License

Project ini digunakan untuk pembelajaran dan pengembangan pribadi.
