using Cargoflash.nGen.DTD.Automation.Configuration;
using Cargoflash.nGen.DTD.Automation.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Cargoflash.nGen.DTD.Automation.Drivers
{
    public abstract class Driver
    {
        protected IWebDriver WebDriver { get; private set; } = null!;

        [SetUp]
        public void OpenBrowser()
        {
            ChromeOptions options = new ChromeOptions();
            if (TestSettings.Headless)
            {
                options.AddArgument("--headless=new");
            }

            WebDriver = new ChromeDriver(options);
            WebDriver.Manage().Window.Maximize();
            WebDriver.Navigate().GoToUrl(TestSettings.BaseUrl);

            WaitUtil.WaitForPageLoad(WebDriver);
        }

   
    }
}
