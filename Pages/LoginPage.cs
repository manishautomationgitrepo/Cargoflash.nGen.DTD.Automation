using Cargoflash.nGen.DTD.Automation.Utilities;
using OpenQA.Selenium;

namespace Cargoflash.nGen.DTD.Automation.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private By UsernameTextbox => By.Id("txtUser");

        private By PasswordTextbox => By.Id("txtPassword");

        private By LoginButton => By.Id("LogInBtn");

        private By CaptchaImage => By.Id("imgCaptcha");

        private By CaptchaTextbox => By.Id("txtCaptcha");

        private By RefreshCaptchaButton => By.CssSelector("#textCaptcha a");

        private By IncorrectCaptchaMessage => By.XPath(
            "//*[contains(normalize-space(text()), 'Kindly enter correct captcha value.')]");

        public void EnterUsername(string username)
        {
            ElementActions.EnterText(driver, UsernameTextbox, username);
        }

        public void EnterPassword(string password)
        {
            ElementActions.EnterText(driver, PasswordTextbox, password);
        }

        public void ClickLogin()
        {
            ElementActions.Click(driver, LoginButton);
        }

        public DashboardPage? Login(string username, string password, int maximumAttempts = 3)
        {
            if (maximumAttempts < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(maximumAttempts),
                    "At least one login attempt is required.");
            }

            for (int attempt = 1; attempt <= maximumAttempts; attempt++)
            {
                EnterUsername(username);
                EnterPassword(password);
                EnterCaptchaAnswer();
                ClickLogin();

                if (WaitForLoginAttemptResult() == LoginAttemptResult.Succeeded)
                {
                    return new DashboardPage(driver);
                }
            }

            return null;
        }

        public void EnterCaptchaAnswer(int maximumAttempts = 3)
        {
            if (maximumAttempts < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(maximumAttempts),
                    "At least one CAPTCHA attempt is required.");
            }

            for (int attempt = 1; attempt <= maximumAttempts; attempt++)
            {
                IWebElement imageElement =
                    WaitUtils.WaitForElementToBeVisible(driver, CaptchaImage);
                byte[] imageBytes = CaptureElementScreenshot(imageElement);

                try
                {
                    int answer = CaptchaSolver.CalculateAnswer(imageBytes);
                    IWebElement captchaTextbox =
                        WaitUtils.WaitForElementToBeVisible(driver, CaptchaTextbox);
                    captchaTextbox.Clear();
                    captchaTextbox.SendKeys(answer.ToString());
                    return;
                }
                catch (InvalidDataException) when (attempt < maximumAttempts)
                {
                    RefreshCaptcha(imageBytes);
                }
            }

            throw new InvalidDataException(
                $"The CAPTCHA expression could not be recognized after {maximumAttempts} attempts.");
        }

        private static byte[] CaptureElementScreenshot(IWebElement element)
        {
            if (element is not WebElement webElement)
            {
                throw new NotSupportedException(
                    "The current WebDriver does not support element screenshots.");
            }

            return webElement.GetScreenshot().AsByteArray;
        }

        private void RefreshCaptcha(byte[] previousImage)
        {
            IWebElement refreshButton =
                WaitUtils.WaitForElementToBeClickable(driver, RefreshCaptchaButton);
            refreshButton.Click();

            string previousImageText = Convert.ToBase64String(previousImage);
            WaitUtils.WaitForCondition(driver, currentDriver =>
            {
                IWebElement refreshedImage = currentDriver.FindElement(CaptchaImage);
                byte[] refreshedImageBytes = CaptureElementScreenshot(refreshedImage);
                return Convert.ToBase64String(refreshedImageBytes) != previousImageText;
            });
        }

        private LoginAttemptResult WaitForLoginAttemptResult()
        {
            LoginAttemptResult result = LoginAttemptResult.Pending;

            WaitUtils.WaitForCondition(driver, currentDriver =>
            {
                DashboardPage dashboardPage = new DashboardPage(currentDriver);
                if (dashboardPage.IsCurrentPage())
                {
                    result = LoginAttemptResult.Succeeded;
                    return true;
                }

                bool captchaMessageDisplayed = currentDriver
                    .FindElements(IncorrectCaptchaMessage)
                    .Any(element => element.Displayed);
                bool passwordCleared = IsFieldEmpty(currentDriver, PasswordTextbox);
                bool captchaCleared = IsFieldEmpty(currentDriver, CaptchaTextbox);

                if (captchaMessageDisplayed && passwordCleared && captchaCleared)
                {
                    result = LoginAttemptResult.IncorrectCaptcha;
                    return true;
                }

                return false;
            });

            return result;
        }

        private static bool IsFieldEmpty(IWebDriver driver, By locator)
        {
            IReadOnlyCollection<IWebElement> fields = driver.FindElements(locator);
            return fields.Count == 1 && string.IsNullOrEmpty(fields.Single().GetAttribute("value"));
        }

        private enum LoginAttemptResult
        {
            Pending,
            Succeeded,
            IncorrectCaptcha
        }
    }
}
