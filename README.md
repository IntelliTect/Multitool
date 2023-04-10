# [IntelliTect.Multitool](https://www.nuget.org/packages/IntelliTect.Multitool/): [![NuGet](https://img.shields.io/nuget/v/IntelliTect.Multitool.svg)](https://www.nuget.org/packages/IntelliTect.Multitool/)

## ReleaseDateAttribute - Gets an UTC DateTime of compile time. Allows us to determine the build date/time

### Samples:

- Assignment of GetReleaseDate() to a local variable

  ```cs
  DateTime? date = IntelliTect.Multitool.ReleaseDateAttribute.GetReleaseDate(); // Returns a datetime in UTC to date
  ```
  
- Displaying GetReleaseDate() on a cshtml page

  ```cshtml
  // This example is in cshtml.
  @IntelliTect.Multitool.ReleaseDateAttribute.GetReleaseDate() // Returns a datetime in UTC
  ```
  
- Converting and displaying GetReleaseDate() on a cshtml page

  ```cshtml
  // convert this UTC DateTime object into one for my local timezone that is formatted in a “d MMM, yyyy h:mm:ss tt” (ex: 8 Feb, 2023 11:36:31 AM).
  // The following code will format the date and convert it to my local time zone of Pacific Standard Time. 
  Build: @if (IntelliTect.Multitool.ReleaseDateAttribute.GetReleaseDate() is DateTime date)
  {
    @TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time")).ToString("d MMM, yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
  }
  // Result is "Build: 8 Feb, 2023 11:36:31 AM"
  ```

## RepositoryPaths - Provides consistent environment-independent normalized pathing within a git repository

### Samples:

- Get file path from the root of a repository

  ```csharp
  // In this case, the GetDefaultRepoRoot() method can be used to get the root of a repository.
  string fullPathToTheFile = Path.Combine(IntelliTect.Multitool.RepositoryPaths.GetDefaultRepoRoot(), "TheFile.txt");
  ```

## Security

- ClaimsPrincipalExtensions: Extension methods to get a user ID and roles.

## Contributing

-------

See the CONTRIBUTING.md file [here](https://github.com/IntelliTect/Multitool/blob/main/CONTRIBUTING.md).
