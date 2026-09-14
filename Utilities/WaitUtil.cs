using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeProject.Utilities
{
    public static class WaitUtil
    {
        public static IWebElement WaitForElementToBeVisible(IWebDriver driver, By locator, int timeout = 60)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            return wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public static IWebElement WaitForElementToBeClickable(IWebDriver driver, By locator, int timeout = 60)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            return wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        public static void WaitForPageLoad(IWebDriver driver, int timeout = 60)
        {
            WebDriverWait wait =new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));

            wait.Until(d =>((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").ToString() == "complete");
        }
    }
}
