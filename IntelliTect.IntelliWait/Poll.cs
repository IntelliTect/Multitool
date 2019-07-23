using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IntelliTect.IntelliWait.Properties;

namespace IntelliTect.IntelliWait
{
    [Obsolete("Deprecating in favor of Polly, which is a more robust and flexible polling library: https://github.com/App-vNext/Polly")]
    public static class Poll
    {
        /// <summary>
        /// Repeatedly checks for a condition with void return type until it is satisifed or a timeout is reached
        /// </summary>
        /// <typeparam name="TResult">Return type of the function to evaluate</typeparam>
        /// <param name="func">Function to check for valid evaluation</param>
        /// <param name="timeToWait">Time to try evaluating the given function until an exception is thrown</param>
        /// <param name="exceptionsToIgnore">A list of exceptions to ignore when attempting to evaluate the function</param>
        /// <returns>An async task that can return a value of type TResult</returns>
        public static Task<TResult> UntilNoExceptions<TResult>(Func<TResult> func, TimeSpan timeToWait, params Type[] exceptionsToIgnore)
        {
            VerifyAllExceptionTypes(exceptionsToIgnore);
            return ExecutePollingFunction(func, timeToWait, exceptionsToIgnore);
        }

        /// <summary>
        /// Repeatedly checks for a condition with void return type until it is satisifed or a timeout is reached
        /// </summary>
        /// <param name="action">Function to check for valid evaluation</param>
        /// <param name="timeToWait">Time to try evaluating the given function until an exception is thrown</param>
        /// <param name="exceptionsToIgnore">A list of exceptions to ignore when attempting to evaluate the function</param>
        /// <returns>An async task for the operation</returns>
        public static Task UntilNoExceptions(Action action, TimeSpan timeToWait, params Type[] exceptionsToIgnore)
        {
            VerifyAllExceptionTypes(exceptionsToIgnore);
            return ExecutePollingFunction(action, timeToWait, exceptionsToIgnore);
        }

        /// <summary>
        /// Repeatedly checks for a condition with void return type until it is satisifed or a timeout is reached
        /// </summary>
        /// <typeparam name="TException">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TResult">Return type of the function to evaluate</typeparam>
        /// <param name="func">Function to check for valid evaluation</param>
        /// <param name="timeToWait">Time to try evaluating the given function until an exception is thrown</param>
        /// <returns>An async task that can return a value of type TResult</returns>
        public static Task<TResult> UntilNoExceptions<TException, TResult>(Func<TResult> func, TimeSpan timeToWait)
            where TException : Exception
        {
            return ExecutePollingFunction(func, timeToWait, typeof(TException));
        }

