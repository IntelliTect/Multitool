using System;
using Xunit;

namespace IntelliTect.Multitool.Tests;

public class ReleaseDateAttributeTests
{
    [Fact]
    public void ReleaseDateIsGettingInjected()
    {
        DateTime? releaseDate = ReleaseDateAttribute.GetReleaseDate(GetType().Assembly);
        Assert.NotNull(releaseDate);
        Assert.Equal(DateTimeKind.Utc, releaseDate?.Kind);
        Assert.NotEqual(DateTime.MinValue, releaseDate);
        Assert.NotEqual(DateTime.MaxValue, releaseDate);
    }
}