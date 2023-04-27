using System.Security.Claims;
using System.Security.Principal;
using Xunit;

namespace IntelliTect.Multitool.Extensions.Tests;

public class ClaimsPrincipalExtensionsTests
{
    [Fact]
    public void GetRoles_WhenClaimsPrincipalNull_Should_Throw()
    {
        ClaimsPrincipal? sut = null;

        Assert.Throws<ArgumentNullException>(() => sut!.GetRoles());
    }

    [Fact]
    public void GetRoles_WhenClaimsPrincipalHasNoRoles_Should_ReturnEmpty()
    {
        ClaimsPrincipal sut = new();

        Assert.Empty(sut.GetRoles());
    }

    [Fact]
    public void GetRoles_WhenClaimsPrincipalHasRoles_Should_ReturnNotEmpty()
    {
        ClaimsPrincipal sut = new GenericPrincipal(new GenericIdentity("Uncle Festus"), new[] { "Foo", "Bar" });

        Assert.Collection(sut.GetRoles(), s => Assert.Equal("Foo", s), t => Assert.Equal("Bar", t));
    }

    [Fact]
    public void GetUserId_WhenClaimsPrincipalNull_Should_Throw()
    {
        ClaimsPrincipal? sut = null;

        Assert.Throws<ArgumentNullException>(() => sut!.GetUserId());
    }

    [Fact]
    public void GetUserId_WhenClaimsPrincipalHasNoProperty_Should_ReturnNull()
    {
        ClaimsPrincipal sut = new();

        Assert.Null(sut.GetUserId());
    }

    [Fact]
    public void GetUserId_WhenClaimsPrincipalHasId_Should_ReturnString()
    {
        ClaimsPrincipal sut = new GenericPrincipal(new GenericIdentity("Taki The Frog"), new[] { "Bar" });

        Assert.Equal("Taki The Frog", sut.GetUserId());
    }
}