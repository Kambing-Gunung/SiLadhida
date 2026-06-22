# 🖥️ SiLadhida.App

**SiLadhida.App** adalah aplikasi desktop berbasis **Avalonia UI** yang berfungsi sebagai **client (frontend)** untuk berinteraksi dengan sistem SiLadhida melalui API.

Layer ini menggunakan arsitektur **MVVM (Model-View-ViewModel)** untuk memisahkan tampilan dan logika.

---

## 🎯 Tujuan Layer App

Layer ini bertugas untuk:

* Menampilkan UI kepada pengguna
* Mengelola interaksi user
* Menghubungkan UI dengan API
* Mengatur state dan behavior tampilan

---

## 🧱 Struktur Umum

```id="y3h7kz"
SiLadhida.App
│
├── Features/
│   ├── Dashboard/
│   ├── Order/
│   ├── Product/
│   └── Transaction/
│
├── Components/
│   ├── Dialogs/
│   ├── Inputs/
│   └── Converters/
│
├── Services/
│   └── Api/
│       ├── OrderService.cs
│       └── ProductService.cs
│
├── Core/
│   ├── Api/
│   │   └── ApiClient.cs
│   └── Auth/
│
├── Styles/
│   ├── Buttons.axaml
│   ├── Typography.axaml
│   ├── Layouts.axaml
│   └── Colors.axaml
│
└── App.axaml
```

---

## 🧠 Arsitektur MVVM

### 🔹 View (XAML)

```id="r8v4sz"
DashboardView.axaml
ProductView.axaml
OrderView.axaml
TransactionView.axaml
```

Bertanggung jawab untuk:

```id="t3k6mz"
✔ Layout UI
✔ Binding ke ViewModel
✔ Styling
```

---

### 🔹 ViewModel

```id="g6n2qb"
OrderViewModel
DashboardViewModel
TransactionViewModel
```

Bertanggung jawab untuk:

```id="m1q5xr"
✔ State UI
✔ Command (aksi tombol)
✔ Memanggil Service
✔ Data binding
```

---

### 🔹 Model (Client-side)

```id="p2l9vs"
Order
OrderItem
Product
```

Digunakan untuk:

```id="d4f8xt"
✔ Representasi data dari API
✔ Binding ke UI
```

---

## 🔄 Alur Data UI

```id="v6j3qw"
View
 ↓
ViewModel
 ↓
App Service
 ↓
ApiClient
 ↓
API
```

---

## 🔧 Service (Client-side)

### 🔹 OrderService (App)

Digunakan untuk komunikasi HTTP ke backend:

```id="n7k2ad"
✔ Get Orders
✔ Create Order
✔ Add Item
✔ Update Status
✔ Update Quantity
```

---

### 🔹 ApiClient

Wrapper untuk HTTP:

```id="q9t5lb"
✔ GET
✔ POST
✔ PUT
✔ PATCH
✔ DELETE
✔ Handle Error
✔ Attach Token
```

---

## 🎨 Styling System

Menggunakan **Design System berbasis Styles**:

### 🔹 Colors

```id="z8n4yf"
CoffeePrimaryBrush
CoffeeSecondaryBrush
CoffeeAccentBrush
```

---

### 🔹 Typography

```id="f3k7qd"
heading
subtitle
label
dashboard-number
```

---

### 🔹 Components

```id="w6v2xm"
Button.primary
Button.secondary
Button.danger
Button.icon-button
```

---

### 🔹 Layout

```id="b5p9rc"
page
section
card
```

---

## 🧩 Komponen UI

### 🔹 Dialog

```id="d8k1zu"
BaseDialog
OrderDialog
ProductPickerDialog
```

---

### 🔹 Input

```id="n4x6sj"
SearchBox
TextBox
```

---

### 🔹 Converter

Digunakan untuk binding logic:

```id="y2f8cm"
Boolean Converter
Opacity Converter
```

---

## ⚠️ Batasan Layer App

Layer ini **tidak boleh**:

```id="p7s3kc"
❌ Mengandung business logic kompleks
❌ Mengakses database langsung
❌ Menggunakan domain logic langsung dari Core
```

---

## ✅ Yang Dilakukan App

```id="g1k6vt"
✔ UI rendering
✔ User interaction
✔ API communication
✔ State management sederhana
```

---

## 🧠 Prinsip Desain

```id="j9w2dp"
✔ MVVM Pattern
✔ Separation of Concerns
✔ Reusable Components
✔ Centralized Styling
```

---

## 🚧 Pengembangan Selanjutnya

* State management lebih advanced
* UI animation & micro interaction
* Lazy loading / pagination
* Offline mode
* Theme switching (dark/light)

---

## 🎯 Peran dalam Sistem

```id="k2z9qx"
🖥️ Interface pengguna
🎨 Tampilan visual aplikasi
🔗 Penghubung user dengan sistem backend
```

---
