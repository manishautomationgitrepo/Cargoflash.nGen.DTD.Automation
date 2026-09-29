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

            // =====================================
            // LOGIN DATA
            // =====================================

            DataTable loginData = ExcelReader.ReadWorksheet(
              TestSettings.TestDataPath,
              "Consolidator_Login",
              "Username",
              "Password");

            Assert.That(
                loginData.Rows.Count,
                Is.GreaterThan(0),
                "No login data was found in Consolidator_Login sheet.");

            DataRow loginRow = loginData.Rows[0];
            string username = ExcelReader.GetRequiredText(loginRow, "Username");
            string password = ExcelReader.GetRequiredText(loginRow, "Password");

            // =====================================
            // LOGIN
            // =====================================

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

            // =====================================
            // C2C BOOKING DATA
            // =====================================

            DataTable bookingData = ExcelReader.ReadWorksheet(
                TestSettings.TestDataPath,
                "C2C_Booking");

            Assert.That(
                bookingData.Rows.Count,
                Is.GreaterThan(0),
                "No C2C Booking data was found in C2C_Booking sheet.");

            DataRow bookingRow =bookingData.Rows[0];

            // =====================================
            // C2C BOOKING
            // =====================================

            C2CBookingPage c2cbooking = new C2CBookingPage(WebDriver);

            c2cbooking.OpenC2CBooking();
            c2cbooking.switchFrameC2CBooking();

            // =====================================
            // SHIPPER DETAILS
            // =====================================

            c2cbooking.clickAddShipperDetails();
            c2cbooking.enterShipperName(ExcelReader.GetRequiredText(bookingRow, "ShipperName"));
            c2cbooking.enterShipperLastName(ExcelReader.GetRequiredText(bookingRow, "ShipperLastName"));
            c2cbooking.enterShipperZipCode(ExcelReader.GetRequiredText(bookingRow, "ShipperZipCode"));
            c2cbooking.enterShipperAddress(ExcelReader.GetRequiredText(bookingRow, "ShipperAddress"));
            c2cbooking.enterShipperEmailID(ExcelReader.GetRequiredText(bookingRow, "ShipperEmail"));
            c2cbooking.EntershipperMobileNoPrefix(ExcelReader.GetRequiredText(bookingRow, "ShipperPrefix"));
            c2cbooking.EntershipperMobileNoCode(ExcelReader.GetRequiredText(bookingRow, "ShipperCountryCode"));
            c2cbooking.EntershipperMobileNo(ExcelReader.GetRequiredText(bookingRow, "ShipperMobileNo"));
            c2cbooking.saveShipperDetails();

            // =====================================
            // CONSIGNEE DETAILS
            // =====================================

            c2cbooking.clickAddConsigneeDetails();
            c2cbooking.enterConsigneeName(ExcelReader.GetRequiredText(bookingRow, "ConsigneeName"));
            c2cbooking.selectConsigneeCountry(ExcelReader.GetRequiredText(bookingRow, "ConsigneeCountry"));
            c2cbooking.selectConsigneeCity(ExcelReader.GetRequiredText(bookingRow, "ConsigneeCity"));
            c2cbooking.selectConsigneeZipcode(ExcelReader.GetRequiredText(bookingRow, "ConsigneeZipCode"));
            c2cbooking.enterConsigneeAddress(ExcelReader.GetRequiredText(bookingRow, "ConsigneeAddress"));
            c2cbooking.enterConsigneeEmailID(ExcelReader.GetRequiredText(bookingRow, "ConsigneeEmail"));
            c2cbooking.selectConsigneeMobilePrefixSNo(ExcelReader.GetRequiredText(bookingRow, "ConsigneePrefix"));
            c2cbooking.enterConsigneeMobileCountryCode(ExcelReader.GetRequiredText(bookingRow, "ConsigneeCountryCode"));
            c2cbooking.enterConsigneeMobileNo(ExcelReader.GetRequiredText(bookingRow, "ConsigneeMobileNo"));
            c2cbooking.saveConsigneeDetails();

            // =====================================
            // SHIPMENT DETAILS
            // =====================================

            c2cbooking.selectServiceType(ExcelReader.GetRequiredText(bookingRow, "ServiceType"));
            c2cbooking.selectProductType(ExcelReader.GetRequiredText(bookingRow, "ProductType"));
            c2cbooking.selectCommodity(ExcelReader.GetRequiredText(bookingRow, "Commodity"));
            c2cbooking.enterPieces(ExcelReader.GetRequiredText(bookingRow, "Pieces"));

            // =====================================
            // DIMENSION DETAILS
            // =====================================

            c2cbooking.clickAddDimension();
            c2cbooking.enterLenght(ExcelReader.GetRequiredText(bookingRow, "Length"));
            c2cbooking.enterWidth(ExcelReader.GetRequiredText(bookingRow, "Width"));
            c2cbooking.enterHeight(ExcelReader.GetRequiredText(bookingRow, "Height"));
            c2cbooking.enterPerPcsGrossWeight(ExcelReader.GetRequiredText(bookingRow, "GrossWeight"));
            c2cbooking.saveAddDimension();

            // =====================================
            // RATE & SAVE BOOKING
            // =====================================

            c2cbooking.enterItemDesription(ExcelReader.GetRequiredText(bookingRow, "ItemDescription"));
            c2cbooking.clickGetRate();
            c2cbooking.saveGetRate();
            c2cbooking.clickTermsAndConditions();
            c2cbooking.clickMandatoryDeclaration();

            string shipmentNo = c2cbooking.ClickSaveBookingAndGetShipmentNo();

            Console.WriteLine($"Generated Shipment No: {shipmentNo}");

        }
    }
}
