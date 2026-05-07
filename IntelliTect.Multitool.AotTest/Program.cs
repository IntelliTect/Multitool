using IntelliTect.Multitool;
using IntelliTect.Multitool.Extensions;

// StringExtensions
bool slugOk = "Hello, World!".CreateUrlSlug() == "hello-world";
bool urlOk = "https://github.com/IntelliTect".ValidateUrlString();

// SystemLinqExtensions
string?[] items = ["a", null, "b", null, "c"];
bool filterOk = items.WhereNotNull().Count() == 3;

// ReleaseDateAttribute — returns null since no attribute on this assembly,
// but exercises the reflection-free GetCustomAttribute<T> code path
DateTime? releaseDate = ReleaseDateAttribute.GetReleaseDate();
bool attributeOk = releaseDate is null;

if (!slugOk || !urlOk || !filterOk || !attributeOk)
{
    Console.Error.WriteLine("AOT test FAILED.");
    return 1;
}

Console.WriteLine("AOT test passed.");
return 0;
