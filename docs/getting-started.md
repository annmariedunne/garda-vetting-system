# Garda Vetting System
A secure web-based data reuse system for Garda vetting applications.

## Tech Stack
- ASP.NET Core Razor Pages (.NET 10)
- Entity Framework Core
- SQL Server (LocalDB)
- ASP.NET Identity
- Bootstrap / Bootstrap Icons
- NUnit

## NuGet Packages

### GardaVettingSystem
- `Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore` v10.0.7
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore` v10.0.7
- `Microsoft.AspNetCore.Identity.UI` v10.0.7
- `Microsoft.EntityFrameworkCore.SqlServer` v10.0.7
- `Microsoft.EntityFrameworkCore.Tools` v10.0.7
- `Microsoft.VisualStudio.Web.CodeGeneration.Design` v10.0.2
- `NuGet.Packaging` v7.3.1
- `NuGet.Protocol` v7.3.1
- `QuestPDF` v2026.2.4

### GardaVettingSystem.Tests
- `Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore` v10.0.7
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore` v10.0.7
- `Microsoft.AspNetCore.Identity.UI` v10.0.7
- `Microsoft.EntityFrameworkCore.SqlServer` v10.0.7
- `Microsoft.NET.Test.Sdk` v18.4.0
- `NuGet.Packaging` v7.3.1
- `NuGet.Protocol` v7.3.1
- `NUnit` v4.5.1
- `NUnit.Analyzers` v4.12.0
- `NUnit3TestAdapter` v6.2.0
- `coverlet.collector` v10.0.0

---

## Setting Up the Project


### 1. Clone the Repository

```
git clone https://github.com/annmariedunne/garda-vetting-system.git
```

### 2. Open the Project
Open `GardaVettingSystem.sln` in Visual Studio.

### 3. Check the Connection String
In `appsettings.json` confirm the connection string is correct:
```json
"ConnectionStrings": {
  "GardaVettingSystemDbContextConnection": "Server=(localdb)\\mssqllocaldb;Database=GardaVettingSystem;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

### 4. Create the Database
Open **Tools → NuGet Package Manager → Package Manager Console** and run:
```
Update-Database
```
This creates the database and tables.

### 5. Run the Application
Press **F5** in Visual Studio to compile and run the build or run this in the console:

```
dotnet run
```

---



## Features
- Volunteer registration and secure authentication via ASP.NET Identity
- Single applicant profile per registered user
- Full address history management
- Cryptographically secure 12-character access code generation
- Organisation-side code validation — no login required
- Access code revocation with audit trail preserved
- PDF export of vetting profile
- GDPR-compliant — full right to view, edit and delete personal data

---

## Project Structure
```
GardaVettingSystem/
├── Data/
│   ├── GardaVettingSystemDbContext.cs    # Database context
│   └── Migrations/                       # EF Core migrations
├── Models/
│   ├── Applicant.cs                      # Applicant profile model
│   ├── ApplicantAddress.cs               # Address history model
│   └── AccessCode.cs                     # Access code model
├── Pages/
│   ├── Applicants/                       # Applicant profile CRUD pages
│   ├── ApplicantAddresses/               # Address history CRUD pages
│   ├── AccessCodes/                      # Access code CRUD pages
│   └── Shared/                           # Layout and partials
├── Services/
│   └── VettingPdfService.cs              # PDF export service
└── wwwroot/                              # Static assets
```


## Running the Tests
Open **Package Manager Console** and run:

```
dotnet test
```