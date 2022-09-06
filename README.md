# [IntelliTect.Multitool](https://www.nuget.org/packages/IntelliTect.Multitool/): [![NuGet](https://img.shields.io/nuget/v/IntelliTect.Multitool.svg)](https://www.nuget.org/packages/IntelliTect.Multitool/)

## Namespaces within this library

-------

### IntelliTect.Multitool

* AssemblyInfo: Gets an assembly's linker date/time as shown in [IntelliTect's Blog](https://intellitect.com/blog/displaying-deploymentbuild-date-web-pages/).
  * Example Usage:

  ```csharp
  // This example is in cshtml.
  @(AssemblyInfo.Date.ToString("yyyy-MM-dd HH-mm"))
  ```

* RepositoryPaths: Provides consistent environment-independent normalized pathing within a repository.
  * Example Usage:

  ```csharp
  // In this case, the GetDefaultRepoRoot() method can be used to get the root of a repository.
  string fullPathToTheFile = Path.Combine(RepositoryPaths.GetDefaultRepoRoot(), "TheFile.txt");
  ```

### IntelliTect.Multitool.Security

* ClaimsPrincipalExtensions: Extention methods to get a user ID and roles.

## Contributing

-------

See the CONTRIBUTING.md file [here](https://github.com/IntelliTect/Multitool/blob/main/CONTRIBUTING.md).