        /// <summary>
        /// Repeatedly checks for a condition with void return type until it is satisifed or a timeout is reached
        /// </summary>
        /// <typeparam name="TException">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <param name="action">Function to check for valid evaluation</param>
        /// <param name="timeToWait">Time to try evaluating the given function until an exception is thrown</param>
        /// <returns>An async task for the operation</returns>
        public static Task UntilNoExceptions<TException>(Action action, TimeSpan timeToWait)
            where TException : Exception
        {
            return ExecutePollingFunction(action, timeToWait, typeof(TException));
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
        public static Task<TResult> UntilNoExceptions<TException1, TException2, TResult>(Func<TResult> func, TimeSpan timeToWait)
            where TException1 : Exception
            where TException2 : Exception
        {
            return ExecutePollingFunction(func, timeToWait, typeof(TException1), typeof(TException2));
        }

        /// <summary>
        /// Repeatedly checks for a condition with void return type until it is satisifed or a timeout is reached
        /// </summary>
        /// <typeparam name="TException1">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TException2">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <param name="action">Function to check for valid evaluation</param>
        /// <param name="timeToWait">Time to try evaluating the given function until an exception is thrown</param>
        /// <returns>An async task for the operation</returns>
        public static Task UntilNoExceptions<TException1, TException2>(Action action, TimeSpan timeToWait)
            where TException1 : Exception
            where TException2 : Exception
        {
            return ExecutePollingFunction(action, timeToWait, typeof(TException1), typeof(TException2));
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
        public static Task<TResult> UntilNoExceptions<TException1, TException2, TException3, TResult>(Func<TResult> func, TimeSpan timeToWait)
            where TException1 : Exception
            where TException2 : Exception
            where TException3 : Exception
        {
            return ExecutePollingFunction(func, timeToWait, typeof(TException1), typeof(TException2), typeof(TException3));
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
        public static Task UntilNoExceptions<TException1, TException2, TException3>(Action action, TimeSpan timeToWait)
            where TException1 : Exception
            where TException2 : Exception
            where TException3 : Exception
        {
            return ExecutePollingFunction(action, timeToWait, typeof(TException1), typeof(TException2), typeof(TException3));
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
        public static Task<TResult> UntilNoExceptions<TException1, TException2, TException3, TException4, TResult>(Func<TResult> func, TimeSpan timeToWait)
            where TException1 : Exception
            where TException2 : Exception
            where TException3 : Exception
            where TException4 : Exception
        {
            return ExecutePollingFunction(func, timeToWait, typeof(TException1), typeof(TException2), typeof(TException3), typeof(TException4));
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
        public static Task UntilNoExceptions<TException1, TException2, TException3, TException4>(Action action, TimeSpan timeToWait)
            where TException1 : Exception
            where TException2 : Exception
            where TException3 : Exception
            where TException4 : Exception
        {
            return ExecutePollingFunction(action, timeToWait, typeof(TException1), typeof(TException2), typeof(TException3), typeof(TException4));
        }

        /// <summary>
        /// Repeatedly checks for a condition with void return type until it is satisifed or a timeout is reached
        /// </summary>
        /// <typeparam name="TException1">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TException2">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TException3">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TException4">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TException5">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TResult">Return type of the function to evaluate</typeparam>
        /// <param name="func">Function to check for valid evaluation</param>
        /// <param name="timeToWait">Time to try evaluating the given function until an exception is thrown</param>
        /// <returns>An async task that can return a value of type TResult</returns>
        public static Task<TResult> UntilNoExceptions<TException1, TException2, TException3, TException4, TException5, TResult>(Func<TResult> func, TimeSpan timeToWait)
            where TException1 : Exception
            where TException2 : Exception
            where TException3 : Exception
            where TException4 : Exception
            where TException5 : Exception
        {
            return ExecutePollingFunction(func, timeToWait, typeof(TException1), typeof(TException2), typeof(TException3), typeof(TException4), typeof(TException5));
        }

        /// <summary>
        /// Repeatedly checks for a condition with void return type until it is satisifed or a timeout is reached
        /// </summary>
        /// <typeparam name="TException1">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TException2">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TException3">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TException4">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <typeparam name="TException5">An exception type to ignore when attempting to evaluate the function</typeparam>
        /// <param name="action">Function to check for valid evaluation</param>
        /// <param name="timeToWait">Time to try evaluating the given function until an exception is thrown</param>
        /// <returns>An async task for the operation</returns>
        public static Task UntilNoExceptions<TException1, TException2, TException3, TException4, TException5>(Action action, TimeSpan timeToWait)
            where TException1 : Exception
            where TException2 : Exception
            where TException3 : Exception
            where TException4 : Exception
            where TException5 : Exception
        {
            return ExecutePollingFunction(action, timeToWait, typeof(TException1), typeof(TException2), typeof(TException3), typeof(TException4), typeof(TException5));
        }

        private static Task ExecutePollingFunction(Action actionToWaitForComplete, TimeSpan timeToWait, params Type[] types)
        {
            return ExecutePollingFunction(() => { actionToWaitForComplete(); return true; }, timeToWait, types);
        }

        private static async Task<TResult> ExecutePollingFunction<TResult>(Func<TResult> actionToWaitForComplete, TimeSpan timeToWait, params Type[] types)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            List<Exception> exceptions = new List<Exception>();
            do
            {
                try
                {
                    return actionToWaitForComplete();
                }
                catch (Exception ex) when (types.Contains(ex.GetType()))
                {
                    exceptions.Add(ex);
                }
                await Task.Delay(250).ConfigureAwait(false);
            } while (sw.Elapsed < timeToWait);
            throw new AggregateException(exceptions);
        }

        private static void VerifyAllExceptionTypes(params Type[] exes)
        {
            if (!exes.All(e => e.IsSubclassOf(typeof(Exception)) || e == typeof(Exception)))
            {
                throw new ArgumentException(Resources.VerifyAllExceptionTypes_ArgumentException);
            }
        }
    }
}
