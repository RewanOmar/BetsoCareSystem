# 🐾 BetsoCare System

A comprehensive veterinary care management system designed to connect pet owners with veterinary clinics and provide essential pet healthcare services.

## 📌 About the Project

BetsoCare System is a web-based veterinary care platform that helps pet owners manage their pets' healthcare and connect with veterinary clinics.

The system provides services such as clinic discovery, appointment management, vaccination tracking, reports, notifications, articles, and shelters.

## 🚀 Features

- 🔐 User Registration & Login
- 🔑 JWT Authentication
- 🔵 Google Authentication
- 🏥 Veterinary Clinics
- 📅 Appointment Management
- 💉 Vaccination Management
- 🔔 Vaccination Reminders & Notifications
- 📰 Veterinary Articles
- 🏠 Animal Shelters
- 🚨 Animal Bite Reports
- ⚠️ Dangerous Animal Reports
- 📋 Complaint Reports
- 🗺️ Location-based Reports
- 👨‍💼 Admin Dashboard
- 📊 Admin Management & Statistics

## 🛠️ Technologies

### Backend
- ASP.NET Core Web API
- .NET 8
- Entity Framework Core
- SQL Server
- JWT Authentication
- Clean Architecture
- RESTful APIs

### Tools & Libraries
- Hangfire
- Swagger
- Firebase
- Google Authentication
- BCrypt
- Docker

## 🏗️ Architecture

The backend follows a clean and maintainable architecture with separation of responsibilities between:

- Controllers
- DTOs
- Services
- Repositories
- Jobs
- Helpers
- Infrastructure

## 🔔 Background Jobs

The system uses Hangfire for background tasks such as checking upcoming vaccination appointments and sending reminders to users.

## 🔒 Security

Sensitive configuration values such as:

- Database connection strings
- API keys
- OAuth credentials
- Authentication secrets

are not included in the public repository.

## 👨‍💻 Project

**BetsoCare System**

A graduation project focused on developing a modern veterinary healthcare management platform.
