# CovertCode
Example Blazor/gRPC solution in C# and ASP.NET Core that allows people to share a secret with someone a single time. 

> Note: This was used for a demo in a recent talk. It is currently incomplete and needs A LOT of work to be considered a real MVP.
> There are a few bugs which I will document on github.

## Requirements
* SQL Server LocalDB
* .net Core 3.0

## Tools Needed
Visual Studio 2019 v16.3 (that includes .net Core 3)

## Preparation needed to get started: 
* Create a database and create table in src\Data\dbo.Secret.sql
* Create UserSecrets entry on the CovertCode.Services.Secret.Api project (AppSettings:DBConnectionString) 
