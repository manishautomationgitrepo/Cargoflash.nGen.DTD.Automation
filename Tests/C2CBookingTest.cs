using Cargoflash.nGen.DTD.Automation.Configuration;
using Cargoflash.nGen.DTD.Automation.Drivers;
using Cargoflash.nGen.DTD.Automation.Pages;
using Cargoflash.nGen.DTD.Automation.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargoflash.nGen.DTD.Automation.Tests
{
    [TestFixture]
    public class C2CBookingTest : Driver
    {
        [Test]
        public void C2C_booking()
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


            C2CBookingPage c2cbooking = new C2CBookingPage(WebDriver);

            c2cbooking.OpenC2CBooking();
            c2cbooking.switchFrameC2CBooking();
            c2cbooking.clickAddShipperDetails();
            c2cbooking.enterShipperName("Manish");
            c2cbooking.enterShipperLastName("Arya");
            c2cbooking.enterShipperZipCode("23E23");
            c2cbooking.enterShipperAddress("Downtown Shanghai");
            c2cbooking.enterShipperEmailID("manish@gmail.com");
            c2cbooking.EntershipperMobileNoPrefix("+");
            c2cbooking.EntershipperMobileNoCode("91");
            c2cbooking.EntershipperMobileNo("4875643323");
            c2cbooking.saveShipperDetails();

            c2cbooking.clickAddConsigneeDetails();
            c2cbooking.enterConsigneeName("Manish Addis");
            c2cbooking.selectConsigneeCountry("ET-ETHIOPIA");
            c2cbooking.selectConsigneeCity("ADDIS ABABA [ADD]");
            c2cbooking.selectConsigneeZipcode("1000");
            c2cbooking.enterConsigneeAddress("Downtown Addis");
            c2cbooking.enterConsigneeEmailID("manish.addis@gmail.com");
            c2cbooking.selectConsigneeMobilePrefixSNo("+");
            c2cbooking.enterConsigneeMobileCountryCode("91");
            c2cbooking.enterConsigneeMobileNo("5647647453");
            c2cbooking.saveConsigneeDetails();

            c2cbooking.selectServiceType("EXPRESS");
            c2cbooking.selectProductType("DOOR TO DOOR - [D2D]");
            c2cbooking.selectCommodity("00001-DTD");
            c2cbooking.enterPieces("1");
            c2cbooking.clickAddDimension();
            c2cbooking.enterLenght("10");
            c2cbooking.enterWidth("10");
            c2cbooking.enterHeight("10");
            c2cbooking.enterPerPcsGrossWeight("10");
            c2cbooking.saveAddDimension();

            c2cbooking.enterItemDesription("Testing");
            c2cbooking.clickGetRate();
            c2cbooking.saveGetRate();
            c2cbooking.clickTermsAndConditions();
            c2cbooking.clickMandatoryDeclaration();
            c2cbooking.clickSaveBooking();



        }
    }
}
