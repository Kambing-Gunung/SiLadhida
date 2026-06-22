# ⚙️ SiLadhida.Application

**SiLadhida.Application** adalah layer yang bertanggung jawab untuk menjalankan **logika bisnis aplikasi** dengan memanfaatkan domain dari `SiLadhida.Core`.

Layer ini menjadi penghubung antara **API (Controller)** dan **Domain (Core)**.

---

## 🎯 Tujuan Layer Application

Layer ini bertugas untuk:

* Menjalankan **use case aplikasi**
* Mengatur alur bisnis (business flow)
* Menghubungkan Domain dengan Infrastruktur
* Menjadi tempat implementasi logic operasional

---

## 🧱 Struktur Umum

```id="1s6gqf"
SiLadhida.Application
│
├── Interfaces/
│   └── IOrderService.cs
│
├── Services/
│   └── OrderService.cs
│
├── DTOs/ (optional / future)
│
└── Validators/ (optional)
```

---

## 🔧 Application Service

### 🔹 OrderService

Service utama yang menangani seluruh operasi terkait order:

```id="4t2mx2"
- Create Order
- Add Item
- Update Quantity
- Update Status
- Get Order
- Get All Orders
```

---

## 🧠 Peran Application Service

Application Service bertanggung jawab untuk:

```id="3k4k8c"
✔ Menjalankan use case
✔ Validasi input
✔ Mengatur flow bisnis
✔ Memanggil Domain (Core)
✔ Berinteraksi dengan Repository
```

---

## 🔄 Contoh Flow

### 🔹 Update Status Order

```id="i2evkg"
Controller → Application Service → Domain → Save
```

Penjelasan:

1. Controller menerima request
2. Application Service:

   * Validasi trigger
   * Cek apakah transisi valid (via StateMachine)
3. Domain menentukan state baru
4. Data disimpan

---

## 🔗 Hubungan dengan Layer Lain

```id="n5p4c8"
API → Application → Core
                 ↓
           Infrastructure
```

---

## 📌 Contoh Method

```csharp id="j3z5gc"
public async Task<Order> UpdateStatusAsync(int orderId, StateTrigger trigger)
{
    var order = await _repository.GetByIdAsync(orderId);

    if (!OrderStateMachine.CanTransition(order.Status, trigger))
        throw new Exception("Transisi tidak valid");

    var newState = OrderStateMachine.GetNext(order.Status, trigger);

    order.UpdateStatus(newState);

    await _repository.UpdateAsync(order);

    return order;
}
```

---

## ⚠️ Batasan Layer Application

Layer ini **tidak boleh**:

```id="1m6v1d"
❌ Mengandung UI logic
❌ Mengandung HTTP logic
❌ Mengandung database detail (SQL, dll)
❌ Mengandung styling / tampilan
```

---

## ✅ Yang Boleh Dilakukan

```id="g5p0bd"
✔ Orkestrasi business logic
✔ Validasi input
✔ Mengatur flow proses
✔ Memanggil Domain
✔ Memanggil Repository
```

---

## 🧠 Prinsip Desain

Layer ini mengikuti:

```id="c1yxkg"
✔ Separation of Concerns
✔ Single Responsibility
✔ Dependency Inversion
```

---

## 🚧 Pengembangan Selanjutnya

* DTO Request & Response terpisah
* Fluent Validation
* Use Case per feature (Clean Architecture style)
* CQRS (Command Query Responsibility Segregation)

---

## 🎯 Peran dalam Sistem

```id="8y9mcz"
⚙️ Pengatur alur bisnis
🔗 Penghubung domain dan data
📦 Implementasi use case aplikasi
```

---
