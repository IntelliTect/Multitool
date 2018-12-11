using System;
using System.Diagnostics;

namespace IntelliTect.IntelliWait.Tests
{
    public class FakeTestClass
    {
        public void ThrowExceptionWithNoReturn()
        {
            throw new Exception();
        }

        public bool ThrowExceptionWithReturn()
        {
            throw new Exception();
        }

        public void ThrowNullRefExceptionWithNoReturn()
        {
            throw new NullReferenceException();
        }

        public bool ThrowNullRefExceptionWithReturn()
        {
            throw new NullReferenceException();
        }

        public void CheckExceptionsVoidReturn(int secondsToFail = 1, int numberOfDifferentExceptions = 1)
        {
            ThrowExceptions(secondsToFail, numberOfDifferentExceptions);
        }

        public bool CheckExceptionsBoolReturn(int secondsToFail = 1, int numberOfDifferentExceptions = 1)
        {
            ThrowExceptions(secondsToFail, numberOfDifferentExceptions);
            return true;
        }

        // Find a better way to do this to better facilitate test parallelization
        private void ThrowExceptions(int secondsToFail, int numberOfExceptions)
        {
            if (_Timeout == TimeSpan.MinValue)
            {
                _Timeout = TimeSpan.FromSeconds(secondsToFail);
                _Sw.Start();
            }

            while (_Sw.Elapsed <= _Timeout)
            {
                _Attempts++;
                if (_Attempts > numberOfExceptions)
                {
                    _Attempts = 1;
                }
                switch (_Attempts)
                {
                    case 1:
                        throw new InvalidOperationException();
                    case 2:
                        throw new InvalidProgramException();
                    case 3:
                        throw new IndexOutOfRangeException();
                    case 4:
                        throw new ArgumentNullException();
                    case 5:
                        throw new FieldAccessException();
                    default:
                        throw new ArgumentException();
                }
            }
        }

        private int _Attempts = 0;
        private TimeSpan _Timeout = TimeSpan.MinValue;
        private Stopwatch _Sw = new Stopwatch();
    }
}