# SiLadhida.Infrastructure

## 📌 Overview

Layer **Infrastructure** bertanggung jawab untuk implementasi teknis seperti database dan external dependency.

## 🧱 Contents

* **Persistence**

  * `AppDbContext`
  * Seeder (`ProductSeeder`, `UserSeeder`)
* **Repositories**

  * `ProductRepository`
  * `OrderRepository`
* **Migrations**
* **Extensions**

  * Dependency Injection setup

## 🎯 Responsibility

* Implementasi:

  * Database (EF Core)
  * Repository pattern
* Integrasi dengan:

  * MySQL (Pomelo)

## 🔧 Technologies

* Entity Framework Core
* Pomelo MySQL Provider

## ⚠️ Rules

* Tidak boleh berisi:

  * Business logic
* Hanya implementasi dari interface di Core

## 🧠 Notes

Layer ini bisa diganti (misal MongoDB) tanpa mengubah Core & Application.
