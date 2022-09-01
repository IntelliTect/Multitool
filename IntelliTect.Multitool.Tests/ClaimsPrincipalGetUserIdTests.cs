using System;
using System.Security.Claims;
using System.Security.Principal;
using IntelliTect.Multitool.Security;
using Xunit;

namespace IntelliTect.Multitool.Tests
{
    public class ClaimsPrincipalGetUserIdTests
    {
        [Fact]
        public void WhenClaimsPrincipalNull_Should_Throw()
        {
            ClaimsPrincipal sut = null;

            Assert.Throws<ArgumentNullException>(() => sut.GetUserId());
        }

        [Fact]
        public void WhenClaimsPrincipalHasNoProperty_Should_ReturnNull()
        {
            ClaimsPrincipal sut = new ClaimsPrincipal();

            Assert.Null(sut.GetUserId());
        }

        [Fact]
        public void WhenClaimsPrincipalHasId_Should_ReturnString()
        {
            ClaimsPrincipal sut = new GenericPrincipal(new GenericIdentity("Taki The Frog"), new []{ "Bar" } );

            Assert.Equal("Taki The Frog", sut.GetUserId());
        }
    }
}