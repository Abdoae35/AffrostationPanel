# AfforestationPanel

A comprehensive web-based system for tracking and managing afforestation activities across multiple locations. This project streamlines the process of recording, monitoring, and reporting tree planting operations, eliminating the manual Excel-based workflow and reducing data entry errors.

## 🌳 Problem Statement

Organizations involved in large-scale afforestation projects often face challenges when tracking planting activities across multiple locations and time periods. The traditional approach involves:

- Creating weekly Excel files and distributing them to field workers at different locations
- Manual data collection and compilation from multiple sources
- Time-consuming data merging when reports span multiple weeks or months
- High error rates due to manual data entry and file handling
- Delays in responding to ministry or management requests for activity reports

**AfforestationPanel** solves these problems by providing a centralized, automated system that tracks all planting activities in real-time and generates reports for any custom duration instantly.

## ✨ Features

### Admin Dashboard
- Add, edit, and delete afforestation records
- View comprehensive statistics and analytics
- Search and filter data by custom date ranges
- **Generate and export Excel reports** for any time period automatically
- User management: create, edit, and delete user accounts
- Role management: assign Admin, User, or Viewer roles
- Reset user passwords and manage account access

### User Portal
- Add new afforestation entries with:
  - Plant name/type
  - Location
  - Number of plants
  - Planting date
- Simple, intuitive interface for field workers

### Viewer Access
- Read-only access to all afforestation data
- View statistics and reports
- No ability to modify database records
- Perfect for company management and stakeholders

## 🛠️ Tech Stack

- **Backend:** ASP.NET Web API (.NET Core/Framework)
- **Frontend:** ASP.NET MVC
- **Languages:** C#, HTML, CSS, JavaScript
- **UI Framework:** Bootstrap
- **Database:** Entity Framework Core (Code First)
- **ORM:** Entity Framework Core

## 📋 Prerequisites

Before you begin, ensure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or higher recommended)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or SQL Server Express
- [Visual Studio](https://visualstudio.microsoft.com/) 2019/2022 or [Visual Studio Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

## 🚀 Installation

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/AfforestationPanel.git
cd AfforestationPanel
```

### 2. Configure Database Connection

Open `appsettings.json` and update the connection string to match your SQL Server instance:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=AfforestationDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### 3. Run Entity Framework Migrations

The project uses Entity Framework Core Code First approach. The database models are located in the **DAL (Data Access Layer)**.

Open the Package Manager Console in Visual Studio or use the command line:

```bash
# Add migration
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update
```

Or using Package Manager Console in Visual Studio:

```powershell
Add-Migration InitialCreate
Update-Database
```

### 4. Run the Application

```bash
dotnet run
```

Or press `F5` in Visual Studio to run with debugging.

The application should now be running at `https://localhost:5001` (or the port specified in your launch settings).

## 👥 User Roles

### Admin
- Full system access
- Create, read, update, and delete afforestation records
- Generate Excel reports for any custom duration
- View statistics and analytics
- **User Management:**
  - Create new Admin, User, and Viewer accounts
  - Reset user passwords
  - Delete user accounts
  - Manage role assignments

### User
- Add new afforestation entries
- Input plant name, location, quantity, and planting date
- View their own submitted records

### Viewer
- Read-only access to the system
- View all afforestation data and statistics
- Cannot modify or delete any records
- Ideal for company executives and ministry stakeholders

## 📊 Usage

### Adding Afforestation Records (User/Admin)

1. Log in to your account
2. Navigate to "Add New Record"
3. Fill in the required information:
   - Plant Name/Type
   - Location
   - Number of Plants
   - Planting Date
4. Submit the form

### Generating Reports (Admin Only)

1. Navigate to the Reports section
2. Select your desired date range (start and end dates)
3. Apply any additional filters (location, plant type, etc.)
4. Click "Generate Report"
5. Export to Excel with one click

### Managing Users (Admin Only)

1. Go to User Management
2. Create new accounts by selecting role and entering details
3. Reset passwords or delete accounts as needed
4. Assign or modify user roles

## 📁 Project Structure

```
AfforestationPanel/
├── Controllers/          # MVC and API Controllers
├── Models/              # View Models
├── Views/               # Razor Views
├── DAL/                 # Data Access Layer (DbContext, Entities)
├── wwwroot/             # Static files (CSS, JS, Images)
├── Services/            # Business Logic
└── appsettings.json     # Configuration
```

## 🤝 Contributing

Contributions are welcome! If you'd like to contribute to this project:

1. Fork the repository
2. Create a new branch (`git checkout -b feature/YourFeature`)
3. Commit your changes (`git commit -m 'Add some feature'`)
4. Push to the branch (`git push origin feature/YourFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📧 Contact

For questions, suggestions, or support, please contact:

- **Project Maintainer:** Abdelrahman
- **Email:** Abdelrahman3535.elsheref@gmail.com
- **GitHub:** [@Abdoae35](https://github.com/Abdoae35)

## 🙏 Acknowledgments

- Thanks to all field workers and environmental teams who inspired this project
- Built to support sustainable afforestation efforts and environmental conservation

---

**Made with 🌱 for a greener future**