using OpenQA.Selenium;

namespace Cargoflash.nGen.DTD.Automation.Utilities
{
    public static class ElementActions
    {
        public static void SelectFromAutoComplete(
            IWebDriver driver,
            By inputLocator,
            By dropdownListLocator,
            By dropdownOptions,
            string expectedValue)
        {
            IWebElement input =
                WaitUtils.WaitForElementToBeClickable(
                    driver,
                    inputLocator);

            input.Click();
            input.Clear();

            string searchText =
                expectedValue.Length >= 3
                    ? expectedValue.Substring(0, 3)
                    : expectedValue;

            input.SendKeys(searchText);

            // Wait until dropdown is visible
            WaitUtils.WaitForElementToBeVisible(
                driver,
                dropdownListLocator);

            // Wait until dropdown options are loaded
            WaitUtils.WaitForCondition(
                driver,
                currentDriver =>
                    currentDriver
                        .FindElements(dropdownOptions)
                        .Count > 0);

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

        public static void SwitchToFrame(
            IWebDriver driver,
            By frameLocator)
        {
            try
            {
                WaitUtils.WaitForFrameAndSwitchToIt(
                    driver,
                    frameLocator);
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception(
                    $"Frame not found: {frameLocator}");
            }
        }

        public static void EnterText(
            IWebDriver driver,
            By locator,
            string value)
        {
            try
            {
                IWebElement element =
                    WaitUtils.WaitForElementToBeClickable(
                        driver,
                        locator);

                element.Click();
                element.Clear();
                element.SendKeys(value);
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception(
                    $"Element not clickable: {locator}");
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

        public static void Click(IWebDriver driver,By locator)
        {
            try
            {
                IWebElement element =
                    WaitUtils.WaitForElementToBeClickable(
                        driver,
                        locator);

                element.Click();
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception(
                    $"Element not clickable: {locator}");
            }
        }
    }
}