# Vertical Project Starter

## Description
This project is a base project for any feature-based application 

## Installation
1. Clone the repository
2. Configure the app by copying the contents of `.env.example` to a new `.env` file and setting the appropriate values
3. Restore the project:
```bash
dotnet restore
```

## Usage
Run the project using the following command:
```bash
dotnet run --project WebApi
```

## Migrations
Because Migrations run automatically when starting the project in **Development**
(see `Infrastructure/DependencyInjection`@`EnsureDatabaseMigratedAsync`)
Migrations don't need manual intervention, but you can run the following:
- Add new migration based on any Entity changes:
```bash
dotnet ef migrations add [CustomName] --project Infrastructure
```
- Run any new migrations:
```bash
dotnet ef database update --project Infrastructure
```