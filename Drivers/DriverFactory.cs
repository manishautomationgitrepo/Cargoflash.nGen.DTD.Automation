using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace PracticeProject.Drivers
{
    internal class DriverFactory
    {
        public IWebDriver driver;

        public void LaunchBrowser()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
        
            driver = new ChromeDriver(options);


        }
    }
}
