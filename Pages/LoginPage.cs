using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using PracticeProject.Utilities;

namespace PracticeProject.Pages
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

        public void EnterUsername(string username)
        {
            //IWebElement usernameElement = wait.Until(ExpectedConditions.ElementIsVisible(UsernameTextbox));
            //usernameElement.Clear();
            //usernameElement.SendKeys(username);

            IWebElement usernameElement = WaitUtil.WaitForElementToBeVisible(driver, UsernameTextbox);
            usernameElement.Clear();
            usernameElement.SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            //IWebElement passwordElement = wait.Until(ExpectedConditions.ElementIsVisible(PasswordTextbox));
            //passwordElement.Clear();
            //passwordElement.SendKeys(password);

            IWebElement passwordElement=WaitUtil.WaitForElementToBeVisible(driver, PasswordTextbox);
            passwordElement.Clear();
            passwordElement.SendKeys(password);
        }

        public void ClickLogin()
        {
            //IWebElement loginButton=wait.Until(ExpectedConditions.ElementToBeClickable(LoginButton));
            //loginButton.Click();

            IWebElement loginButton=WaitUtil.WaitForElementToBeClickable(driver, LoginButton);
            loginButton.Click();    
        } 
    }
}