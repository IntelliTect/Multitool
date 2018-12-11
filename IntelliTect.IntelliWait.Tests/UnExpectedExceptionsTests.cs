using System;
using System.Threading.Tasks;
using Xunit;

namespace IntelliTect.IntelliWait.Tests
{
    public class UnExpectedExceptionsTests : BaseTest
    {
        [Fact]
        public async Task CheckActionParamsWithNoExceptionArgumentThrowsException()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => Poll.UntilNoExceptions(
                    () => Test.CheckExceptionsVoidReturn(1, 1), TimeSpan.FromSeconds(2)));
        }

        [Fact]
        public async Task CheckFuncParamsForNoExceptionArgumentThrowsException()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => Poll.UntilNoExceptions(
                    () => Test.CheckExceptionsBoolReturn(1, 1), TimeSpan.FromSeconds(2)));
        }

        [Fact]
        public async Task CheckActionParamsForUnExpectedExpectionThrows()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => Poll.UntilNoExceptions(
                    () => Test.CheckExceptionsVoidReturn(1, 1), TimeSpan.FromSeconds(2), typeof(NullReferenceException)));
        }

        [Fact]
        public async Task CheckFuncParamsForUnExpectedExpectionThrows()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => Poll.UntilNoExceptions(
                    () => Test.CheckExceptionsBoolReturn(1, 1), TimeSpan.FromSeconds(2), typeof(NullReferenceException)));
        }

        [Fact]
        public async Task CheckActionGenericForOneUnExpectedExpectionThrows()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => Poll.UntilNoExceptions<NullReferenceException>(
                    () => Test.CheckExceptionsVoidReturn(1, 1), TimeSpan.FromSeconds(2)));
        }

        [Fact]
        public async Task CheckFuncGenericForOneUnExpectedExpectionThrows()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => Poll.UntilNoExceptions<NullReferenceException>(
                    () => Test.CheckExceptionsBoolReturn(1, 1), TimeSpan.FromSeconds(2)));
        }

        [Fact]
        public async Task CheckActionGenericForTwoUnExpectedExpectionsThrows()
        {
            await Assert.ThrowsAsync<InvalidProgramException>(
                () => Poll.UntilNoExceptions<NullReferenceException, InvalidOperationException>(
                    () => Test.CheckExceptionsVoidReturn(1, 2), TimeSpan.FromSeconds(2)));
        }

        [Fact]
        public async Task CheckFuncGenericForTwoUnExpectedExpectionsThrows()
        {
            await Assert.ThrowsAsync<InvalidProgramException>(
                () => Poll.UntilNoExceptions<NullReferenceException, InvalidOperationException>(
                    () => Test.CheckExceptionsBoolReturn(1, 2), TimeSpan.FromSeconds(2)));
        }

        [Fact]
        public async Task CheckActionGenericForThreeUnExpectedExpectionsThrows()
        {
            await Assert.ThrowsAsync<IndexOutOfRangeException>(
                () => Poll.UntilNoExceptions<NullReferenceException, InvalidOperationException, InvalidProgramException>(
                    () => Test.CheckExceptionsVoidReturn(1, 3), TimeSpan.FromSeconds(2)));
        }

        [Fact]
        public async Task CheckFuncGenericForThreeUnExpectedExpectionsThrows()
        {
            await Assert.ThrowsAsync<IndexOutOfRangeException>(
                () => Poll.UntilNoExceptions<NullReferenceException, InvalidOperationException, InvalidProgramException>(
                    () => Test.CheckExceptionsBoolReturn(1, 3), TimeSpan.FromSeconds(2)));
        }

        [Fact]
        public async Task CheckActionGenericForFourUnExpectedExpectionsThrows()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => Poll.UntilNoExceptions<NullReferenceException, InvalidOperationException, IndexOutOfRangeException, InvalidProgramException>(
                    () => Test.CheckExceptionsVoidReturn(1, 4), TimeSpan.FromSeconds(2)));
        }

        [Fact]
        public async Task CheckFuncGenericForFourUnExpectedExpectionsThrows()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => Poll.UntilNoExceptions<NullReferenceException, InvalidOperationException, IndexOutOfRangeException, InvalidProgramException>(
                    () => Test.CheckExceptionsBoolReturn(1, 4), TimeSpan.FromSeconds(2)));
        }

        [Fact]
        public async Task CheckActionGenericForFiveUnExpectedExpectionsThrows()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => Poll.UntilNoExceptions<NullReferenceException, InvalidOperationException, IndexOutOfRangeException, InvalidProgramException, FieldAccessException>(
                    () => Test.CheckExceptionsVoidReturn(1, 5), TimeSpan.FromSeconds(2)));
        }

        [Fact]
        public async Task CheckFuncGenericForFiveUnExpectedExpectionsThrows()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => Poll.UntilNoExceptions<NullReferenceException, InvalidOperationException, IndexOutOfRangeException, InvalidProgramException, FieldAccessException>(
                    () => Test.CheckExceptionsBoolReturn(1, 5), TimeSpan.FromSeconds(2)));
        }
    }
}
