using System;
using System.Diagnostics;

namespace IntelliTect.IntelliWait.Tests
{
    public class BaseTest
    {
        public BaseTest()
        {
            Test = new FakeTestClass();
        }

        protected FakeTestClass Test { get; }
    }
}
