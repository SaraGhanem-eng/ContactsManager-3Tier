# Contacts Manager – 3-Tier Architecture

A desktop Contacts Management application built with C# and Windows Forms, following a clean **3-Tier (Layered) Architecture** to separate concerns between the user interface, business logic, and data access.

## 🏗️ Architecture Overview

The solution is organized into three independent projects:

| Layer | Project | Responsibility |
|---|---|---|
| **Presentation Layer** | `ContactsConsoleApp-PresentationLayer` (Windows Forms) | Handles UI, user interaction, and input validation display |
| **Business Layer** | `ContactsBusinessLayer` (Class Library) | Contains business rules, entity classes (`clsContacts`, `clsCountries`), and orchestrates calls between UI and data access |
| **Data Access Layer** | `ContactsDataAccessLayer` (Class Library) | Handles all direct communication with SQL Server using ADO.NET (SqlConnection, SqlCommand, SqlDataReader) |


Each layer only communicates with the layer directly below it, keeping the codebase modular, testable, and easy to maintain or extend.

## ✨ Features

- Add, edit, delete, and search contacts
- Full CRUD operations for Countries (linked to contacts)
- Contact photo upload and preview
- Find contact/country by ID or by name
- Existence checks before insert/update operations
- Parameterized SQL queries to prevent SQL Injection

## 🛠️ Tech Stack

- **Language:** C#
- **UI Framework:** Windows Forms (.NET Framework)
- **Data Access:** ADO.NET (`SqlConnection`, `SqlCommand`, `SqlDataReader`, `DataTable`)
- **Database:** Microsoft SQL Server

## 📂 Database

The application connects to a SQL Server database (`ContactsDB`) containing two main tables:
- `Contacts` — stores contact details (name, email, phone, address, date of birth, image path, country)
- `Countries` — stores country reference data (name, code, phone code)

## 🚀 Getting Started

1. Clone the repository
2. Restore the database from the provided `.bak` file (see `/Database` folder if included)
3. Update the connection string in `clsDataAccessSettings.cs` under `ContactsDataAccessLayer`
4. Open the solution in Visual Studio and set the Presentation Layer project as the startup project
5. Build and run

## 📌 Notes

This project was built as a learning exercise to practice:
- ADO.NET fundamentals (Connection, Command, DataReader, DataAdapter)
- Layered software architecture
- Separation of concerns between UI, logic, and data persistence
