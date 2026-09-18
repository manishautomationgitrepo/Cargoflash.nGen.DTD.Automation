using Cargoflash.nGen.DTD.Automation.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Cargoflash.nGen.DTD.Automation.Helpers
{
    public class ElementActions
    {
        /* =====================================================
           KENDO / AUTOCOMPLETE DROPDOWN SUPPORT
           ===================================================== */

        public static void SelectFromAutoComplete(
            IWebDriver driver,
            By inputLocator,
            By dropdownListLocator,
            By dropdownOptions,
            string expectedValue)
        {
            WebDriverWait wait =
                new WebDriverWait(
                    driver,
                    TimeSpan.FromSeconds(20));

            // Wait for input field
            IWebElement input =
                wait.Until(
                    ExpectedConditions.ElementToBeClickable(inputLocator));

            input.Click();
            input.Clear();

            // Type first 3 characters
            string searchText = expectedValue.Length >= 3
                ? expectedValue.Substring(0, 3)
                : expectedValue;

            input.SendKeys(searchText);

            // Wait for dropdown to display
            wait.Until(
                ExpectedConditions.ElementIsVisible(dropdownListLocator));

            // Wait until options are available
            wait.Until(d =>
                d.FindElements(dropdownOptions).Count > 0);

            IReadOnlyCollection<IWebElement> options =
                driver.FindElements(dropdownOptions);

            foreach (IWebElement option in options)
            {
                if (option.Text.Contains(
                    expectedValue,
                    StringComparison.OrdinalIgnoreCase))
                {
                    option.Click();
                    return;
                }
            }

            throw new Exception(
                $"Value not found in dropdown: {expectedValue}");
        }


        /* =====================================================
           IFRAME SUPPORT
           ===================================================== */

        public static void SwitchToFrame(
            IWebDriver driver,
            By frameLocator)
        {
            try
            {
                WebDriverWait wait =
                    new WebDriverWait(
                        driver,
                        TimeSpan.FromSeconds(20));

                wait.Until(
                    ExpectedConditions
                        .FrameToBeAvailableAndSwitchToIt(
                            frameLocator));
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception(
                    $"Frame not found: {frameLocator}");
            }
        }

        public static void EnterText(IWebDriver driver, By locator, string value)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

                IWebElement element = wait.Until(ExpectedConditions.ElementToBeClickable(locator));

                element.Click();
                element.Clear();
                element.SendKeys(value);
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception(
                    $"Element not clickable within 30 seconds: {locator}");
            }
            catch (ElementNotInteractableException)
            {
                throw new Exception(
                    $"Element found but not interactable: {locator}");
            }
            catch (StaleElementReferenceException)
            {
                throw new Exception(
                    $"Element became stale while entering value: {locator}");
            }
        }
    }
}