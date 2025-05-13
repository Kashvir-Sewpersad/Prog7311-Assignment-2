

# Prog7311 Assignment 2

## AgriConnect

AgriConnect is a website application built for PROG7311 Part 2. The web app is developed using ASP.NET Core 8.0 and provides a comprehensive platform for managing agriculture-based data, including farmer profiles, employee profiles, and product listings. The application leverages Entity Framework Core for database operations with SQL Server LocalDB (AgriConnectDb), implements services for authentication purposes, and utilizes sessions for user interactions. Following a layered architecture approach through Model-View-Controller with an additional service layer, this README provides instructions on setting up, running, and exploring the application after downloading it from a .zip file.

## Features

- **User Authentication**: Secure login with session management
- **Employee Management**: Create, view, update, and delete farmer profiles
- **Product Management**: Add, edit, and remove agricultural products
- **Database Integration**: Persist data using SQL Server
- **Responsive UI**: Built with ASP.NET Core MVC for a user-friendly interface

## Prerequisites

To set up and run AgriConnect, ensure the following tools are installed:

| Tool | Version | Download Link | Notes |
|------|---------|--------------|-------|
| .NET SDK | 8.0 | [Download](https://dotnet.microsoft.com/download/dotnet/8.0) | Required for building and running the app |
| Visual Studio | 2022 (Community or higher) | [Download](https://visualstudio.microsoft.com/downloads/) | Install with ASP.NET and web development workload |
| SQL Server LocalDB | Latest | [Download](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb) | Typically included with Visual Studio |
| Git (optional) | Latest | [Download](https://git-scm.com/downloads) | For cloning or version control |
| Web Browser | Latest | Chrome, Firefox, or Edge | For accessing the app |

## Setup Instructions

Follow these steps to set up and run AgriConnect from the provided .zip file:

### 1. Download + Extract the .zip File

- Download the `Prog7311_Assignment_2.zip` file from the GitHub repository or submission link
- Extract the .zip file to a directory (e.g., `Projects\Prog7311_Assignment_2`):
  - On Windows: Right-click → Extract All
- Confirm the extracted folder contains:
  - `Prog7311 Assignment 2.sln`
  - `Program.cs`
  - `appsettings.json`
  - Other files and folders related to the project

### 2. Open the Project

- Using Visual Studio:
  1. Launch Visual Studio
  2. Click File → Open → Project/Solution
  3. Navigate to the folder and select `Prog7311 Assignment 2.sln`

### 3. Restore Dependencies

- In Visual Studio:
  - Visual Studio will automatically restore NuGet packages (e.g., Microsoft.EntityFrameworkCore.SqlServer) upon opening the solution
  - If prompted, click Restore in the notification bar

### 4. Configure the Database

- The application uses SQL Server LocalDB with the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=AgriConnectDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```
### Apply Database Migrations:
- In Visual Studio:
  1. Open Tools → NuGet Package Manager → Package Manager Console
  2. Run the following commands:
  ```
  clear
  Add-Migration InitialCreate
  Update-Database
  clear
  ```
  3. This will create the AgriConnectDb database in LocalDB

### 5. Run the Application
- In Visual Studio:
  1. Ensure the startup project is Prog7311 Assignment 2
  2. Press Start Debugging (F5) or click the green run button
  3. The application will launch in your default browser

### 6. Access the Application
- Upon running the application, you will be directed to the app in your default browser
- The default route (Home/Index) loads the app homepage

## Application Usage

### Authentication
- Navigate to the login page
- Enter credentials (check for provided default credentials)
- Sessions persist for 30 minutes of inactivity, after which re-login is required

### Farmer Management - Employee
- Access the farmer section: Farmer
- Add Farmer: Complete a form with details
- View Farmers: View a list of registered farmers
- Edit/Delete: Modify or remove farmer profiles – this functionality is partially implemented

### Product Management – Farmer
- Navigate to the product section
- Add Product: Enter details for new products
- View Products: View available products
- Edit/Delete: Update or remove product listings - this functionality is partially implemented

### Navigation Tips
- Forms validate input; ensure all required fields are filled

## Project Structure

Key files and their roles:

| File/Folder | Description |
|-------------|-------------|
| Prog7311 Assignment 2.sln | Visual Studio solution file |
| Program.cs | Application entry point, configures services |
| Prog7311 Assignment 2.csproj | Project file listing .NET 8.0 dependencies |
| appsettings.json | Configuration for database connection strings |
| appsettings.Development.json | Development-specific logging settings |
| Data/DataContext.cs | Entity Framework Core DbContext for database operations |
| Services/ | AuthService, FarmerService, and ProductService for business logic |
| Controllers/ | MVC controllers handling HTTP requests |
| Views/ | Razor views for building the UI |
| wwwroot/ | Static files including CSS, images, and videos |
| .gitignore | Ignores temporary files and build artifacts |
| .gitattributes | Configures Git file attributes |
