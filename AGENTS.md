# Repository Guidelines

## Project Structure & Module Organization

This repository contains one .NET 8 NUnit test project for the Cargoflash nGen DTD web application.

- `Drivers/`: WebDriver lifecycle and browser startup code.
- `Pages/`: Selenium page objects, such as `LoginPage.cs`.
- `Tests/`: NUnit test fixtures and test cases.
- `Utilities/`: Shared helpers, including explicit waits.
- `ExcelFiles/`: Excel-backed test data and its path configuration.
- `Cargoflash.nGen.DTD.Automation.csproj`: NuGet dependencies and test-project settings.

Treat `.vs/`, `bin/`, `obj/`, and `TestResults/` as generated output. Do not add new source files under these directories.

## Build, Test, and Development Commands

Run commands from the repository root:

```powershell
dotnet restore
dotnet build Cargoflash.nGen.DTD.sln
dotnet test Cargoflash.nGen.DTD.sln
dotnet test --filter "FullyQualifiedName~LoginTests"
dotnet format --verify-no-changes
```

`restore` downloads packages, `build` compiles the solution, and `test` runs NUnit tests. Use `--filter` while developing a focused test. The Selenium tests require Chrome, access to the configured test environment, and valid test credentials.

## Coding Style & Naming Conventions

Use four-space indentation and standard C# conventions: PascalCase for classes, methods, and public members; camelCase for parameters and local variables; and `_camelCase` for private fields. Name page objects `<Feature>Page` and test fixtures `<Feature>Tests`. Keep locators private inside page objects and keep assertions in test classes. Prefer explicit waits over `Thread.Sleep` and avoid duplicating browser or data-access logic.

Remove unused imports and commented-out implementations before submitting changes. Use nullable annotations rather than suppressing warnings without justification.

## Testing Guidelines

NUnit is the test framework, with Selenium WebDriver for browser automation and ExcelDataReader for workbook data. Name tests after observable behavior, for example `Login_WithValidCredentials_OpensDashboard`. Every UI test should have a clear assertion and must clean up its driver in teardown. There is currently no formal coverage threshold; add tests for new behavior and relevant failure paths. Never print passwords or other secrets in test output.

## Commit & Pull Request Guidelines

History currently contains only `Initial project setup with Selenium, C#, and NUnit`, so no detailed convention is established. Use short, imperative subjects such as `Add login failure validation` and keep each commit focused.

Pull requests should explain the behavior changed, list test commands and results, link the relevant issue, and note required environment or test-data changes. Include screenshots only when visible UI behavior or failure evidence is relevant. Do not commit credentials, machine-specific absolute paths, browser binaries, or generated build artifacts.
