using Xunit;

namespace IntelliTect.Multitool.Extensions.Tests
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData(" ExtraSpacing ", "extraspacing")]
        [InlineData("Hello World", "hello-world")]
        [InlineData("Coding the Publish–Subscribe Pattern with Multicast Delegates", "coding-the-publish-subscribe-pattern-with-multicast-delegates")]
        [InlineData("C#", "c")]
        [InlineData("C# Syntax Fundamentals", "c-syntax-fundamentals")]
        [InlineData("C#_Syntax_Fundamentals", "c-syntax-fundamentals")]
        [InlineData("C# Syntax_Fundamentals-for-me", "c-syntax-fundamentals-for-me")]
        [InlineData("Bitwise Operators (<<, >>, |, &, ^, ~)", "bitwise-operators")]
        [InlineData(".NET Standard", "net-standard")]
        [InlineData("Working with System.Threading", "working-with-system-threading")]
        [InlineData("á, Working í,withú, System.Thróeading", "working-with-system-threading")]
        public void SanitizeStringToOnlyHaveDashesAndLowerCase(string actual, string sanitized)
        {
            Assert.Equal(sanitized, actual.Slugify());
        }
    }
}
