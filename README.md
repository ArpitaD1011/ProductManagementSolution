# Product-Management-Solution 
A layered ASP.NET Core Web API application for Managing products, built using clean architecture principles, Entity Framework Core code first approach, and tested with NUnit and Moq.
## Features
-Get,Add,Update,Delete,DecreamentStock,AddToStock 
-Entity framework Core with code-first migrations
-Repository Pattern & Dependency Injection
-DTOs for data transfer across layers
-NUnit +Moq unit testing
-Layered Architecture
  -API
  -Application
  -Domain
  -Infrastructure
  -Tests

## Technologies Used
-ASP.NET Core 8 Web API
-Entity Framework Core
-SQL Express
-c#
-NUnit
-Moq
-Swagger

### Prerequisites
-[.NET 8 SDK](https://microsoft.com/download)
-SQL Express 
-Visual Studio 2022+ 
### Setup
**Clone the repo**
git clone: https://github.com/ArpitaD1011/Product-Management
### Update DB Connection string
Edit Products.API/appsettings.json to match your SQL server connection string
 "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\SQLEXPRESS; Database=ProductManagement;Trusted_Connection=True;TrustServerCertificate=True"

### Apply EF Core Migrations
cd Products.Infrastructure
dotnet ef database update

### Run the Api
cd../Products.API
dotnet run
## Note: Run the script: ProductManagementSolution\Products.Infrastructure\DBScripts
this is DB Programmability/Sequence created to generate Unique Product Id
 
