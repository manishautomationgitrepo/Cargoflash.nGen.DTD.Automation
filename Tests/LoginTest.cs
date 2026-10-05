using Cargoflash.nGen.DTD.Automation.Configuration;
using Cargoflash.nGen.DTD.Automation.Drivers;
using Cargoflash.nGen.DTD.Automation.Pages;
using Cargoflash.nGen.DTD.Automation.Utilities;
using NUnit.Framework;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cargoflash.nGen.DTD.Automation.Tests
{
    [TestFixture]
    public class LoginTests : Driver
    {
        [Test]
        public void Verify_LoginTest()
        {
            DataTable loginData =ExcelReader.GetSheetData(TestSettings.TestDataPath,"Consolidator_Login");

            Assert.That(loginData.Rows.Count,Is.GreaterThan(0),"No login data found in Consolidator_Login sheet.");

            string username = loginData.Rows[0]["Username"].ToString().Trim();

            string password = loginData.Rows[0]["Password"].ToString().Trim();

            LoginPage loginPage = new LoginPage(WebDriver);
            DashboardPage? dashboardPage = loginPage.Login(username, password);

            Assert.That(dashboardPage,Is.Not.Null,"Login did not complete after three CAPTCHA attempts.");
            Assert.That(dashboardPage!.IsDisplayed(),Is.True,"The dashboard URL or Dashboard menu was not displayed after login.");
        }
    }
}
