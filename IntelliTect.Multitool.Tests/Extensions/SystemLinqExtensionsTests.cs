using Xunit;

namespace IntelliTect.Multitool.Extensions.Tests;

public class SystemLinqExtensionsTests
{
    private static readonly List<string?> stringsWithSomeNullValues = ["Hello", null, "World", null, "!"];

    [Fact]
    public void WhereNotNull_ReturnsOnlyNonNullValues()
    {
        IEnumerable<string> nonNullableStringType = stringsWithSomeNullValues.WhereNotNull();
        Assert.All(stringsWithSomeNullValues.WhereNotNull(), s => Assert.NotNull(s));
        Assert.Equal(new List<string> { "Hello", "World", "!" }, nonNullableStringType);
    }
}
