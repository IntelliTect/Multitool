# [IntelliTect.Multitool](https://www.nuget.org/packages/IntelliTect.Multitool/): [![NuGet](https://img.shields.io/nuget/v/IntelliTect.Multitool.svg)](https://www.nuget.org/packages/IntelliTect.Multitool/)

## Install Instructions

1. Add package to project from [nuget.org](https://www.nuget.org/packages/IntelliTect.Multitool/). More instructions to get started with consuming nuget packages can be found on [learn.microsoft.com](https://learn.microsoft.com/nuget/install-nuget-client-tools)
2. All tools are under the `IntelliTect.Multitool` namespace.
3. That's it! Please open an issue if you have any problems with any of these steps or have other questions.

## ReleaseDateAttribute - Gets an UTC DateTime of compile time. Allows us to determine the build date/time

### Blog Post/Additional Information: [How To Display the Build Date of a .NET Application](https://intellitect.com/blog/build-date-net-application/)

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

### Miscellaneous Information:

- To help [hot reload in visual studio work properly](https://github.com/IntelliTect/Multitool/issues/125), the release date attribute will be automatically be generated during compilation on a CI runner, but it won’t generate locally by default. If you want it to generate locally, set the `ReleaseDateAttribute` to true within a property group in your project file. (`<ReleaseDateAttribute>true</ReleaseDateAttribute>`)

## RepositoryPaths - Provides consistent environment-independent normalized pathing within a git repository

### Samples:

- Get file path from the root of a repository

  ```csharp
  // In this case, the GetDefaultRepoRoot() method can be used to get the root of a repository.
  string fullPathToTheFile = Path.Combine(IntelliTect.Multitool.RepositoryPaths.GetDefaultRepoRoot(), "TheFile.txt");
  ```

## Extensions

- StringExtensions: Extension methods for `System.String`
  - `ValidateUrlString`: Extension method to validate a URL string by checking to make sure the string is formatted correctly.
  - `CreateUrlSlug`: Extension method modify a string so that it is URL compatible
- HttpExtensions: Extension methods for `System.Net.Http.HttpClient`
  `ValidateUri`: Extension methods to validate a Uri by attempting to make a GET request to it.
- SystemLinqExtensions
  - `WhereNotNull`: Extension method to allow return of non-null value from a null object.
    - Sample:

      ```csharp
      List<string?> listWithSomeNullValues = ["this", null, "is", null, "my", null, "favorite", null];
      List<string> listWithoutNullValues = listWithSomeNullValues.WhereNotNull().ToList();
      // returns ["this", "is", "my", "favorite"]
      ```

    - On:
      - `System.Linq.Generic.IEnumerable<T>`

## Contributing

See the CONTRIBUTING.md file [here](https://github.com/IntelliTect/Multitool/blob/main/CONTRIBUTING.md).

If you have any problems, please feel free to check for existing [issues](https://github.com/IntelliTect/Multitool/issues) or open a [new issue](https://github.com/IntelliTect/Multitool/issues/new).
