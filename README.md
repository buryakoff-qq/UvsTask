# UVS Test - Employee Console App

A simple console application for **creating/updating** and **retrieving** employees from a PostgreSQL database using **Entity Framework Core**.

The project follows a **Clean Architecture** pattern with Application, Domain, and Infrastructure layers.

---

## Important Note

This project targets **.NET 6** as required by the assignment.

While setting up the project I had to **upgrade the `Npgsql` package** in the provided project, because the originally resolved version failed to run due to a **critical vulnerability restriction**.  
After upgrading the package, the script runs normally.

---

## Technologies Used

- .NET 6
- Entity Framework Core
- PostgreSQL
- Npgsql EF Core provider
- Dependency Injection (`Microsoft.Extensions.DependencyInjection`)
- DotNetEnv (for `.env` configuration)

---

## Project Structure

```
src/
├── UvsTask.ConsoleApp/          # CLI entry point, argument parsing, DI setup
├── UvsTask.Application/         # Application services, business logic, interfaces
├── UvsTask.Domain/              # Domain entities
└── UvsTask.Infrastructure/      # EF Core DbContext, repository implementations
```

## Database Setup

Run the setup script from the repository root:

```powershell
.\setUpDatabase.ps1
```


## Testing

Run the initial verification script to validate application behavior from the root directory:

```powershell
.\verifySubmission.ps1
```

---

## Configuration

The application requires a `DB_CONNECTION` environment variable with a PostgreSQL connection string.

You can either provide it directly as an environment variable or create a `.env` file in `src/UvsTask.ConsoleApp`:


```
DB_CONNECTION=Host=localhost;Port=7777;Database=uvsproject;Username=postgres;Password=guest
```

The application will automatically load the `.env` file on startup.  
Alternatively, the variable can be provided directly via the CLI environment.


## Running the Application

Execute all commands from the `src/UvsTask.ConsoleApp` directory.

**Set Employee** - Create or update an employee:

```powershell
dotnet run set-employee --employeeId 1 --employeeName Steve --employeeSalary 999
```

**Get Employee** - Retrieve employee data by ID:

```powershell
dotnet run get-employee --employeeId 1
```

**Output example:**

```
EmployeeId=1, Name=Steve, Salary=999
```

**If employee not found:**

```
Employee 1 not found
```

---


