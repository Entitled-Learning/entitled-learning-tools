# Entitled Learning Tools UI

This project is a Blazor Server application that provides tools for managing Entitled Learning operations.

## Prerequisites

Before you can run the application locally, ensure you have the following prerequisites installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or access to an Azure SQL database
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (recommended) or [Visual Studio Code](https://code.visualstudio.com/) with C# extensions
- [Git](https://git-scm.com/downloads)

## Local Development Setup

Follow these steps to set up and run the application locally:

### 1. Clone the Repository

```bash
git clone <repository-url>
cd entitled-learning-tools/src
```

### 2. Database Configuration

The application requires a connection to a SQL database. By default, it uses an Azure SQL database as specified in the `appsettings.Development.json` file. 

If you want to use a local database instead:
1. Create a local SQL Server database 
2. Update the connection string in `appsettings.Development.json`:

```json
"ConnectionStrings": {
  "ELSQLConnectionString": "Server=localhost;Database=EntitledLearningDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 3. Azure Blob Storage

This application uses Azure Blob Storage for file management. 

To use a local development storage instead:
1. Install and run the [Azure Storage Emulator](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite)
2. Update the connection string in `appsettings.Development.json`:

```json
"ConnectionStrings": {
  "ELBlobConnectionString": "UseDevelopmentStorage=true"
}
```

### 4. Authentication Configuration

The application uses Microsoft Identity for authentication. To run locally:

1. Create an App Registration in Azure AD (or use an existing one)
2. Update the `AzureAd` section in your `appsettings.Development.json` with your App Registration details

### 5. Building and Running the Application

#### Using .NET CLI:

```bash
cd EntitledLearning.Tools.UI
dotnet restore
dotnet build
dotnet run
```

The application should now be running on https://localhost:7150

**Note:** With the standard `dotnet run` command, the application will not automatically reload when you make code changes. You'll need to stop the application and restart it to see your changes.

#### Using Hot Reload with .NET CLI:

For development with automatic reloading when code changes:

```bash
cd EntitledLearning.Tools.UI
dotnet watch run
```

This will start the application with hot reload enabled, allowing you to see your changes immediately without manually restarting the application.

#### Using Visual Studio:

1. Open the `src.sln` solution file in Visual Studio
2. Set `EntitledLearning.Tools.UI` as the startup project
3. Press F5 or click the "Start" button

### 6. Debugging with VS Code and .NET CLI

To debug the application using VS Code and the .NET CLI:

1. Open the project folder in VS Code:
   ```bash
   code .
   ```

2. Install the following VS Code extensions if you haven't already:
   - C# Dev Kit
   - .NET Runtime Install Tool
   - C# Extensions

3. Create a launch configuration for debugging by adding a `.vscode/launch.json` file with the following content:
   ```json
   {
     "version": "0.2.0",
     "configurations": [
       {
         "name": ".NET Core Launch (web)",
         "type": "coreclr",
         "request": "launch",
         "preLaunchTask": "build",
         "program": "${workspaceFolder}/EntitledLearning.Tools.UI/bin/Debug/net8.0/EntitledLearning.Tools.UI.dll",
         "args": [],
         "cwd": "${workspaceFolder}/EntitledLearning.Tools.UI",
         "stopAtEntry": false,
         "serverReadyAction": {
           "action": "openExternally",
           "pattern": "\\bNow listening on:\\s+(https?://\\S+)"
         },
         "env": {
           "ASPNETCORE_ENVIRONMENT": "Development"
         },
         "sourceFileMap": {
           "/Views": "${workspaceFolder}/Views"
         }
       }
     ]
   }
   ```

4. Create a tasks configuration by adding a `.vscode/tasks.json` file:
   ```json
   {
     "version": "2.0.0",
     "tasks": [
       {
         "label": "build",
         "command": "dotnet",
         "type": "process",
         "args": [
           "build",
           "${workspaceFolder}/EntitledLearning.Tools.UI/EntitledLearning.Tools.UI.csproj",
           "/property:GenerateFullPaths=true",
           "/consoleloggerparameters:NoSummary"
         ],
         "problemMatcher": "$msCompile"
       }
     ]
   }
   ```

5. Set breakpoints in your code by clicking in the margin next to the line numbers.

6. Press F5 or go to the Run and Debug view (Ctrl+Shift+D) and click the green play button.

#### Debugging with Hot Reload

To enable hot reload while debugging in VS Code:

1. Modify your `.vscode/launch.json` file to include hot reload capabilities:
   ```json
   {
     "version": "0.2.0",
     "configurations": [
       {
         "name": ".NET Core Launch with Hot Reload",
         "type": "coreclr",
         "request": "launch",
         "preLaunchTask": "build",
         "program": "dotnet",
         "args": [
           "watch",
           "--project",
           "${workspaceFolder}/EntitledLearning.Tools.UI/EntitledLearning.Tools.UI.csproj",
           "--verbose"
         ],
         "cwd": "${workspaceFolder}/EntitledLearning.Tools.UI",
         "stopAtEntry": false,
         "serverReadyAction": {
           "action": "openExternally",
           "pattern": "\\bNow listening on:\\s+(https?://\\S+)"
         },
         "env": {
           "ASPNETCORE_ENVIRONMENT": "Development",
           "DOTNET_WATCH_RESTART_ON_RUDE_EDIT": "true"
         },
         "sourceFileMap": {
           "/Views": "${workspaceFolder}/Views"
         }
       },
       {
         "name": ".NET Core Launch (web)",
         "type": "coreclr",
         "request": "launch",
         "preLaunchTask": "build",
         "program": "${workspaceFolder}/EntitledLearning.Tools.UI/bin/Debug/net8.0/EntitledLearning.Tools.UI.dll",
         "args": [],
         "cwd": "${workspaceFolder}/EntitledLearning.Tools.UI",
         "stopAtEntry": false,
         "serverReadyAction": {
           "action": "openExternally",
           "pattern": "\\bNow listening on:\\s+(https?://\\S+)"
         },
         "env": {
           "ASPNETCORE_ENVIRONMENT": "Development"
         },
         "sourceFileMap": {
           "/Views": "${workspaceFolder}/Views"
         }
       }
     ]
   }
   ```

2. Select the ".NET Core Launch with Hot Reload" configuration from the debugging dropdown in VS Code.

3. Start debugging (F5). This will launch the application using `dotnet watch`, which provides hot reload capabilities. Now you can:
   - Set breakpoints in your code
   - Make changes to your code while debugging
   - See your changes applied immediately without having to restart the debugging session

4. For some changes (like adding new fields to a class), you might see the message "Hot Reload: Applying changes..." in the debug console, but the application will need to restart to apply them. This restart is handled automatically by the watch process.

Note: The hot reload experience may vary depending on the type of changes you make. Simple changes like modifying method bodies or Razor markup are most likely to work without requiring a restart.

7. Alternative CLI debugging approach:
   ```bash
   cd EntitledLearning.Tools.UI
   dotnet watch run
   ```
   
   This will enable hot reload and you can use the browser's developer tools to debug the client-side aspects of the Blazor app.

## Project Structure

- **Pages**: Contains the Razor pages that make up the UI
- **Shared**: Contains shared components used throughout the application
- **Models**: Data models used by the application
- **Repository**: Data access layer components

## Troubleshooting

### Common Issues

1. **Database Connection Issues**:
   - Verify your connection string is correct
   - Ensure your SQL Server is running and accessible
   - Check firewall settings if using a remote database

2. **Authentication Issues**:
   - Verify the redirect URLs in your Azure AD app registration
   - Check that the Client ID and Tenant ID are correct in your config

3. **Blob Storage Issues**:
   - Ensure your storage account exists and is accessible
   - Verify your storage connection string

### Logging

The application uses Serilog for logging. Logs are stored in the `eltools_application_logs` directory in the application root. 

## Additional Resources

- [Blazor Documentation](https://docs.microsoft.com/en-us/aspnet/core/blazor/)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [Azure Blob Storage Documentation](https://docs.microsoft.com/en-us/azure/storage/blobs/)
- [Microsoft Identity Web Documentation](https://docs.microsoft.com/en-us/azure/active-directory/develop/microsoft-identity-web)