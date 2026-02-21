
# BillingReminder

Sistema de recordatorios automáticos de facturas basado en estados almacenados en MongoDB.

El sistema procesa facturas según su estado y envía notificaciones por correo electrónico utilizando SMTP.

---

# Descripción del Problema

El sistema debe:

- Procesar facturas en estado **primerrecordatorio**
  - Enviar correo notificando cambio
  - Actualizar estado a **segundorecordatorio**

- Procesar facturas en estado **segundorecordatorio**
  - Enviar correo notificando desactivación
  - Actualizar estado a **desactivado**

Incluye:
- Backend en .NET 8 (API REST)
- MongoDB
- SMTP con MailKit
- Frontend Angular (SPA Standalone)
- Pruebas unitarias con xUnit + Moq

# Requisitos

- .NET 8 SDK
- Node.js 18+
- Angular CLI
- MongoDB

---

# 🗄 MongoDB

Base de datos:
billing_db

Colección:
invoices

Ejemplo documento:

{
  "ClientId": "C1",
  "ClientEmail": "cliente1@mail.com",
  "Status": "primerrecordatorio",
  "Total": 100000,
  "CreatedAt": "2026-02-17T00:00:00Z"
}

---

# SMTP (appsettings.json)

{
  "Smtp": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "UseSsl": false,
    "From": "tu_correo@gmail.com",
    "UserName": "tu_correo@gmail.com",
    "Password": "APP_PASSWORD"
  }
}

---

# Ejecutar Backend

1. Abrir solución en Visual Studio
2. Seleccionar BillingReminder.Api y compilarlo
3. Ejecutar (F5)

---

# Ejecutar Frontend

1. cd billing-reminder-ui
2. npm install (Version sugerida: 20.11.1)
3. ng serve -o

---

# Tests

Visual Studio:
Test > Run All Tests

CLI:
dotnet test

---
