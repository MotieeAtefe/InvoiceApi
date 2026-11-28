# InvoiceApi

A RESTful **Invoice Management API** built with **ASP.NET Core Web API** and **C#**.

The project provides invoice and product management, JWT-based authentication, and MongoDB-based audit logging to track changes made to invoices.

## ✨ Features

- **Invoice Management** – Create, retrieve, and print invoices.
- **Product Management** – Full CRUD operations for products.
- **JWT Authentication & Authorization** – Secure API endpoints using JSON Web Tokens.
- **Invoice Total Calculation** – Automatically calculates invoice amounts.
- **Invoice Audit Logging** – Tracks changes made to invoices and their items.
- **Audit History Storage** – Stores invoice change events in MongoDB.
- **API Documentation** – Interactive API documentation with Swagger.
- **Unit Testing** – Automated unit tests using xUnit.

## 🛠️ Technologies

- **C#**
- **ASP.NET Core Web API**
- **.NET 5**
- **Entity Framework Core**
- **SQL Server**
- **MongoDB**
- **JWT (JSON Web Tokens)**
- **BCrypt** – Password hashing
- **Swagger / OpenAPI**
- **xUnit** – Unit testing
- **Docker** – Local database environment

## 📋 Prerequisites

Before running the project, make sure you have:

- **.NET 5 SDK**
- **SQL Server**
- **MongoDB**
- **Git**

> **Note:** .NET 5 is out of support. This project currently targets .NET 5 and has not yet been migrated to a supported .NET version.

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/MotieeAtefe/InvoiceApi.git
cd InvoiceApi
```

### 2. Configure the Databases

Make sure SQL Server and MongoDB are running.

Configure the SQL Server connection string and MongoDB settings using the appropriate application configuration or User Secrets.

### 3. Run the Application

```bash
dotnet restore
dotnet run --project InvoiceApi/InvoiceApi.csproj
```

### 4. Explore the API

Once the application is running, open Swagger UI to explore and test the available endpoints.

Check the application output for the actual Swagger URL and port.
