using System;
using System.Security.Claims;
using System.Security.Principal;
using IntelliTect.Multitool.Security;
using Xunit;

namespace IntelliTect.Multitool.Tests
{
    public class ClaimsPrincipalGetRolesTests
    {
        [Fact]
        public void WhenClaimsPrincipalNull_Should_Throw()
        {
            ClaimsPrincipal sut = null;

            Assert.Throws<ArgumentNullException>(() => sut.GetRoles());
        }

        [Fact]
        public void WhenClaimsPrincipalHasNoRoles_Should_ReturnEmpty()
        {
            ClaimsPrincipal sut = new ClaimsPrincipal();

            Assert.Empty(sut.GetRoles());
        }

        [Fact]
        public void WhenClaimsPrincipalHasRoles_Should_ReturnNotEmpty()
        {
            ClaimsPrincipal sut = new GenericPrincipal(new GenericIdentity("Uncle Festus"), new[] {"Foo", "Bar"});

            Assert.Collection(sut.GetRoles(), s => Assert.Equal("Foo", s), t => Assert.Equal("Bar", t) );
        }
    }
}