# SiLadhida.Core

## 📌 Overview

Layer **Core** merupakan pusat dari domain bisnis aplikasi SiLadhida.
Berisi entitas, aturan bisnis, dan kontrak (interface) tanpa ketergantungan ke layer lain.

## 🧱 Contents

* **Entities**

  * `Product`
  * `Order`
  * `OrderItem`
  * `User`
* **Enums**

  * `Status`
* **Exceptions**

  * `BusinessException`
  * `NotFoundException`
  * `ProductException`
* **Interfaces**

  * `IProductRepository`
  * `IOrderRepository`
* **StateMachines**

  * `OrderStateMachine`

## 🎯 Responsibility

* Menyimpan **business rules**
* Menjaga **validasi domain**
* Tidak bergantung ke framework (EF, API, UI)

## ⚠️ Rules

* Tidak boleh ada dependency ke:

  * Infrastructure
  * API
  * UI
* Semua logic bisnis harus berada di sini

## 🧠 Notes

Layer ini adalah **jantung aplikasi**.
Perubahan di sini akan berdampak ke seluruh sistem.
