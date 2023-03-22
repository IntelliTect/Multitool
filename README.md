# [IntelliTect.Multitool](https://www.nuget.org/packages/IntelliTect.Multitool/): [![NuGet](https://img.shields.io/nuget/v/IntelliTect.Multitool.svg)](https://www.nuget.org/packages/IntelliTect.Multitool/)

## Namespaces within this library

-------

### IntelliTect.Multitool

* ReleaseDateAttribute: Gets an UTC DateTime from compile time. Allows us to determine the build date/time.
  * Example Usage:
  
  ```cs
  // Simple assignment of GetReleaseDate() to a local variable
  DateTime? date = IntelliTect.Multitool.ReleaseDateAttribute.GetReleaseDate(); // Returns a datetime in UTC to date
  ```
  
  ```cshtml
  // This example is in cshtml.
  @ReleaseDateAttribute.GetReleaseDate() // Returns a datetime in UTC
  ```
  
  ```cshtml
  // convert this UTC DateTime object into one for my local timezone that is formatted in a “d MMM, yyyy h:mm:ss tt” (ex: 8 Feb, 2023 11:36:31 AM).
  // The following code will format the date and convert it to my local time zone of Pacific Standard Time. 
  Build: @if (IntelliTect.Multitool.ReleaseDateAttribute.GetReleaseDate() is DateTime date)
  {
    @TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time")).ToString("d MMM, yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
  }
  // Result is "Build: 8 Feb, 2023 11:36:31 AM"
    ```

* RepositoryPaths: Provides consistent environment-independent normalized pathing within a repository.
  * Example Usage:

  ```csharp
  // In this case, the GetDefaultRepoRoot() method can be used to get the root of a repository.
  string fullPathToTheFile = Path.Combine(IntelliTect.Multitool.RepositoryPaths.GetDefaultRepoRoot(), "TheFile.txt");
  ```

### IntelliTect.Multitool.Security

* ClaimsPrincipalExtensions: Extention methods to get a user ID and roles.

## Contributing

-------

See the CONTRIBUTING.md file [here](https://github.com/IntelliTect/Multitool/blob/main/CONTRIBUTING.md).
