using System;
using System.Threading.Tasks;
using Xunit;

namespace IntelliTect.IntelliWait.Tests
{
    public class NoExceptionThrownTests : BaseTest
    {
        [Fact]
        public async Task CheckActionParamsForExpectedExpectionDoesNotThrow()
        {
            await Poll.UntilNoExceptions(() => CheckExceptionsVoidReturn(1, 1), TimeSpan.FromSeconds(2), typeof(InvalidOperationException));
        }

        [Fact]
        public async Task CheckFuncParamsForExpectedExpectionDoesNotThrow()
        {
            await Poll.UntilNoExceptions(() => CheckExceptionsBoolReturn(1, 1), TimeSpan.FromSeconds(2), typeof(InvalidOperationException));
        }

        [Fact]
        public async Task CheckActionsGenericForOneExpectedExceptionDoesNotThrow()
        {
            await Poll.UntilNoExceptions<InvalidOperationException>(() => CheckExceptionsVoidReturn(1, 1), TimeSpan.FromSeconds(2));
        }

        [Fact]
        public async Task CheckFuncGenericForOneExpectedExceptionDoesNotThrow()
        {
            await Poll.UntilNoExceptions<InvalidOperationException>(() => CheckExceptionsBoolReturn(1, 1), TimeSpan.FromSeconds(2));
        }

        [Fact]
        public async Task CheckActionsGenericForTwoExpectedExceptiosnDoesNotThrow()
        {
            await Poll.UntilNoExceptions<InvalidOperationException, InvalidProgramException>(() => CheckExceptionsVoidReturn(1, 2), TimeSpan.FromSeconds(2));
        }

        [Fact]
        public async Task CheckFuncGenericForTwoExpectedExceptionsDoesNotThrow()
        {
            await Poll.UntilNoExceptions<InvalidOperationException, InvalidProgramException>(() => CheckExceptionsBoolReturn(1, 2), TimeSpan.FromSeconds(2));
        }

        [Fact]
        public async Task CheckActionsGenericForThreeExpectedExceptiosnDoesNotThrow()
        {
            await Poll.UntilNoExceptions<InvalidOperationException, InvalidProgramException, IndexOutOfRangeException>(() => CheckExceptionsVoidReturn(1, 3), TimeSpan.FromSeconds(2));
        }

        [Fact]
        public async Task CheckFuncGenericForThreeExpectedExceptionsDoesNotThrow()
        {
            await Poll.UntilNoExceptions<InvalidOperationException, InvalidProgramException, IndexOutOfRangeException>(() => CheckExceptionsBoolReturn(1, 3), TimeSpan.FromSeconds(2));
        }

        [Fact]
        public async Task CheckActionsGenericForFourExpectedExceptiosnDoesNotThrow()
        {
            await Poll.UntilNoExceptions<InvalidOperationException, InvalidProgramException, IndexOutOfRangeException, ArgumentNullException>(() => CheckExceptionsVoidReturn(1, 4), TimeSpan.FromSeconds(2));
        }

        [Fact]
        public async Task CheckFuncGenericForFourExpectedExceptionsDoesNotThrow()
        {
            await Poll.UntilNoExceptions<InvalidOperationException, InvalidProgramException, IndexOutOfRangeException, ArgumentNullException>(() => CheckExceptionsBoolReturn(1, 4), TimeSpan.FromSeconds(2));
        }

        [Fact]
        public async Task CheckActionsGenericForFiveExpectedExceptiosnDoesNotThrow()
        {
            await Poll.UntilNoExceptions<InvalidOperationException, InvalidProgramException, IndexOutOfRangeException, ArgumentNullException, FieldAccessException>(() => CheckExceptionsVoidReturn(1, 5), TimeSpan.FromSeconds(2));
        }

        [Fact]
        public async Task CheckFuncGenericForFiveExpectedExceptionsDoesNotThrow()
        {
            await Poll.UntilNoExceptions<InvalidOperationException, InvalidProgramException, IndexOutOfRangeException, ArgumentNullException, FieldAccessException>(() => CheckExceptionsBoolReturn(1, 5), TimeSpan.FromSeconds(2));
        }
    }
}
