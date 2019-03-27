using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IntelliTect.IntelliWait
{
    [Obsolete("Deprecating in favor of Polly, which is a more robust and flexible polling library: https://github.com/App-vNext/Polly")]
    public static class Wait
    {
        /// <summary>
        /// Repeatedly checks for a condition with void return type until it is satisifed or a timeout is reached
        /// </summary>
        /// <typeparam name="TResult">Return type of the function to evaluate</typeparam>
        /// <param name="func">Function to check for valid evaluation</param>
        /// <param name="timeToWait">Time to try evaluating the given function until an exception is thrown</param>
        /// <param name="exceptionsToIgnore">A list of exceptions to ignore when attempting to evaluate the function</param>
        /// <returns>An async task that can return a value of type TResult</returns>
        [Obsolete("Deprecating Wait.Until() naming convention in favor of more accurate and descriptive Poll.UntilNoExceptions naming")]
        public static Task<TResult> Until<TResult>(Func<TResult> func, TimeSpan timeToWait, params Type[] exceptionsToIgnore)
        {
            return Poll.UntilNoExceptions<TResult>(func, timeToWait, exceptionsToIgnore);
        }

        /// <summary>
        /// Repeatedly checks for a condition with void return type until it is satisifed or a timeout is reached
        /// </summary>
        /// <param name="action">Function to check for valid evaluation</param>
        /// <param name="timeToWait">Time to try evaluating the given function until an exception is thrown</param>
        /// <param name="exceptionsToIgnore">A list of exceptions to ignore when attempting to evaluate the function</param>
        /// <returns>An async task for the operation</returns>
        [Obsolete("Deprecating Wait.Until() naming convention in favor of more accurate and descriptive Poll.UntilNoExceptions naming")]
        public static Task Until(Action action, TimeSpan timeToWait, params Type[] exceptionsToIgnore)
        {
            return Poll.UntilNoExceptions(action, timeToWait, exceptionsToIgnore);
        }

        /// <summary>
        /// Repeatedly checks for a condition with void return type until it is satisifed or a timeout is reached
        /// </summary>
        /// <typeparam name="TException">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TResult">Return type of the function to evaluate</typeparam>
        /// <param name="func">Function to check for valid evaluation</param>
        /// <param name="timeToWait">Time to try evaluating the given function until an exception is thrown</param>
        /// <returns>An async task that can return a value of type TResult</returns>
        [Obsolete("Deprecating Wait.Until() naming convention in favor of more accurate and descriptive Poll.UntilNoExceptions naming")]
        public static Task<TResult> Until<TException, TResult>(Func<TResult> func, TimeSpan timeToWait)
            where TException : Exception
        {
            return Poll.UntilNoExceptions<TException, TResult>(func, timeToWait);
        }

        /// <summary>
        /// Repeatedly checks for a condition with void return type until it is satisifed or a timeout is reached
        /// </summary>
        /// <typeparam name="TException">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <param name="action">Function to check for valid evaluation</param>
        /// <param name="timeToWait">Time to try evaluating the given function until an exception is thrown</param>
        /// <returns>An async task for the operation</returns>
        [Obsolete("Deprecating Wait.Until() naming convention in favor of more accurate and descriptive Poll.UntilNoExceptions naming")]
        public static Task Until<TException>(Action action, TimeSpan timeToWait)
            where TException : Exception
        {
            return Poll.UntilNoExceptions<TException>(action, timeToWait);
        }

        /// <summary>
        /// Repeatedly checks for a condition with void return type until it is satisifed or a timeout is reached
        /// </summary>
        /// <typeparam name="TException1">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TException2">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TResult">Return type of the function to evaluate</typeparam>
        /// <param name="func">Function to check for valid evaluation</param>
        /// <param name="timeToWait">Time to try evaluating the given function until an exception is thrown</param>
        /// <returns>An async task that can return a value of type TResult</returns>
        [Obsolete("Deprecating Wait.Until() naming convention in favor of more accurate and descriptive Poll.UntilNoExceptions naming")]
        public static Task<TResult> Until<TException1, TException2, TResult>(Func<TResult> func, TimeSpan timeToWait)
            where TException1 : Exception
            where TException2 : Exception
        {
            return Poll.UntilNoExceptions<TException1, TException2, TResult>(func, timeToWait);
        }

        /// <summary>
        /// Repeatedly checks for a condition with void return type until it is satisifed or a timeout is reached
        /// </summary>
        /// <typeparam name="TException1">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TException2">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <param name="action">Function to check for valid evaluation</param>
        /// <param name="timeToWait">Time to try evaluating the given function until an exception is thrown</param>
        /// <returns>An async task for the operation</returns>
        [Obsolete("Deprecating Wait.Until() naming convention in favor of more accurate and descriptive Poll.UntilNoExceptions naming")]
        public static Task Until<TException1, TException2>(Action action, TimeSpan timeToWait)
            where TException1 : Exception
            where TException2 : Exception
        {
            return Poll.UntilNoExceptions<TException1, TException2>(action, timeToWait);
        }

        /// <summary>
        /// Repeatedly checks for a condition with void return type until it is satisifed or a timeout is reached
        /// </summary>
        /// <typeparam name="TException1">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TException2">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TException3">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TResult">Return type of the function to evaluate</typeparam>
        /// <param name="func">Function to check for valid evaluation</param>
        /// <param name="timeToWait">Time to try evaluating the given function until an exception is thrown</param>
        /// <returns>An async task that can return a value of type TResult</returns>
        [Obsolete("Deprecating Wait.Until() naming convention in favor of more accurate and descriptive Poll.UntilNoExceptions naming")]
        public static Task<TResult> Until<TException1, TException2, TException3, TResult>(Func<TResult> func, TimeSpan timeToWait)
            where TException1 : Exception
            where TException2 : Exception
            where TException3 : Exception
        {
            return Poll.UntilNoExceptions<TException1, TException2, TException3, TResult>(func, timeToWait);
        }

        /// <summary>
        /// Repeatedly checks for a condition with void return type until it is satisifed or a timeout is reached
        /// </summary>
        /// <typeparam name="TException1">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TException2">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TException3">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <param name="action">Function to check for valid evaluation</param>
        /// <param name="timeToWait">Time to try evaluating the given function until an exception is thrown</param>
        /// <returns>An async task for the operation</returns>
        [Obsolete("Deprecating Wait.Until() naming convention in favor of more accurate and descriptive Poll.UntilNoExceptions naming")]
        public static Task Until<TException1, TException2, TException3>(Action action, TimeSpan timeToWait)
            where TException1 : Exception
            where TException2 : Exception
            where TException3 : Exception
        {
            return Poll.UntilNoExceptions<TException1, TException2, TException3>(action, timeToWait);
        }

        /// <summary>
        /// Repeatedly checks for a condition with void return type until it is satisifed or a timeout is reached
        /// </summary>
        /// <typeparam name="TException1">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TException2">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TException3">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TException4">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TResult">Return type of the function to evaluate</typeparam>
        /// <param name="func">Function to check for valid evaluation</param>
        /// <param name="timeToWait">Time to try evaluating the given function until an exception is thrown</param>
        /// <returns>An async task that can return a value of type TResult</returns>
        [Obsolete("Deprecating Wait.Until() naming convention in favor of more accurate and descriptive Poll.UntilNoExceptions naming")]
        public static Task<TResult> Until<TException1, TException2, TException3, TException4, TResult>(Func<TResult> func, TimeSpan timeToWait)
            where TException1 : Exception
            where TException2 : Exception
            where TException3 : Exception
            where TException4 : Exception
        {
            return Poll.UntilNoExceptions<TException1, TException2, TException3, TException4, TResult>(func, timeToWait);
        }

        /// <summary>
        /// Repeatedly checks for a condition with void return type until it is satisifed or a timeout is reached
        /// </summary>
        /// <typeparam name="TException1">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TException2">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TException3">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TException4">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <param name="action">Function to check for valid evaluation</param>
        /// <param name="timeToWait">Time to try evaluating the given function until an exception is thrown</param>
        /// <returns>An async task for the operation</returns>
        [Obsolete("Deprecating Wait.Until() naming convention in favor of more accurate and descriptive Poll.UntilNoExceptions naming")]
        public static Task Until<TException1, TException2, TException3, TException4>(Action action, TimeSpan timeToWait)
            where TException1 : Exception
            where TException2 : Exception
            where TException3 : Exception
            where TException4 : Exception
        {
            return Poll.UntilNoExceptions<TException1, TException2, TException3, TException4>(action, timeToWait);
        }
    }
}
