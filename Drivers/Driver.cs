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

        [OneTimeSetUp]
        public void OpenBrowser()
        {
            ChromeOptions options = new ChromeOptions();

            options.AddArgument("--start-maximized");

            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-popup-blocking");

            ChromeDriverService service =
                ChromeDriverService.CreateDefaultService();

            service.HideCommandPromptWindow = true;

            WebDriver = new ChromeDriver(
                service,
                options,
                TimeSpan.FromMinutes(2)
            );

            WebDriver.Navigate().GoToUrl(TestSettings.BaseUrl);

            WaitUtil.WaitForPageLoad(WebDriver);
        }

   
    }
}
