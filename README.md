# ☕ SiLadhida

## 📌 Overview

**SiLadhida** adalah aplikasi manajemen kedai kopi yang mendukung:

* Manajemen produk
* Pemesanan (order)
* Transaksi kasir
* Role-based access (Admin & Kasir)

Project ini dibangun menggunakan arsitektur **Clean Architecture** dengan pemisahan layer yang jelas.

---

## 🏗️ Architecture

```
SiLadhida
│
├── SiLadhida.Core          → Domain & Business Rules
├── SiLadhida.Application   → Use Cases / Services
├── SiLadhida.Infrastructure → Database & Repository
└── SiLadhida.API           → HTTP API (Entry Point)
```

### 🔄 Flow

```
Client → API → Application → Core → Infrastructure → Database
```

---

## 🧱 Tech Stack

### Backend

* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* MySQL (Pomelo)

### Auth

* JWT Authentication
* Role-based Authorization

### Validation & Mapping

* FluentValidation
* AutoMapper

### UI (Desktop)

* Avalonia UI

---

## 🔐 Roles

| Role  | Akses                                 |
| ----- | ------------------------------------- |
| Admin | Full access (CRUD produk, order, dll) |
| Kasir | Order & transaksi saja                |

---

## 🚀 Features

### ✅ Product

* Create, Read, Update, Delete
* Validasi domain

### ✅ Order

* Create order
* Add item
* Update quantity
* State machine:

  * MenungguPembayaran
  * SiapDiambil
  * Selesai
  * Dibatalkan

### ✅ Auth

* Login JWT
* Role-based access

### ✅ Error Handling

* Global Exception Middleware
* Standard API Response

---

## 📦 API Response Format

```json
{
  "success": true,
  "message": "string",
  "data": {}
}
```

---

## ⚙️ Setup & Run

### 1. Clone project

```bash
git clone <https://github.com/Kambing-Gunung/SiLadhida>
```

### 2. Setup database

* Pastikan MySQL aktif
* Update connection string di:

```bash
SiLadhida.API/appsettings.json
```

---

### 3. Migration

```bash
dotnet ef database update \
--project SiLadhida.Infrastructure \
--startup-project SiLadhida.API
```

---

### 4. Run API

```bash
cd SiLadhida.API
dotnet run
```

API akan berjalan di:

```
http://localhost:5135
```

---

## 🔑 Default User (Seeder)

| Username | Password | Role  |
| -------- | -------- | ----- |
| admin    | admin123 | Admin |
| kasir    | kasir123 | Kasir |

---

## 🧠 Design Principles

* Clean Architecture
* Separation of Concerns
* Domain-driven design (basic)
* Dependency Inversion

---

## 📌 Notes

* Business logic hanya ada di **Core & Application**
* API hanya sebagai **entry point**
* Infrastructure bisa diganti tanpa ubah domain
