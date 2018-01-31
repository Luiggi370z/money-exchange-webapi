# Money Exchange WebApi

### Quick start

**Make sure you have .NET Framework >= 4.7**

```bash
# clone our repo
# --depth 1 removes all but one .git commit history
git clone --depth 1 https://github.com/Luiggi370z/money-exchange-angular2.git

* Open `.sln` file with Visual Studio.

* Restore Nuget Packages.

* Set `MoneyEchange.WebApi` project as the initial project.

* Run the application (IISExpress) or publish to local IIS or publish to the cloud.
```


A browser will appeared with the default Swagger page to try and inspect the endpoints or go to [http://localhost:52479/swagger/ui/index](http://localhost:52479/swagger/ui/index) in your browser.

> Click on Rates and try it!!.

# Table of Contents
* [Stack](#stack)
* [Patterns](#Patterns)
* [Principles](#Principles)
* [File Structure](#file-structure)
* [Getting Started](#getting-started)


## Stack
* ASP.NET Web API 2.
* C# 7.0.
* Entity Framework 6.x.
* Swagger / Swashbuckle.
* Unity.
* Xunit.
* Moq.
* Newtonsoft.Json.

## Patterns
* DI - Dependency Injection.
* Unity of work.
* Repository.
* CQRS - Event Sourcing and Command and Query Responsibility Segregation.
* Decorator.
* Extension Object.
* Lazy initialization.
* Singleton.

## Principles
* SOLID.

## File Structure
Here's how it looks:
```
money-exchange-webapi/
 ├──MoneyExchange.Data/     * our data project that contains the context to manage and initialize the data, includes Unit of Work.
 │   └──Configurations/     * helper functions to configure entities with Code First.
 │
 ├──MoneyExchange.Model/    * our model project that contains all the model classes of the Business.
 │   └──Exceptions/         * our models for custom exceptions.
 │
 ├──MoneyExchange.Server/   * our server project that contains all the execution of the business logic.
 │   ├──Commands/           * our commands files. (CQRS)
 │   └──Queries/            * our queries files. (CQRS)
 │
 ├──MoneyExchange.WebApi/   * our web api project that hosts all the endpoints.
 │   ├──App_Start/          * our configuration files on start.
 │   ├──Controllers/        * our controllers classes for the actions/endpoints.
 │   └──Filters/            * our filter classes like Exception handler.
 │
 └──MoneyExchange.Tests/    * our test project that includes our unit tests.

```

# Getting Started
## Dependencies
What you need to run this app:
* Microsoft SQL Express or a higher version.
* Microsoft Visual Studio.

> The first time we execute the application it will take some extra seconds to get the first response because the DB willl be created and populated with initial data.