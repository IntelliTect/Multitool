using System;
using System.Threading.Tasks;
using Xunit;

namespace IntelliTect.IntelliWait.Tests
{
    public class TypeCheckingTests : BaseTest
    {
        [Fact]
        public async Task CheckActionParamsForInvalidTypeChecking()
        {
            Exception ex = await Assert.ThrowsAsync<ArgumentException>(
                () => Poll.UntilNoExceptions(Test.ThrowExceptionWithNoReturn, TimeSpan.FromSeconds(1), typeof(string)));
            Assert.Equal(ExceptionMessage, ex.Message);
        }

        [Fact]
        public async Task CheckFuncParamsForInvalidTypeChecking()
        {
            Exception ex = await Assert.ThrowsAsync<ArgumentException>(
                () => Poll.UntilNoExceptions(Test.ThrowExceptionWithReturn, TimeSpan.FromSeconds(1), typeof(string)));
            Assert.Equal(ExceptionMessage, ex.Message);
        }

        [Fact]
        public async Task CheckActionParamsForMixedInvalidTypeChecking()
        {
            Exception ex = await Assert.ThrowsAsync<ArgumentException>(
                () => Poll.UntilNoExceptions(Test.ThrowNullRefExceptionWithNoReturn, TimeSpan.FromSeconds(1), typeof(Exception), typeof(string)));
            Assert.Equal(ExceptionMessage, ex.Message);
        }

        [Fact]
        public async Task CheckFuncParamsForMixedInvalidTypeChecking()
        {
            Exception ex = await Assert.ThrowsAsync<ArgumentException>(
                () => Poll.UntilNoExceptions(Test.ThrowNullRefExceptionWithReturn, TimeSpan.FromSeconds(1), typeof(string), typeof(NullReferenceException)));
            Assert.Equal(ExceptionMessage, ex.Message);
        }

        private const string ExceptionMessage = "Invalid type passed into exceptionsToIgnore parameter. Must be of type Exception.";
    }
}
