using NUnit.Framework;
using PracticeProject.Drivers;

namespace PracticeProject.Base
{
    internal class BaseTest : DriverFactory
    {
        [SetUp]
        public void Setup()
        {
            LaunchBrowser();
        }
    }
}
