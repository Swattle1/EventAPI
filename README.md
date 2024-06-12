# EventAPI Iventis Technical Task

EventAPI is a RESTful Web API built with c# and SQLite. It fulfills the requirements of the briefing document.

## Prerequisites

- NET 8.0 SDK
- SQLite
- Visual Studio
- Git

## Dependencies

The project uses the following NuGet packages:

- Microsoft.EntityFrameworkCore.Design 8.0.6
- Microsoft.EntityFrameworkCore.InMemory 8.0.6
- Microsoft.EntityFrameworkCore.Sqlite 8.0.6
- Microsoft.EntityFrameworkCore.Tools 8.0.6
- Microsoft.NET.Test.Sdk 17.10.0
- Swashbuckle.AspNetCore 6.4.0
- xunit 2.8.1
- xunit.runner.visualstudio 2.8.1

These packages can be installed via the NuGet package manager in Visual Studio.

## Installation

1. Clone the repository

https://github.com/Swattle1/EventAPI.git

2. Restore the .NET packages if required

dotnet restore

3. Run either the Visual Studio project or in the file with 

dotnet run

## To initialise database 

dotnet ef database update

## Testing

To use the tests, either use test explorer in Visual Studio or run:

dotnet test
