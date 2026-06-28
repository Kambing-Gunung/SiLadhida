# 🧠 SiLadhida.Core

**SiLadhida.Core** adalah inti dari seluruh sistem yang berisi **domain utama**, termasuk entity, enum, dan aturan bisnis (business rules) yang bersifat independen dari UI maupun infrastruktur.

---

## 🎯 Tujuan Layer Core

Layer ini bertanggung jawab untuk:

* Mendefinisikan **struktur data utama (Entity)**
* Menyimpan **aturan bisnis inti**
* Menjadi **single source of truth** untuk domain
* Tidak bergantung pada layer lain

---

## 🧱 Isi Utama

```id="q1xg9s"
SiLadhida.Core
│
├── Entities/
│   ├── Order.cs
│   ├── OrderItem.cs
│   └── Product.cs
│
├── Enums/
│   ├── StateOrder.cs
│   └── StateTrigger.cs
│
└── StateMachines/
    └── OrderStateMachine.cs
```

---

## 📦 Entity

### 🔹 Order

Mewakili satu transaksi pesanan.

```id="5p3l0k"
- Id
- NamaPemesan
- Status
- Items
- TotalHarga
```

---

### 🔹 OrderItem

Item dalam sebuah pesanan.

```id="0ik9l7"
- ProductId
- Quantity
- Harga
- SubTotal
```

---

### 🔹 Product

Data produk yang dijual.

```id="d3yx1t"
- Id
- Nama
- Harga
- Stock
```

---

## 🔁 State Machine (Order)

Menggunakan pendekatan **State Machine Pattern** untuk mengatur alur status pesanan.

### 🔹 StateOrder

```id="z7b6yl"
MenungguPembayaran
SiapDiambil
Selesai
Dibatalkan
```

---

### 🔹 StateTrigger

```id="yiv2p2"
PembayaranDikonfirmasi
WaktuPembayaranHabis
KueDiambilPelanggan
DibatalkanPelanggan
```

---

### 🔹 Transisi

```id="m1pbz9"
MenungguPembayaran + PembayaranDikonfirmasi → SiapDiambil
MenungguPembayaran + DibatalkanPelanggan → Dibatalkan
SiapDiambil + KueDiambilPelanggan → Selesai
```

---

## ⚙️ OrderStateMachine

Class ini mengatur validasi transisi status.

### Contoh:

```csharp id="0dbdwl"
OrderStateMachine.CanTransition(current, trigger);
OrderStateMachine.GetNext(current, trigger);
```

---

## 🧠 Prinsip Desain

Layer ini mengikuti prinsip:

```id="n2c9s3"
✔ Pure Domain (tanpa dependency luar)
✔ Business Rule terpusat
✔ Tidak bergantung UI / API
✔ Reusable
```

---

## ❌ Yang TIDAK BOLEH ADA di Core

```id="7l2v8g"
❌ HttpClient
❌ Database access
❌ UI logic (IsEnabled, Button state, dll)
❌ Dependency ke App / API
```

---

## ✅ Yang BOLEH ADA

```id="9m4u3h"
✔ Entity
✔ Value Object
✔ Enum
✔ Business Rule
✔ State Machine
```

---

## 🔗 Relasi dengan Layer Lain

```id="jv9tq8"
Core ← Application ← API ← App
```

Core menjadi fondasi dari seluruh sistem.

---

## 🎯 Peran dalam Arsitektur

Core adalah:

```id="r4lf3y"
🧠 pusat logika bisnis
📦 representasi data utama
🔒 penjaga konsistensi sistem
```

---

## 🚧 Pengembangan Selanjutnya

* Validasi domain yang lebih ketat
* Value Object (Money, Quantity, dll)
* Domain Event
* Domain Service

---
