using Xunit;

namespace IntelliTect.Multitool.Extensions.Tests
{
    public class UriExtensionsTests
    {
        [Theory]
        [InlineData("https://thisisaverybadurlthatwillnotworkhopefullyever.com/", false)]
        [InlineData("https://www.google.com/", true)]
        [InlineData("https://api.github.com/meta", true)]
        public async void ValidateUri_CheckUrl_SuccessIsAsExpected(string urlUnderTest, bool expected)
        {
            Assert.Equal(expected, await urlUnderTest.ValidateUrlString(false));
        }
    }
}
