using System;
using System.Security.Claims;
using IntelliTect.Utilities.Security;
using Xunit;

namespace IntelliTect.Utilities.Tests
{
    public class ClaimsPrincipalGetUserIdTests
    {
        [Fact]
        public void WhenClaimsPrincipalNull_Should_Throw()
        {
            ClaimsPrincipal sut = null;

            Assert.Throws<ArgumentNullException>(() => sut.GetUserId());
        }
    }
}