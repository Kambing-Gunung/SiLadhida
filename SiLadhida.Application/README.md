# SiLadhida.Application

## 📌 Overview

Layer **Application** berisi orchestration logic yang menghubungkan domain dengan dunia luar.

## 🧱 Contents

* **Interfaces**

  * `IProductService`
  * `IOrderService`
* **Services**

  * `ProductService`
  * `OrderService`

## 🎯 Responsibility

* Menjalankan **use case aplikasi**
* Mengatur flow:

  * Ambil data dari repository
  * Validasi tambahan
  * Jalankan domain logic
  * Simpan perubahan

## 🔄 Flow Example

Controller → Application Service → Domain → Repository

## ⚠️ Rules

* Tidak boleh:

  * Akses database langsung
  * Pakai EF Core
  * Pakai HTTP / JWT / UI
* Hanya menggunakan:

  * Core layer
  * Interface repository

## 🧠 Notes

Layer ini adalah **penghubung antara API dan Core**.
