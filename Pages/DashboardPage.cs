using Cargoflash.nGen.DTD.Automation.Utilities;
using OpenQA.Selenium;

namespace Cargoflash.nGen.DTD.Automation.Pages
{
    public class DashboardPage
    {
        private readonly IWebDriver driver;

        public DashboardPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private By DashboardMenu => By.XPath(
            "//*[normalize-space(text())='Dashboard']");

        public bool IsDisplayed()
        {
            try
            {
                return WaitUtils.WaitForCondition(
                    driver,
                    currentDriver => IsDashboardUrl(currentDriver.Url) &&
                        currentDriver.FindElements(DashboardMenu)
                            .Any(element => element.Displayed));
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        internal bool IsCurrentPage()
        {
            return IsDashboardUrl(driver.Url) &&
                driver.FindElements(DashboardMenu)
                    .Any(element => element.Displayed);
        }

        private static bool IsDashboardUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri? currentUrl) &&
                currentUrl.AbsolutePath.EndsWith(
                    "/Index.aspx",
                    StringComparison.OrdinalIgnoreCase);
        }
    }
}
