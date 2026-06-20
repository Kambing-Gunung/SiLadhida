# SiLadhida.API

## 📌 Overview

Layer **API** adalah entry point aplikasi yang menangani HTTP request dan response.

## 🧱 Contents

* **Controllers**

  * `AuthController`
  * `ProductController`
  * `OrderController`
* **Auth**

  * JWT Authentication
  * `AuthService`
* **DTOs**

  * Request & Response models
* **Middleware**

  * Exception handling
* **Validators**

  * FluentValidation
* **Mappings**

  * AutoMapper profile

## 🎯 Responsibility

* Menerima request dari client
* Validasi input
* Memanggil Application Service
* Mengembalikan response

## 🔐 Features

* JWT Authentication
* Role-based Authorization (`Admin`, `Kasir`)
* Global Exception Handling
* Standard API Response format

## 📦 Response Format

```json
{
  "success": true,
  "message": "string",
  "data": {}
}
```

## ⚠️ Rules

* Tidak boleh berisi:

  * Business logic
* Semua logic harus di Application/Core

## 🧠 Notes

Layer ini hanya sebagai **interface ke dunia luar**.
