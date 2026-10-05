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
    public class B2BBookingTest : Driver
    {

        [Test]  
        public void B2B_booking()
        {
            // =====================================
            // LOGIN DATA
            // =====================================

            DataTable loginData = ExcelReader.GetSheetData(TestSettings.TestDataPath, "Consolidator_Login");

            Assert.That(loginData.Rows.Count, Is.GreaterThan(0), "No login data found in Consolidator_Login sheet.");

            string username = loginData.Rows[0]["Username"].ToString().Trim();

            string password = loginData.Rows[0]["Password"].ToString().Trim();

            // =====================================
            // LOGIN
            // =====================================

            LoginPage loginPage = new LoginPage(WebDriver);
            DashboardPage? dashboardPage = loginPage.Login(username, password);

            Assert.That(dashboardPage, Is.Not.Null, "Login did not complete after three CAPTCHA attempts.");
            Assert.That(dashboardPage!.IsDisplayed(), Is.True, "The dashboard URL or Dashboard menu was not displayed after login.");



            // =====================================
            // B2B BOOKING DATA
            // =====================================

            DataTable B2B_BookingData = ExcelReader.GetSheetData(TestSettings.TestDataPath, "B2B_Booking");

            Assert.That(B2B_BookingData.Rows.Count, Is.GreaterThan(0), "No B2B Booking data was found in B2B_Booking sheet.");

            DataRow bookingRow = B2B_BookingData.Rows[0];

            // =====================================
            // C2C BOOKING
            // =====================================
            MenuPage menuPage = new MenuPage(WebDriver);
            menuPage.OpenB2BBooking();

            BookingPage b2bbooking=new BookingPage(WebDriver);
            b2bbooking.clickNewBookingBtn();
            b2bbooking.selectingCustomerName(ExcelReader.GetRequiredText(bookingRow, "CustomerName"));


            // =====================================
            // SHIPPER DETAILS
            // =====================================

            b2bbooking.clickAddShipperDetails();
            b2bbooking.enterShipperName(ExcelReader.GetRequiredText(bookingRow, "ShipperFirstName"));
            b2bbooking.enterShipperZipCode(ExcelReader.GetRequiredText(bookingRow, "ShipperZipCode"));
            b2bbooking.enterShipperAddress(ExcelReader.GetRequiredText(bookingRow, "ShipperAddress"));
            b2bbooking.enterShipperEmailID(ExcelReader.GetRequiredText(bookingRow, "ShipperEmail"));
            b2bbooking.EntershipperMobileNoPrefix(ExcelReader.GetRequiredText(bookingRow, "ShipperPrefix"));
            b2bbooking.EntershipperMobileNoCode(ExcelReader.GetRequiredText(bookingRow, "ShipperCountryCode"));
            b2bbooking.EntershipperMobileNo(ExcelReader.GetRequiredText(bookingRow, "ShipperMobileNo"));
            b2bbooking.saveShipperDetails();
            b2bbooking.EnterShipperCustomRegNo(ExcelReader.GetRequiredText(bookingRow, "ShipperCustomsRegNo"));

            // =====================================
            // CONSIGNEE DETAILS
            // =====================================

            b2bbooking.clickAddConsigneeDetails();
            b2bbooking.enterConsigneeName(ExcelReader.GetRequiredText(bookingRow, "ConsigneeName"));
            b2bbooking.selectConsigneeCountry(ExcelReader.GetRequiredText(bookingRow, "ConsigneeCountry"));
            b2bbooking.selectConsigneeCity(ExcelReader.GetRequiredText(bookingRow, "ConsigneeCity"));
            b2bbooking.selectConsigneeZipcode(ExcelReader.GetRequiredText(bookingRow, "ConsigneeZipCode"));
            b2bbooking.enterConsigneeAddress(ExcelReader.GetRequiredText(bookingRow, "ConsigneeAddress"));
            b2bbooking.enterConsigneeEmailID(ExcelReader.GetRequiredText(bookingRow, "ConsigneeEmail"));
            b2bbooking.selectConsigneeMobilePrefixSNo(ExcelReader.GetRequiredText(bookingRow, "ConsigneePrefix"));
            b2bbooking.enterConsigneeMobileCountryCode(ExcelReader.GetRequiredText(bookingRow, "ConsigneeCountryCode"));
            b2bbooking.enterConsigneeMobileNo(ExcelReader.GetRequiredText(bookingRow, "ConsigneeMobileNo"));
            b2bbooking.saveConsigneeDetails();
            b2bbooking.EnterConsigneeCustomRegNo(ExcelReader.GetRequiredText(bookingRow, "ConsigneeCustomsRegNo"));

            // =====================================
            // SHIPMENT DETAILS
            // =====================================

            b2bbooking.selectServiceType(ExcelReader.GetRequiredText(bookingRow, "ServiceType"));
            b2bbooking.selectProductType(ExcelReader.GetRequiredText(bookingRow, "ProductType"));
            b2bbooking.selectCommodity(ExcelReader.GetRequiredText(bookingRow, "Commodity"));
            b2bbooking.enterPieces(ExcelReader.GetRequiredText(bookingRow, "Pieces"));

            // =====================================
            // DIMENSION DETAILS
            // =====================================

            b2bbooking.clickAddDimension();
            b2bbooking.enterLenght(ExcelReader.GetRequiredText(bookingRow, "Length"));
            b2bbooking.enterWidth(ExcelReader.GetRequiredText(bookingRow, "Width"));
            b2bbooking.enterHeight(ExcelReader.GetRequiredText(bookingRow, "Height"));
            b2bbooking.enterPerPcsGrossWeight(ExcelReader.GetRequiredText(bookingRow, "GrossWeight"));
            b2bbooking.saveAddDimension();

            // =====================================
            // RATE & SAVE BOOKING
            // =====================================

            b2bbooking.enterItemDesription(ExcelReader.GetRequiredText(bookingRow, "ItemDescription"));
            b2bbooking.clickGetRate();
            b2bbooking.saveGetRate();
            b2bbooking.clickTermsAndConditions();
            b2bbooking.clickMandatoryDeclaration();

            string shipmentNo = b2bbooking.ClickSaveBookingAndGetShipmentNo();

            Console.WriteLine($"Generated Shipment No: {shipmentNo}");

        }
    }
}
