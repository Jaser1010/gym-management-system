<div align="center">

<img src="https://img.icons8.com/3d-fluency/94/dumbbell.png" width="80" alt="Gym Logo"/>

# Gym Management System

### Full-Stack Web Application · ASP.NET Core MVC · Layered Architecture

<br/>

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12-239120?style=flat-square&logo=csharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![ASP.NET Core MVC](https://img.shields.io/badge/ASP.NET_Core-MVC-0078D4?style=flat-square&logo=dotnet&logoColor=white)](https://learn.microsoft.com/en-us/aspnet/core/mvc/)
[![EF Core](https://img.shields.io/badge/EF_Core-ORM-68217A?style=flat-square&logo=nuget&logoColor=white)](https://learn.microsoft.com/en-us/ef/core/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-Database-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/en-us/sql-server/)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5-7952B3?style=flat-square&logo=bootstrap&logoColor=white)](https://getbootstrap.com/)

[![GitHub repo size](https://img.shields.io/github/repo-size/Jaser1010/gym-management-system?style=flat-square&color=blue)](https://github.com/Jaser1010/gym-management-system)
[![GitHub last commit](https://img.shields.io/github/last-commit/Jaser1010/gym-management-system?style=flat-square&color=green)](https://github.com/Jaser1010/gym-management-system/commits/master)
[![GitHub stars](https://img.shields.io/github/stars/Jaser1010/gym-management-system?style=flat-square&color=yellow)](https://github.com/Jaser1010/gym-management-system/stargazers)
[![License: MIT](https://img.shields.io/badge/License-MIT-orange?style=flat-square)](LICENSE)

<br/>

*A comprehensive gym management platform with member registration, trainer management, session scheduling,*
*membership plans, booking workflows, health records, analytics dashboard, and file attachment support —*
*built on a strict three-layer architecture separating UI, business logic, and data access.*

<br/>

[🚀 Quick Start](#-quick-start) · [🐛 Report Bug](https://github.com/Jaser1010/gym-management-system/issues) · [💡 Request Feature](https://github.com/Jaser1010/gym-management-system/issues)

</div>

---

<br/>

## 📑 Table of Contents

<details>
<summary>Click to expand</summary>

- [Highlights](#-highlights)
- [Architecture Overview](#-architecture-overview)
- [Solution Structure](#-solution-structure)
- [Tech Stack](#-tech-stack)
- [Features in Detail](#-features-in-detail)
- [Quick Start](#-quick-start)
- [Configuration](#%EF%B8%8F-configuration)
- [Design Patterns](#-design-patterns)
- [Roadmap](#-roadmap)
- [Contributing](#-contributing)
- [License](#-license)
- [Author](#-author)

</details>

<br/>

---

## ⚡ Highlights

<table>
<tr>
<td width="50%">

👥 **Member Management**
Complete CRUD operations for gym members with profile photos (file upload), personal info, gender, and linked health records with BMI, blood pressure, and medical notes.

</td>
<td width="50%">

🏋️ **Trainer Management**
Full trainer profiles with specialties (Fitness, Yoga, CrossFit, Bodybuilding), certifications, photo uploads, and linked session assignments.

</td>
</tr>
<tr>
<td width="50%">

📅 **Session Scheduling**
Create, edit, and manage gym sessions — each linked to a trainer, category, time slot, capacity, and location. Tracks session status (Upcoming, Ongoing, Completed).

</td>
<td width="50%">

📋 **Booking System**
Book members into sessions with capacity tracking. View member lists for upcoming and ongoing sessions. Manage member-session relationships via a many-to-many join.

</td>
</tr>
<tr>
<td width="50%">

💳 **Membership & Plans**
Create subscription plans with pricing, duration, and descriptions. Assign members to plans with start/end dates. Track membership histories per member.

</td>
<td width="50%">

📊 **Analytics Dashboard**
Overview page with total counts for members, trainers, sessions, and plans — giving managers a quick operational snapshot.

</td>
</tr>
<tr>
<td width="50%">

🏗️ **Layered Architecture (PL / BLL / DAL)**
Three dedicated projects with strict separation — swap your data layer, modify business rules, or redesign the UI independently.

</td>
<td width="50%">

📦 **Unit of Work + Generic Repository**
`UnitOfWork` coordinates all repositories. `GenericRepository<T>` provides type-safe CRUD out of the box, with specialized repos for sessions, bookings, and memberships.

</td>
</tr>
</table>

---

## 🏛 Architecture Overview

The project follows a **classic 3-Tier Layered Architecture**:

```
┌─────────────────────────────────────────────────────────────┐
│                   PRESENTATION LAYER (PL)                   │
│                      GymManagementPL                        │
│    MVC Controllers · Razor Views · Static Assets · CSS      │
│    Bootstrap 5 · jQuery Validation · wwwroot                │
└────────────────────────┬────────────────────────────────────┘
                         │
┌────────────────────────▼────────────────────────────────────┐
│                 BUSINESS LOGIC LAYER (BLL)                  │
│                     GymManagementBLL                        │
│    Services · ViewModels · AutoMapper Profiles              │
│    Business Rules · Validation · Data Transformation        │
└────────────────────────┬────────────────────────────────────┘
                         │
┌────────────────────────▼────────────────────────────────────┐
│                   DATA ACCESS LAYER (DAL)                   │
│                     GymManagementDAL                        │
│    EF Core DbContext · Entities · Repositories · UnitOfWork │
│    Fluent API Configs · Migrations · Data Seeding           │
└─────────────────────────────────────────────────────────────┘
```

### Layer Responsibilities

| Layer | Project | Description |
|:------|:--------|:------------|
| **PL** | `GymManagementPL` | 7 MVC controllers, Razor views (CRUD + Detail pages), `_Layout.cshtml` with navigation, Bootstrap 5 + custom CSS, static file serving, `Program.cs` entry point with DI configuration, auto-migration & seeding. |
| **BLL** | `GymManagementBLL` | 7 services (`MemberService`, `TrainerService`, `PlanService`, `SessionService`, `MembershipService`, `BookingService`, `AnalyticsService`), `AttachmentService` for file uploads, AutoMapper `MappingProfiles`, and comprehensive ViewModels for each domain. |
| **DAL** | `GymManagementDAL` | `GymDbContext` with Fluent API, 11+ entities (`Member`, `Trainer`, `Session`, `Plan`, `MemberShip`, `MemberSession`, `Category`, `HealthRecord`, `GymUser`, `BaseEntity`), `GenericRepository<T>`, specialized repositories, `UnitOfWork`, 9 EF Configurations, data seeding, 6 migrations. |

---

## 📁 Solution Structure

```
GymManagementSystem/
│
├── GymManagementDAL/                  # Data Access Layer
│   ├── Entities/                      # Domain models
│   │   ├── BaseEntity.cs
│   │   ├── GymUser.cs
│   │   ├── ApplicationUser.cs
│   │   ├── Member.cs
│   │   ├── Trainer.cs
│   │   ├── Plan.cs
│   │   ├── Session.cs
│   │   ├── MemberShip.cs
│   │   ├── MemberSession.cs
│   │   ├── HealthRecord.cs
│   │   ├── Category.cs
│   │   └── Enums/
│   ├── Data/
│   │   ├── Contexts/                  # GymDbContext
│   │   ├── Configurations/            # Fluent API entity configs
│   │   ├── DataSeed/                  # GymDbContextSeeding, IdentityDbContextSeeding
│   │   └── Migrations/
│   └── Repositories/
│       ├── Interfaces/                # IGenericRepository, IUnitOfWork, ...
│       └── Classes/                   # GenericRepository, UnitOfWork, ...
│
├── GymManagementBLL/                  # Business Logic Layer
│   ├── Services/
│   │   ├── Interfaces/                # IMemberService, ITrainerService, ...
│   │   ├── Classes/                   # MemberService, TrainerService, ...
│   │   └── AttachmentService/         # File upload handling
│   ├── ViewModels/                    # Per-feature ViewModels (CRUD + Select)
│   │   ├── MemberViewModels/
│   │   ├── TrainerViewModels/
│   │   ├── SessionViewModels/
│   │   ├── PlanViewModels/
│   │   ├── MembershipViewModels/
│   │   ├── BookingViewModels/
│   │   ├── AnalyticsViewModels/
│   │   └── AccountViewModels/
│   └── MappingProfiles.cs             # AutoMapper profiles for all entities
│
└── GymManagementPL/                   # Presentation Layer
    ├── Controllers/
    │   ├── AccountController.cs
    │   ├── HomeController.cs
    │   ├── MemberController.cs
    │   ├── TrainerController.cs
    │   ├── PlanController.cs
    │   ├── SessionController.cs
    │   ├── MembershipController.cs
    │   └── BookingController.cs
    ├── Views/                         # Razor views per controller
    ├── Program.cs
    └── appsettings.json
```

---

## 🛠️ Tech Stack

| Category | Technology | Purpose |
|:---------|:-----------|:--------|
| **Runtime** | [.NET 8.0+](https://dotnet.microsoft.com/) | Long-term support runtime |
| **Framework** | [ASP.NET Core MVC](https://learn.microsoft.com/en-us/aspnet/core/mvc/) | Server-rendered views with Razor |
| **Language** | [C# 12](https://learn.microsoft.com/en-us/dotnet/csharp/) | Primary language (50.5%) |
| **ORM** | [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/) | Database access, migrations & Fluent API |
| **Database** | [SQL Server](https://www.microsoft.com/en-us/sql-server/) | Relational data store |
| **Mapping** | [AutoMapper](https://automapper.org/) | Entity ↔ ViewModel mapping |
| **Frontend** | [Bootstrap 5](https://getbootstrap.com/) + Custom CSS | Responsive UI (44.9% HTML, 4.5% CSS) |
| **Validation** | [jQuery Validation](https://jqueryvalidation.org/) + Data Annotations | Client + server-side validation |
| **Patterns** | Repository, Unit of Work, DI, ViewModel, Service Layer | Enterprise design patterns |
| **Architecture** | 3-Tier Layered (PL / BLL / DAL) | Separation of concerns |

---

## 🎯 Features in Detail

### 👥 Members

- Create members with name, email, phone, gender, date of birth, and **profile photo upload**
- Edit and delete member profiles
- View detailed member cards with all personal information
- **Health Records** — Dedicated health tracking per member (BMI, blood pressure, height, weight, notes)
- Health record create, view, and edit flows

### 🏋️ Trainers

- Full CRUD with photo upload
- Specialty selection: **Fitness, Yoga, CrossFit, Bodybuilding**
- Certified trainer profiles with contact details

### 📅 Sessions

- Create sessions with title, description, location, date/time, duration, and capacity
- Assign a trainer and category to each session
- Dropdown selections powered by `CategorySelectViewModel` and `TrainerSelectViewModel`
- Session lifecycle tracking (Upcoming / Ongoing / Completed)

### 📋 Bookings

- Book members into specific sessions
- View enrolled member lists for ongoing and upcoming sessions
- Capacity-aware booking with member-session many-to-many relationship

### 💳 Memberships & Plans

- Define plans with name, price, duration, max freeze days, and description
- Assign members to plans with start/end dates
- View all memberships for a specific member
- Track active vs. expired memberships

### 📊 Analytics Dashboard

- At-a-glance dashboard with totals: Members, Trainers, Sessions, Plans
- Powered by the `AnalyticsService` aggregation logic

---

## 🚀 Quick Start

### Prerequisites

| Requirement | Minimum Version | Download |
|:------------|:----------------|:---------|
| **.NET SDK** | 8.0 | [dotnet.microsoft.com](https://dotnet.microsoft.com/download) |
| **SQL Server** | Any recent | [SQL Server Downloads](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) |
| **Visual Studio** | 2022+ | [visualstudio.com](https://visualstudio.microsoft.com/) |

### Step-by-Step Setup

```bash
# 1. Clone the repository
git clone https://github.com/Jaser1010/gym-management-system.git
cd gym-management-system

# 2. (Optional) Switch to the most advanced branch
git checkout Ass_5

# 3. Update connection string in appsettings.Development.json if needed

# 4. Restore packages
dotnet restore

# 5. Run (auto-migrates DB + seeds categories & plans on startup)
dotnet run --project GymManagementPL
```

> 💡 **Tip:** The database is automatically migrated and seeded with categories and plans from JSON files on first startup.

---

## ⚙️ Configuration

```jsonc
// appsettings.Development.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=GymManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

---

## 🔬 Design Patterns

| Pattern             | Where used                                              |
|---------------------|---------------------------------------------------------|
| **Repository**      | `IGenericRepository<T>` + entity-specific repositories |
| **Unit of Work**    | `IUnitOfWork` coordinates all repositories             |
| **Service Layer**   | BLL services encapsulate all business logic            |
| **AutoMapper**      | ViewModel ↔ Entity mapping in `MappingProfiles.cs`     |
| **Data Seeding**    | `GymDbContextSeeding` + `IdentityDbContextSeeding`     |
| **Dependency Injection** | All services/repos registered in `Program.cs`     |
---

## 🗺️ Roadmap

#### ✅ Completed

- [x] **Member CRUD** with profile photos and health records
- [x] **Trainer CRUD** with specialties and photos
- [x] **Session scheduling** with trainer/category assignment
- [x] **Booking system** with capacity tracking
- [x] **Membership plans** with pricing and duration
- [x] **Analytics** dashboard
- [x] **Generic Repository** + **Unit of Work**
- [x] **AutoMapper** integration
- [x] **File attachment service**
- [x] JSON-based **data seeding**
- [x] **Auto DB migration**
- [x] Bootstrap 5 responsive UI (12K+ lines of custom CSS)
- [x] **Authentication & Authorization** — ASP.NET Identity with role-based access (Admin, Trainer, Member)


#### 🔜 Coming Soon

- [ ] 💰 **Billing & Invoicing** — Payment tracking and invoice generation
- [ ] 📧 **Email Notifications** — Membership renewal and session reminders
- [ ] 📱 **Responsive Improvements** — Mobile-first redesign
- [ ] 🧪 **Testing Suite** — Unit and integration tests
- [ ] 🐳 **Docker Support** — Containerized deployment

---

## 🤝 Contributing

Contributions are welcome! Fork the repo, create a feature branch, and submit a Pull Request.

```bash
git checkout -b feature/amazing-feature
git commit -m "feat: add amazing feature"
git push origin feature/amazing-feature
```

---

## 📄 License

Distributed under the **MIT License**.

---

## 👤 Author

<table>
<tr>
<td align="center">
<a href="https://github.com/Jaser1010"><b>Jaser Kasim</b></a>
<br/><sub>Software Engineer</sub>
<br/><br/>
<a href="https://github.com/Jaser1010"><img src="https://img.shields.io/badge/GitHub-100000?style=flat-square&logo=github&logoColor=white" alt="GitHub"/></a>
<a href="https://www.linkedin.com/in/jaser-kasim-j1k2/"><img src="https://img.shields.io/badge/LinkedIn-0077B5?style=flat-square&logo=linkedin&logoColor=white" alt="LinkedIn"/></a>
</td>
</tr>
</table>

---

<div align="center">

**If you found this project helpful, consider giving it a ⭐**

Built with ❤️ using **C#** and **ASP.NET Core MVC**

</div>
