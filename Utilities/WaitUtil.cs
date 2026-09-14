using Cargoflash.nGen.DTD.Automation.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Cargoflash.nGen.DTD.Automation.Utilities
{
    public static class WaitUtil
    {
        public static IWebElement WaitForElementToBeVisible(
            IWebDriver driver,
            By locator,
            int? timeoutSeconds = null)
        {
            WebDriverWait wait = CreateWait(driver, timeoutSeconds);

            return wait.Until(currentDriver =>
            {
                IWebElement element = currentDriver.FindElement(locator);
                return element.Displayed ? element : null;
            })!;
        }

        public static IWebElement WaitForElementToBeClickable(
            IWebDriver driver,
            By locator,
            int? timeoutSeconds = null)
        {
            WebDriverWait wait = CreateWait(driver, timeoutSeconds);

            return wait.Until(currentDriver =>
            {
                IWebElement element = currentDriver.FindElement(locator);
                return element.Displayed && element.Enabled ? element : null;
            })!;
        }

        public static bool WaitForElementToBeInvisible(
            IWebDriver driver,
            By locator,
            int? timeoutSeconds = null)
        {
            WebDriverWait wait = CreateWait(driver, timeoutSeconds);

            try
            {
                return wait.Until(currentDriver =>
                {
                    try
                    {
                        return !currentDriver.FindElement(locator).Displayed;
                    }
                    catch (NoSuchElementException)
                    {
                        return true;
                    }
                    catch (StaleElementReferenceException)
                    {
                        return true;
                    }
                });
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public static void WaitForPageLoad(IWebDriver driver, int? timeoutSeconds = null)
        {
            WebDriverWait wait = CreateWait(driver, timeoutSeconds);

            wait.Until(currentDriver =>
                ((IJavaScriptExecutor)currentDriver)
                    .ExecuteScript("return document.readyState")
                    .ToString() == "complete");
        }

        public static bool WaitForCondition(
            IWebDriver driver,
            Func<IWebDriver, bool> condition,
            int? timeoutSeconds = null)
        {
            WebDriverWait wait = CreateWait(driver, timeoutSeconds);
            return wait.Until(condition);
        }

        private static WebDriverWait CreateWait(IWebDriver driver, int? timeoutSeconds)
        {
            WebDriverWait wait = new WebDriverWait(
                driver,
                TimeSpan.FromSeconds(timeoutSeconds ?? TestSettings.DefaultTimeoutSeconds));

            wait.IgnoreExceptionTypes(
                typeof(NoSuchElementException),
                typeof(StaleElementReferenceException));

            return wait;
        }
    }
}
