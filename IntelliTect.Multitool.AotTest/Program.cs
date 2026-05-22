using IntelliTect.Multitool;
using IntelliTect.Multitool.Extensions;

// StringExtensions
bool slugOk = "Hello, World!".CreateUrlSlug() == "hello-world";
bool urlOk = "https://github.com/IntelliTect".ValidateUrlString();

// SystemLinqExtensions
string?[] items = ["a", null, "b", null, "c"];
bool filterOk = items.WhereNotNull().Count() == 3;

// ReleaseDateAttribute — returns null since no attribute on this assembly,
// but exercises the AOT-safe (statically-known type) GetCustomAttribute<T> code path
DateTime? releaseDate = ReleaseDateAttribute.GetReleaseDate();
bool attributeOk = releaseDate is null;

if (!slugOk || !urlOk || !filterOk || !attributeOk)
{
    Console.Error.WriteLine("AOT test FAILED.");
    return 1;
}

// RepositoryPaths is excluded: its static initializer reads a build-time file from the output directory
// that doesn't exist at AOT runtime. The class is AOT-compatible (file I/O + LINQ only).

Console.WriteLine("AOT test passed.");
return 0;
