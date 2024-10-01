# Vertical Project Starter

## Description
This project is a base project for 

## Installation
1. Clone the repository.
2. Run `dotnet restore`.
3. Configure the app by setting up copying the `.env.example` into a `.env` file and setting the keys.
4. Optional: Run `dotnet ef migrations add [Users/Other] --project Infrastructure`
5. Run `dotnet ef database update --project Infrastructure`

## Usage
Run the project using the following command:
```bash
dotnet run