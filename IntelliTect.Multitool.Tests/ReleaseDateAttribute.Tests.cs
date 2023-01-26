using System;
using Xunit;

namespace IntelliTect.Multitool.Tests;

public class ReleaseDateAttributeTests
{
    [Fact]
    public void ReleaseDateIsGettingInjected()
    {
        Assert.NotNull(ReleaseDateAttribute.GetReleaseDate());
    }
    [Fact]
    public void ReleaseDateIsRelativelyCorrectWithinAnHour()
    {
        DateTime now = DateTime.Now;
        Assert.True(ReleaseDateAttribute.GetReleaseDate() > now.AddHours(-1) && ReleaseDateAttribute.GetReleaseDate() <= now.AddHours(1), ReleaseDateAttribute.GetReleaseDate().ToString());
    }
}