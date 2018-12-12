using System;
using System.Threading.Tasks;
using Xunit;

namespace IntelliTect.IntelliWait.Tests
{
    public class ExpectedExceptionsTests : BaseTest
    {
        [Fact]
        public async Task CheckActionsParamsForBaseTypeChecking()
        {
            AggregateException ae = await Assert.ThrowsAsync<AggregateException>(() => Poll.UntilNoExceptions(Test.ThrowExceptionWithNoReturn, TimeSpan.FromSeconds(1), typeof(Exception)));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(Exception));
        }

        [Fact]
        public async Task CheckFuncParamsForBaseTypeChecking()
        {
            AggregateException ae = await Assert.ThrowsAsync<AggregateException>(() => Poll.UntilNoExceptions(Test.ThrowExceptionWithReturn, TimeSpan.FromSeconds(1), typeof(Exception)));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(Exception));
        }

        [Fact]
        public async Task CheckActionParamsForMixedTypeChecking()
        {
            AggregateException ae = await Assert.ThrowsAsync<AggregateException>(
                () => Poll.UntilNoExceptions(Test.ThrowExceptionWithNoReturn, TimeSpan.FromSeconds(1), typeof(Exception), typeof(NullReferenceException)));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(Exception));
            // Verify that we only catch the actual exceptions thrown
            Assert.DoesNotContain(ae.InnerExceptions, e => e.GetType() == typeof(NullReferenceException));
        }

        [Fact]
        public async Task CheckFuncParamsForMixedTypeChecking()
        {
            AggregateException ae = await Assert.ThrowsAsync<AggregateException>(
                () => Poll.UntilNoExceptions(Test.ThrowExceptionWithReturn, TimeSpan.FromSeconds(1), typeof(Exception), typeof(NullReferenceException)));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(Exception));
            // Verify that we only catch the actual exceptions thrown
            Assert.DoesNotContain(ae.InnerExceptions, e => e.GetType() == typeof(NullReferenceException));
        }

        [Fact]
        public async Task CheckActionsParamsForOneDerivedTypeChecking()
        {
            AggregateException ae = await Assert.ThrowsAsync<AggregateException>(() => Poll.UntilNoExceptions(() => Test.CheckExceptionsVoidReturn(2, 1), TimeSpan.FromSeconds(1), typeof(InvalidOperationException)));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(InvalidOperationException));
        }

        [Fact]
        public async Task CheckFuncParamsForOneDerivedTypeChecking()
        {
            AggregateException ae = await Assert.ThrowsAsync<AggregateException>(() => Poll.UntilNoExceptions(() => Test.CheckExceptionsBoolReturn(2, 1), TimeSpan.FromSeconds(1), typeof(InvalidOperationException)));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(InvalidOperationException));
        }

        [Fact]
        public async Task CheckActionsGenericForOneDerivedTypeChecking()
        {
            AggregateException ae = await Assert.ThrowsAsync<AggregateException>(() => Poll.UntilNoExceptions<InvalidOperationException>(() => Test.CheckExceptionsVoidReturn(2, 1), TimeSpan.FromSeconds(1)));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(InvalidOperationException));
        }

        [Fact]
        public async Task CheckFuncGenericForOneDerivedTypeChecking()
        {
            AggregateException ae = await Assert.ThrowsAsync<AggregateException>(() => Poll.UntilNoExceptions<InvalidOperationException>(() => Test.CheckExceptionsBoolReturn(2, 1), TimeSpan.FromSeconds(1)));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(InvalidOperationException));
        }

        [Fact]
        public async Task CheckActionsGenericForTwoDerivedTypesChecking()
        {
            AggregateException ae = await Assert.ThrowsAsync<AggregateException>(() => Poll.UntilNoExceptions<InvalidOperationException, InvalidProgramException>(() => Test.CheckExceptionsVoidReturn(2, 2), TimeSpan.FromSeconds(1)));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(InvalidOperationException));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(InvalidProgramException));
        }

        [Fact]
        public async Task CheckFuncGenericForTwoDerivedTypesChecking()
        {
            AggregateException ae = await Assert.ThrowsAsync<AggregateException>(() => Poll.UntilNoExceptions<InvalidOperationException, InvalidProgramException>(() => Test.CheckExceptionsBoolReturn(2, 2), TimeSpan.FromSeconds(1)));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(InvalidOperationException));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(InvalidProgramException));
        }

        [Fact]
        public async Task CheckActionsGenericForThreeDerivedTypesChecking()
        {
            AggregateException ae = await Assert.ThrowsAsync<AggregateException>(() => Poll.UntilNoExceptions<InvalidOperationException, InvalidProgramException, IndexOutOfRangeException>(() => Test.CheckExceptionsVoidReturn(2, 3), TimeSpan.FromSeconds(1)));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(InvalidOperationException));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(InvalidProgramException));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(IndexOutOfRangeException));
        }

        [Fact]
        public async Task CheckFuncGenericForThreeDerivedTypesChecking()
        {
            AggregateException ae = await Assert.ThrowsAsync<AggregateException>(() => Poll.UntilNoExceptions<InvalidOperationException, InvalidProgramException, IndexOutOfRangeException>(() => Test.CheckExceptionsBoolReturn(2, 3), TimeSpan.FromSeconds(1)));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(InvalidOperationException));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(InvalidProgramException));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(IndexOutOfRangeException));
        }

        [Fact]
        public async Task CheckActionsGenericForFourDerivedTypesChecking()
        {
            AggregateException ae = await Assert.ThrowsAsync<AggregateException>(() => 
                Poll.UntilNoExceptions<InvalidOperationException, InvalidProgramException, IndexOutOfRangeException, ArgumentNullException>(() => Test.CheckExceptionsVoidReturn(4, 4), TimeSpan.FromSeconds(3)));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(InvalidOperationException));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(InvalidProgramException));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(IndexOutOfRangeException));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(ArgumentNullException));
        }

        [Fact]
        public async Task CheckFuncGenericForFourDerivedTypesChecking()
        {
            AggregateException ae = await Assert.ThrowsAsync<AggregateException>(() => 
                Poll.UntilNoExceptions<InvalidOperationException, InvalidProgramException, IndexOutOfRangeException, ArgumentNullException>(() => Test.CheckExceptionsBoolReturn(4, 4), TimeSpan.FromSeconds(3)));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(InvalidOperationException));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(InvalidProgramException));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(IndexOutOfRangeException));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(ArgumentNullException));
        }

        [Fact]
        public async Task CheckActionsGenericForFiveDerivedTypesChecking()
        {
            AggregateException ae = await Assert.ThrowsAsync<AggregateException>(() => 
                Poll.UntilNoExceptions<InvalidOperationException, InvalidProgramException, IndexOutOfRangeException, ArgumentNullException, FieldAccessException>(() => Test.CheckExceptionsVoidReturn(5, 5), TimeSpan.FromSeconds(4)));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(InvalidOperationException));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(InvalidProgramException));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(IndexOutOfRangeException));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(ArgumentNullException));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(FieldAccessException));
        }

        [Fact]
        public async Task CheckFuncGenericForFiveDerivedTypesChecking()
        {
            AggregateException ae = await Assert.ThrowsAsync<AggregateException>(() =>
                Poll.UntilNoExceptions<InvalidOperationException, InvalidProgramException, IndexOutOfRangeException, ArgumentNullException, FieldAccessException>(() => Test.CheckExceptionsBoolReturn(5, 5), TimeSpan.FromSeconds(4)));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(InvalidOperationException));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(InvalidProgramException));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(IndexOutOfRangeException));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(ArgumentNullException));
            Assert.Contains(ae.InnerExceptions, e => e.GetType() == typeof(FieldAccessException));
        }
    }
}
