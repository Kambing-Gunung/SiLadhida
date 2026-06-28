# ☕ SiLadhida — Coffee Shop Management System

Modern desktop application for managing products, orders, and transactions built with **.NET 8** and **Avalonia UI**, using a clean and scalable layered architecture.

---

## 📸 Preview

> *(Tambahkan screenshot di sini nanti)*

```id="img1"
![Dashboard](./docs/dashboard.png)
![Orders](./docs/orders.png)
![Transactions](./docs/transactions.png)
```

---

## 🚀 Features

* 📦 Product Management (CRUD + Stock)
* 🧾 Order Management with real workflow
* 🔄 State Machine-based Order Status
* 💳 Payment & Order Processing
* 📊 Dashboard Summary
* 🔍 Search & Filtering
* 🧮 Transaction History

---

## 🧠 Key Concepts

This project is not just CRUD — it implements real engineering concepts:

* **Layered Architecture**
* **MVVM (Model-View-ViewModel)**
* **State Machine (Order Flow)**
* **Service Layer Pattern**
* **Separation of Concerns**

---

## 🏗️ Architecture Overview

```id="arch1"
[ Avalonia UI (App) ]
           ↓
[ API Controller Layer ]
           ↓
[ Application Layer (Business Logic) ]
           ↓
[ Domain Layer (Core) ]
           ↓
[ Infrastructure (Database / Persistence) ]
```

---

## 🧱 Project Structure

```id="struct1"
SiLadhida
│
├── SiLadhida.App            # Desktop UI (Avalonia)
├── SiLadhida.API            # REST API (ASP.NET Core)
├── SiLadhida.Application    # Business Logic
├── SiLadhida.Core           # Domain & Entities
├── SiLadhida.Infrastructure # Data Access
```

---

## 🔄 Order Workflow (State Machine)

```id="state1"
MenungguPembayaran
        ↓
   (Bayar)
        ↓
    SiapDiambil
        ↓
   (Ambil)
        ↓
     Selesai

↘ (Cancel)
  Dibatalkan
```

---

## 🛠️ Tech Stack

* **.NET 8**
* **Avalonia UI**
* **CommunityToolkit.MVVM**
* **ASP.NET Core Web API**
* **Entity Framework Core** *(optional / if used)*
* **RESTful API**

---

## ⚙️ How to Run

### 1. Run API

```bash id="run1"
cd SiLadhida.API
dotnet run
```

---

### 2. Run App

```bash id="run2"
cd SiLadhida.App
dotnet run
```

---

## 🎯 Why This Project?

This project was built to:

* Practice **clean architecture principles**
* Implement **real-world business logic**
* Build a **modern desktop application**
* Improve understanding of **scalable system design**

---

## 🔥 Highlights

* Not just UI — includes **full backend architecture**
* Implements **State Machine for business rules**
* Clean separation between **UI, API, and Domain**
* Reusable and scalable structure

---

## 🚧 Future Improvements

* 📈 Dashboard Charts
* 🔄 Real-time updates
* 🧠 Advanced state management
* 📱 Cross-platform (mobile/web)
* 🌙 Dark/Light theme switching

---

## 📌 Notes

This project focuses on **architecture, maintainability, and real-world patterns**, making it suitable as a portfolio project for software engineering roles.

---
