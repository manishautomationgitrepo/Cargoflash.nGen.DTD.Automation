using Cargoflash.nGen.DTD.Automation.Configuration;
using Cargoflash.nGen.DTD.Automation.Drivers;
using Cargoflash.nGen.DTD.Automation.Pages;
using Cargoflash.nGen.DTD.Automation.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargoflash.nGen.DTD.Automation.Tests
{
    [TestFixture]
    public class ConnoteAcceptanceTest : Driver
    {
        [Test]
        public void Connote_Acceptance()
            {
                // =====================================
                // LOGIN DATA
                // =====================================

                DataTable loginData = ExcelReader.ReadWorksheet(
                    TestSettings.TestDataPath,
                    "Consolidator_Login");

                Assert.That(
                    loginData.Rows.Count,
                    Is.GreaterThan(0),
                    "No login data found in Consolidator_Login sheet.");

                DataRow loginRow = loginData.Rows[0];

                string username =
                    ExcelReader.GetRequiredText(
                        loginRow,
                        "Username");

                string password =
                    ExcelReader.GetRequiredText(
                        loginRow,
                        "Password");


                // =====================================
                // LOGIN
                // =====================================

                LoginPage loginPage =
                    new LoginPage(WebDriver);

                DashboardPage? dashboardPage =
                    loginPage.Login(
                        username,
                        password);

                Assert.That(
                    dashboardPage,
                    Is.Not.Null,
                    "Login was not completed successfully.");

                Assert.That(
                    dashboardPage!.IsDisplayed(),
                    Is.True,
                    "Dashboard was not displayed after login.");


                // =====================================
                // CONNOTE ACCEPTANCE DATA
                // =====================================

                DataTable connoteData = ExcelReader.ReadWorksheet(
                    TestSettings.TestDataPath,
                    "Connote_Acceptance");

                Assert.That(
                    connoteData.Rows.Count,
                    Is.GreaterThan(0),
                    "No data found in Connote_Acceptance sheet.");

                DataRow connoteRow =
                    connoteData.Rows[0];

                // =====================================
                // CONNOTE ACCEPTANCE
                // =====================================

                ConnoteAcceptancePage connoteAcceptance =
                    new ConnoteAcceptancePage(WebDriver);

                connoteAcceptance.OpenConnoteAcceptance();

                connoteAcceptance.switchFrameConnoteAcceptance();

                connoteAcceptance.EnterShipmentNumber(ExcelReader.GetRequiredText(connoteRow, "ShipmentNo"));

                connoteAcceptance.ClickSearch();
            }
        }
    }
