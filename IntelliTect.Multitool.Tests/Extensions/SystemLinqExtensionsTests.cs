using Xunit;

namespace IntelliTect.Multitool.Extensions.Tests;

public class SystemLinqExtensionsTests
{
    private static readonly List<string?> _stringsWithSomeNullValues = ["Hello", null, "World", null, "!"];

    [Fact]
    public void WhereNotNull_ReturnsOnlyNonNullValues()
    {
        IEnumerable<string> nonNullableStringType = _stringsWithSomeNullValues.WhereNotNull();
        Assert.All(_stringsWithSomeNullValues.WhereNotNull(), s => Assert.NotNull(s));
        Assert.Equal(["Hello", "World", "!" ], nonNullableStringType);
    }
}
