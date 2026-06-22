# 🌐 SiLadhida.API

**SiLadhida.API** adalah layer yang bertugas sebagai **interface antara client (App)** dan sistem backend.

Layer ini menggunakan **ASP.NET Core Web API** untuk menerima request dan mengembalikan response dalam bentuk JSON.

---

## 🎯 Tujuan Layer API

Layer ini bertanggung jawab untuk:

* Menerima request dari client (UI)
* Melakukan validasi awal
* Meneruskan request ke Application Layer
* Mengembalikan response ke client
* Menjaga keamanan endpoint

---

## 🧱 Struktur Umum

```id="j1b3v9"
SiLadhida.API
│
├── Controllers/
│   └── OrderController.cs
│
├── DTOs/
│   ├── Requests/
│   └── Responses/
│
├── Common/
│   └── ApiResponse.cs
│
└── Program.cs
```

---

## 🔥 Controller

### 🔹 OrderController

Controller utama untuk mengelola pesanan.

#### Endpoint:

```id="bq5d2x"
GET     /api/orders
GET     /api/orders/{id}
POST    /api/orders
POST    /api/orders/{id}/items
PATCH   /api/orders/{id}/status
PATCH   /api/orders/{id}/items/{productId}
```

---

## 🔄 Alur Request

```id="q9g9uh"
Client → API Controller → Application Service → Domain → Response
```

---

## 📦 DTO (Data Transfer Object)

DTO digunakan untuk:

```id="c92y1p"
✔ Memisahkan data dari domain
✔ Menyesuaikan format request/response
✔ Menghindari overexposure entity
```

---

### 🔹 Request DTO

Contoh:

```id="6x5u7c"
CreateOrderDto
AddOrderItemDto
UpdateItemQuantityDto
UpdateStatusDto
```

---

### 🔹 Response DTO

```id="jv0qxs"
OrderResponseDto
```

---

## 📌 ApiResponse Wrapper

Semua response dibungkus menggunakan format standar:

```json id="61qf2x"
{
  "success": true,
  "message": "Berhasil",
  "data": {}
}
```

---

## 🔐 Authorization

Menggunakan:

```id="ew4csn"
[Authorize(Roles = "Admin, Kasir")]
```

Untuk memastikan hanya user tertentu yang dapat mengakses endpoint.

---

## 🧠 Prinsip Desain

Layer ini mengikuti:

```id="8b5q4f"
✔ Thin Controller (logic minimal)
✔ Separation of Concerns
✔ DTO-based communication
✔ Stateless API
```

---

## ⚠️ Batasan Layer API

Layer ini **tidak boleh**:

```id="8q4m0c"
❌ Mengandung business logic
❌ Mengakses database langsung
❌ Mengandung UI logic
```

---

## ✅ Yang Dilakukan Controller

```id="9v4u1s"
✔ Validasi request
✔ Mapping DTO → Model
✔ Memanggil Application Service
✔ Mengembalikan response
```

---

## 🔧 Contoh Implementasi

```csharp id="a4c5d1"
[HttpPatch("{id}/status")]
public async Task<IActionResult> UpdateStatus(int id, UpdateStatusDto dto)
{
    var order = await _service.UpdateStatusAsync(id, dto.Trigger);
    var result = _mapper.Map<OrderResponseDto>(order);

    return Ok(ApiResponse<object>.SuccessResponse(result, "Status berhasil diperbarui"));
}
```

---

## 🔗 Hubungan dengan Layer Lain

```id="l9p3x2"
App → API → Application → Core
```

---

## 🚧 Pengembangan Selanjutnya

* Global Exception Handling
* Logging terstruktur
* API Versioning
* Swagger Documentation
* Rate Limiting

---

## 🎯 Peran dalam Sistem

```id="g1q6zt"
🌐 Gerbang komunikasi client-server
📦 Penyedia endpoint
🔐 Pengatur akses
```

---
