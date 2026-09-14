using Cargoflash.nGen.DTD.Automation.Configuration;
using Cargoflash.nGen.DTD.Automation.Drivers;
using Cargoflash.nGen.DTD.Automation.Pages;
using Cargoflash.nGen.DTD.Automation.Utilities;
using NUnit.Framework;
using System.Data;

namespace Cargoflash.nGen.DTD.Automation.Tests
{
    [TestFixture]
    public sealed class LoginTests : Driver
    {
        [Test]
        public void Login_WithValidCredentials_OpensDashboard()
        {
            DataTable loginData = ExcelReader.ReadWorksheet(
                TestSettings.LoginDataPath,
                "Sheet1",
                "Username",
                "Password");

            Assert.That(
                loginData.Rows.Count,
                Is.GreaterThan(0),
                "No login data was found in Sheet1.");

            DataRow row = loginData.Rows[0];
            string username = ExcelReader.GetRequiredText(row, "Username");
            string password = ExcelReader.GetRequiredText(row, "Password");

            LoginPage loginPage = new LoginPage(WebDriver);
            DashboardPage? dashboardPage = loginPage.Login(username, password);

            Assert.That(
                dashboardPage,
                Is.Not.Null,
                "Login did not complete after three CAPTCHA attempts.");
            Assert.That(
                dashboardPage!.IsDisplayed(),
                Is.True,
                "The dashboard URL or Dashboard menu was not displayed after login.");
        }
    }
}
