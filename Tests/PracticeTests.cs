using NUnit.Framework;
using PracticeProject.Base;

namespace PracticeProject.Tests
{
    internal class PracticeTests : BaseTest
    {
        [Test]
        public void OpenPracticeWebsite()
        {
            driver.Navigate().GoToUrl("https://testautomationpractice.blogspot.com/");
        }
    }
}
