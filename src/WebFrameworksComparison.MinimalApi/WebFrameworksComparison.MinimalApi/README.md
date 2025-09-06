# Web Frameworks Comparison Minimal API

## Overview
This project demonstrates the use of Minimal APIs in C#. It provides a lightweight approach to building web APIs with minimal overhead.

## Files
- **appsettings.json**: Contains configuration settings for the application, such as connection strings and application-specific settings.
- **GlobalUsings.cs**: Defines global using directives for the project to reduce repetitive code.
- **Program.cs**: The entry point of the application, responsible for setting up the Minimal API, configuring services, and defining API endpoints.
- **WebFrameworksComparison.MinimalApi.csproj**: Project file containing information about dependencies, target framework, and build settings.
- **WebFrameworksComparison.MinimalApi.http**: Used for testing HTTP requests against the API endpoints defined in the project.
- **Properties/launchSettings.json**: Contains settings for launching the application, including environment variables and profiles for different launch configurations.

## Setup Instructions
1. Clone the repository or download the project files.
2. Open the project in your preferred IDE.
3. Restore the project dependencies using the command:
   ```
   dotnet restore
   ```
4. Run the application using the command:
   ```
   dotnet run
   ```

## Usage Examples
Once the application is running, you can access the API endpoints defined in `Program.cs`. Use tools like Postman or curl to test the endpoints.

## Contributing
Feel free to submit issues or pull requests to improve the project.