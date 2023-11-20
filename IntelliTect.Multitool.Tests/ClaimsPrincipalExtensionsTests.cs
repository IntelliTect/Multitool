using System.Security.Claims;
using System.Security.Principal;
using Xunit;

namespace IntelliTect.Multitool.Security.Tests;

public class ClaimsPrincipalExtensionsTests
{
    static readonly string[] roles = new[] { "Foo", "Bar" };
    static readonly string[] singleRole = new[] { "Bar" };

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
        ClaimsPrincipal sut = new GenericPrincipal(new GenericIdentity("Uncle Festus"), roles);

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
        ClaimsPrincipal sut = new GenericPrincipal(new GenericIdentity("Taki The Frog"), singleRole);

        Assert.Equal("Taki The Frog", sut.GetUserId());
    }
}