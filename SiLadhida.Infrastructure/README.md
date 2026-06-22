# 🗄️ SiLadhida.Infrastructure

**SiLadhida.Infrastructure** adalah layer yang bertanggung jawab untuk menangani **akses data** dan integrasi dengan sistem eksternal seperti database, file system, atau layanan lainnya.

Layer ini menjadi implementasi konkret dari kebutuhan yang didefinisikan di `SiLadhida.Application`.

---

## 🎯 Tujuan Layer Infrastructure

Layer ini bertugas untuk:

* Mengelola akses ke database
* Menyediakan implementasi repository
* Menghubungkan aplikasi dengan resource eksternal
* Menyimpan dan mengambil data

---

## 🧱 Struktur Umum

```id="g4m8xz"
SiLadhida.Infrastructure
│
├── Persistence/
│   ├── AppDbContext.cs
│   └── Configurations/
│
├── Repositories/
│   └── OrderRepository.cs
│
├── Migrations/ (jika menggunakan EF Core)
│
└── DependencyInjection.cs
```

---

## 🗃️ Persistence

### 🔹 AppDbContext

Digunakan untuk mengatur koneksi ke database dan mapping entity.

```id="7n1z2r"
- DbSet<Order>
- DbSet<Product>
- DbSet<OrderItem>
```

---

## 📦 Repository Pattern

Repository digunakan sebagai jembatan antara:

```id="qv6x92"
Application Layer ↔ Database
```

---

### 🔹 Contoh Repository

```csharp id="r9t8l3"
public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public async Task<Order?> GetByIdAsync(int id)
        => await _context.Orders.FindAsync(id);

    public async Task<List<Order>> GetAllAsync()
        => await _context.Orders.ToListAsync();

    public async Task AddAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }
}
```

---

## 🔄 Alur Data

```id="y1o7nb"
Application Service
       ↓
Repository
       ↓
DbContext
       ↓
Database
```

---

## 🧠 Prinsip Desain

Layer ini mengikuti:

```id="p6q2kt"
✔ Dependency Injection
✔ Repository Pattern
✔ Separation of Concerns
✔ Low coupling dengan domain
```

---

## ⚠️ Batasan Layer Infrastructure

Layer ini **tidak boleh**:

```id="s8v1xz"
❌ Mengandung business logic
❌ Mengandung UI logic
❌ Mengontrol flow aplikasi
```

---

## ✅ Yang Dilakukan Infrastructure

```id="m2x5fw"
✔ Query database
✔ Simpan data
✔ Integrasi external service
✔ Implementasi repository
```

---

## 🔗 Hubungan dengan Layer Lain

```id="o4t9ks"
Core ← Application ← Infrastructure
```

---

## 🧩 Dependency Injection

Biasanya dikonfigurasi di:

```id="k7v3dc"
DependencyInjection.cs
```

Contoh:

```csharp id="8m2v1y"
services.AddScoped<IOrderRepository, OrderRepository>();
```

---

## 🛠️ Teknologi yang Digunakan

* Entity Framework Core (opsional)
* SQL Database (SQLite / SQL Server)
* Dependency Injection (.NET)

---

## 🚧 Pengembangan Selanjutnya

* Unit of Work Pattern
* Caching (Redis / Memory)
* Logging (Serilog)
* Soft Delete
* Audit Trail (CreatedAt, UpdatedAt)

---

## 🎯 Peran dalam Sistem

```id="u3k8df"
🗄️ Penyimpanan data
🔗 Penghubung ke database
⚙️ Implementasi teknis dari repository
```

---
