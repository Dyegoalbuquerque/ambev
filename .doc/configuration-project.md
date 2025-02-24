[Back to README](../README.md)

### Configurations project


## Database Configuration Documentation

This document provides guidelines on how to configure and apply database migrations for the project.

## Prerequisites

Before starting, ensure you have the following tools installed:

- **.NET SDK** (version 5 or higher)
- **Entity Framework Core CLI** (`dotnet ef`)
- Ensure your project is set up correctly with the necessary connection strings and `DbContext`.

## Folder Structure

This guide assumes the following project folder structure:

- `./Ambev.DeveloperEvaluation.ORM` - This folder contains the Entity Framework Core migration files.
- `./src` - This folder contains the source code and the Web API project.

## Adding a Migration

To add a new migration for the database, run the following command from the `./Ambev.DeveloperEvaluation.ORM` folder:

```bash
dotnet ef migrations add AmbevDBMigrations --output-dir Migrations --context DefaultContext
```

## Applying Migration

To applying migration for the database, run the following command from the `./src` folder:

```bash
dotnet ef database update --project ./Ambev.DeveloperEvaluation.ORM --startup-project ./Ambev.DeveloperEvaluation.WebApi
```



## Running the Project
Once you've set up the project and configured the database, you can run it locally.

Run the Application
To start the application, execute the following command from the root of the project:

```bash
dotnet run --project ./Ambev.DeveloperEvaluation.WebApi
```


## Testing the Project
Unit Tests
To run unit tests for the project, navigate to the test project directory (if separate) and execute:

```bash
dotnet test 
```




<br>
<div style="display: flex; justify-content: space-between;">
  <a href="./general-api.md">Previous: General API</a>
  <a href="./tech-stack.md">Next: Tech Stack</a>
</div>