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
Run the project:
```bash
dotnet run --project WebApi
```

Clean the project:
```bash
dotnet clean
```

If you're using an IDE, the `WebApi/Properties/launchSettings.json` file will be recognized,
and you will be able to `run`/`debug` the application from the UI

## Migrations
Because Migrations run automatically when starting the project
(see `Infrastructure/InfrastructureConfig`@`EnsureDatabaseMigratedAsync`)
Migrations don't need manual intervention, but you can run the following:
- Add new migration based on any Entity changes:
```bash
dotnet ef migrations add [CustomName] --project Infrastructure
```
- Run any new migrations:
```bash
dotnet ef database update --project Infrastructure
```
- Save migration SQL script:
```bash
dotnet ef migrations script --no-build --project ./Infrastructure/ > migration.sql
```

## Tests
### Unit Tests
```bash
dotnet test ./Unit/Unit.csproj
```

### SpecFlow Tests
```bash
dotnet test ./Spec/Spec.csproj
```

### SpecFlow on Windows
```bash
dotnet test .\Spec\Spec.csproj
```