using Cargoflash.nGen.DTD.Automation.Drivers;
using Cargoflash.nGen.DTD.Automation.Helpers;
using Cargoflash.nGen.DTD.Automation.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargoflash.nGen.DTD.Automation.Pages
{
    public class C2CBookingPage
    {
        private readonly IWebDriver driver;

        public C2CBookingPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // =========================
        // Navigation Locators
        // =========================

        private By ShipmentMenu => By.XPath("//span[normalize-space()='Shipment']");

        private By C2CBookingMenu => By.XPath("//a[normalize-space()='C2C-Booking']");

        private By NewBookingButton => By.XPath("//input[@value='New Booking']");

        // =========================
        // Navigation Method
        // =========================

        public void OpenC2CBooking()
        {

            WaitUtil.WaitForElementToBeClickable(driver, ShipmentMenu).Click();
            WaitUtil.WaitForElementToBeClickable(driver, C2CBookingMenu).Click();
        }

        private By switchShipmentFrame => By.Id("iMasterFrame");

        public void switchFrameC2CBooking()
        {

            ElementActions.SwitchToFrame(driver, switchShipmentFrame);
            WaitUtil.WaitForElementToBeClickable(driver, NewBookingButton).Click();
        }


        // =============================
        // Shipper Information Locators
        // =============================

        private By AddShipperDetails => By.XPath("//input[@id='AddShipperDetails']");

        private By txtShipperName => By.Id("txtShipperName");

        private By txtShipperLastName => By.Id("txtShipperLastName");

        private By shipperZipCodeInput => By.Id("Text_ShipperZipCodeSNo");
        private By shipperZipCodeList => By.Id("Text_ShipperZipCodeSNo-list");
        private By shipperZipCodeOptions => By.XPath("//div[@id='Text_ShipperZipCodeSNo-list']//li");

        private By shipperAddressInput => By.Id("txtShipperAddress1");

        private By shipperEmailIdInput => By.Id("ShipperEmailAddress");

        private By shipperMobileNoPrefix1 => By.Id("Text_ShipperMobilePrefixSNo");
        private By shipperMobileNoPrefix2 => By.Id("Text_ShipperMobilePrefixSNo-list");
        private By shipperMobileNoPrefix3 => By.XPath("//div[@id='Text_ShipperMobilePrefixSNo-list']//li");
        private By txtShipperMobileCountryCode => By.Id("ShipperMobileCountryCode");

        private By txtShipperMobileNo => By.Id("ShipperMobileNo");

        private By saveShipperDetail => By.XPath("//input[@name='Saveshipper']");


        // ===========================
        // Shipper Information Methods
        // ===========================

        public void clickAddShipperDetails()
        {
            WaitUtil.WaitForElementToBeClickable(driver, AddShipperDetails);
            driver.FindElement(AddShipperDetails).Click();
        }

        public void enterShipperName(string shipperName)
        {
            ElementActions.EnterText(driver, txtShipperName, shipperName);
        }

        public void enterShipperLastName(String ShipperLastName)
        {
            ElementActions.EnterText(driver, txtShipperLastName, ShipperLastName);
        }

        public void enterShipperZipCode(String ShipperZipcode)
        {

            ElementActions.SelectFromAutoComplete(driver, shipperZipCodeInput, shipperZipCodeList, shipperZipCodeOptions,
                    ShipperZipcode);

        }

        public void enterShipperAddress(String ShipperAddress)
        {
            ElementActions.EnterText(driver, shipperAddressInput, ShipperAddress);
        }

        public void enterShipperEmailID(String ShipperEmailID)
        {
            ElementActions.EnterText(driver, shipperEmailIdInput, ShipperEmailID);
        }

        public void EntershipperMobileNoPrefix(String ShipperMobilePrefix)
        {
            ElementActions.SelectFromAutoComplete(driver, shipperMobileNoPrefix1, shipperMobileNoPrefix2, shipperMobileNoPrefix3,
                     ShipperMobilePrefix);
        }

        public void EntershipperMobileNoCode(String txtShipperMobileNoCode)
        {
            ElementActions.EnterText(driver, txtShipperMobileCountryCode, txtShipperMobileNoCode);
        }

        public void EntershipperMobileNo(String ShipperMobileNo)
        {
            ElementActions.EnterText(driver, txtShipperMobileNo, ShipperMobileNo);
        }

        public void saveShipperDetails()
        {
            WaitUtil.WaitForElementToBeClickable(driver, saveShipperDetail);
            driver.FindElement(saveShipperDetail).Click();
        }

        // =============================
        // Consignee Information Locators
        // =============================

        private By addConsigneeDetailsLink => By.Id("AddConsigneeDetails");
        private By consigneeNameInput => By.Id("txtConsigneeName");

        private By consigneeCountryInput => By.Id("Text_ConsigneeCountrySNo");
        private By consigneeCountryList => By.Id("Text_ConsigneeCountrySNo-list");
        private By consigneeCountryOptions => By.XPath("//div[@id='Text_ConsigneeCountrySNo-list']//li");

        private By consigneeCityInput => By.Id("Text_DestinationSNo");
        private By consigneeCityList => By.Id("Text_DestinationSNo-list");
        private By consigneeCityOptions => By.XPath("//div[@id='Text_DestinationSNo-list']//li");

        private By consigneeZipcodeInput => By.Id("Text_ConsigneeZipCodeSNo");
        private By consigneeZipcodeList => By.Id("Text_ConsigneeZipCodeSNo-list");
        private By consigneeZipcodeOptions => By.XPath("//div[@id='Text_ConsigneeZipCodeSNo-list']//li");

        private By consigneeAddressInput => By.Id("txtConsigneeAddress1");
        private By consigneeEmailIdInput => By.Id("ConsigneeEmailAddress");

        private By ConsigneeMobilePrefixSNo1 => By.Id("Text_ConsigneeMobilePrefixSNo");

        private By ConsigneeMobilePrefixSNo2 => By.Id("Text_ConsigneeMobilePrefixSNo-list");

        private By ConsigneeMobilePrefixSNo3 => By.XPath("//div[@id='Text_ConsigneeMobilePrefixSNo-list']//li");

        private By ConsigneeMobileCountryCodeInput => By.Id("ConsigneeMobileCountryCode");

        private By consigneeMobileNoInput => By.Id("ConsigneeMobileNo");
        private By saveConsigneeDetail => By.XPath("//input[@name='consignee']");


        // ===========================
        // Consignee Information Methods
        // ===========================

        public void clickAddConsigneeDetails()
        {
            WaitUtil.WaitForElementToBeInvisible(driver, addConsigneeDetailsLink);
            driver.FindElement(addConsigneeDetailsLink).Click();
        }

        public void enterConsigneeName(String ConsigneeName)
        {
            ElementActions.EnterText(driver, consigneeNameInput, ConsigneeName);
        }

        public void selectConsigneeCountry(String ConsigneeCountry)
        {
            ElementActions.SelectFromAutoComplete(driver, consigneeCountryInput, consigneeCountryList, consigneeCountryOptions,ConsigneeCountry);
        }

        public void selectConsigneeCity(String ConsigneeCity)
        {
            ElementActions.SelectFromAutoComplete(driver, consigneeCityInput, consigneeCityList, consigneeCityOptions,ConsigneeCity);
        }

        public void selectConsigneeZipcode(String ConsigneeZipcode)
        {
            ElementActions.SelectFromAutoComplete(driver, consigneeZipcodeInput, consigneeZipcodeList, consigneeZipcodeOptions,ConsigneeZipcode);
        }

        public void enterConsigneeAddress(String ConsigneeAddress)
        {
            ElementActions.EnterText(driver, consigneeAddressInput, ConsigneeAddress);
        }

        public void enterConsigneeEmailID(String ConsigneeEmailID)
        {
            ElementActions.EnterText(driver, consigneeEmailIdInput, ConsigneeEmailID);
        }


        public void selectConsigneeMobilePrefixSNo(String ConsigneeMobilePrefix)
        {
            ElementActions.SelectFromAutoComplete(driver, ConsigneeMobilePrefixSNo1, ConsigneeMobilePrefixSNo2, ConsigneeMobilePrefixSNo3, ConsigneeMobilePrefix);
        }

        public void enterConsigneeMobileCountryCode(String ConsigneeMobileCtryCode)
        {
            ElementActions.EnterText(driver, ConsigneeMobileCountryCodeInput, ConsigneeMobileCtryCode);
        }
         
        public void enterConsigneeMobileNo(String ConsigneeMobileNo)
        {
            ElementActions.EnterText(driver, consigneeMobileNoInput, ConsigneeMobileNo);
        }

        public void saveConsigneeDetails()
        {
            WaitUtil.WaitForElementToBeInvisible(driver, saveConsigneeDetail);
            driver.FindElement(saveConsigneeDetail).Click();
        }

        // ==========================================
        // Package and Shipment Information Locators
        // ==========================================

        private By ServiceTypeInput => By.Id("Text_ServiceTypeSNo");
        private By ServiceTypeList => By.Id("Text_ServiceTypeSNo-list");
        private By ServiceTypeOptions => By.XPath("//div[@id='Text_ServiceTypeSNo-list']//li");

        private By ProductTypeInput => By.Id("Text_ProductSNo");
        private By ProductTypeList => By.Id("Text_ProductSNo-list");
        private By ProductTypeOptions => By.XPath("//div[@id='Text_ProductSNo-list']//li");

        private By CommodityInput => By.Id("Text_CommoditySNo");
        private By CommodityList => By.Id("Text_CommoditySNo-list");
        private By CommodityOptions => By.XPath("//div[@id='Text_CommoditySNo-list']//li");

        private By PiecesInput => By.Id("Pieces");
        private By AddDimensionLink = By.Id("AddDimension");
        private By LenghtInput => By.Id("_tempLength");
        private By WidthInput => By.Id("_tempWidth");
        private By HeightInput => By.Id("_tempHeight");
        private By PerPcGrossWeightInput => By.Id("DimPerPcGrossWeight");
        private By SaveDimension => By.Id("SaveExitDimension");

        private By ItemDescriptionInput => By.Id("ItemDescription");

        // ==========================================
        // Package and Shipment Information Methods
        // ==========================================

        public void selectServiceType(String ServiceType)
        {

            ElementActions.SelectFromAutoComplete(driver, ServiceTypeInput, ServiceTypeList, ServiceTypeOptions, ServiceType);

        }

        public void selectProductType(String ProductType)
        {

            ElementActions.SelectFromAutoComplete(driver, ProductTypeInput, ProductTypeList, ProductTypeOptions, ProductType);

        }

        public void selectCommodity(String Commodity)
        {

            ElementActions.SelectFromAutoComplete(driver, CommodityInput, CommodityList, CommodityOptions, Commodity);

        }

        public void enterPieces(String Pieces)
        {
            ElementActions.EnterText(driver, PiecesInput, Pieces);
        }

        public void clickAddDimension()
        {
            WaitUtil.WaitForElementToBeClickable(driver, AddDimensionLink);
            driver.FindElement(AddDimensionLink).Click();
        }

        public void enterLenght(String Lenght)
        {
            //ElementActions.EnterText(driver, LenghtInput, Lenght);
            IWebElement LenghtField = WaitUtil.WaitForElementToBeVisible(driver, LenghtInput);
            LenghtField.SendKeys(Lenght);

        }

        public void enterWidth(String Width)
        {
            //ElementActions.EnterText(driver, WidthInput, Width);
            IWebElement WidthField = WaitUtil.WaitForElementToBeVisible(driver, WidthInput);
            WidthField.SendKeys(Width);
        }

        public void enterHeight(String Height)
        {
            //ElementActions.EnterText(driver, HeightInput, Height);
            IWebElement HeightField = WaitUtil.WaitForElementToBeVisible(driver, HeightInput);
            HeightField.SendKeys(Height);

        }

        public void enterPerPcsGrossWeight(String GrossWeight)
        {
            //ElementActions.EnterText(driver, PerPcGrossWeightInput, GrossWeight);
            IWebElement PerPiecesGrossWeightField = WaitUtil.WaitForElementToBeVisible(driver, PerPcGrossWeightInput);
            PerPiecesGrossWeightField.SendKeys(GrossWeight);
        }

        public void saveAddDimension()
        {
            WaitUtil.WaitForElementToBeClickable(driver, SaveDimension);
            driver.FindElement(SaveDimension).Click();
        }

        public void enterItemDesription(String ItemDescription)
        {
            ElementActions.EnterText(driver, ItemDescriptionInput, ItemDescription);
        }

        // ===========================
        // Rate Information Locators
        // ===========================

        private By GetRateLink => By.Id("GetRate");
        private By SaveExitRate => By.Id("SaveExitRate");
        private By TermsAndConditionsChk => By.Id("TermsAndConditionsChk");



        // ===========================
        // Rate Information Methods
        // ===========================

        public void clickGetRate()
        {
            WaitUtil.WaitForElementToBeClickable(driver, GetRateLink);
            driver.FindElement(GetRateLink).Click();
        }

        public void saveGetRate()
        {
            WaitUtil.WaitForElementToBeClickable(driver, SaveExitRate);
            driver.FindElement(SaveExitRate).Click();
        }

        public void clickTermsAndConditions()
        {
            WaitUtil.WaitForElementToBeClickable(driver, TermsAndConditionsChk);
            driver.FindElement(TermsAndConditionsChk).Click();
        }

        // ===========================
        // Declaration Locators
        // ===========================

        private By MandatoryDeclaration => By.Id("IsMandatoryDeclaration");

        // ===========================
        // Declaration Methods
        // ===========================

        public void clickMandatoryDeclaration()
        {
            WaitUtil.WaitForElementToBeClickable(driver, MandatoryDeclaration);
            driver.FindElement(MandatoryDeclaration).Click();
        }

        // ===========================
        // Save Booking Locator
        // ===========================

        private By SaveBooking => By.XPath("//body[1]/form[1]/div[3]/table[1]/tbody[1]/tr[3]/th[1]/div[1]/table[1]/tbody[1]/tr[2]/td[1]/input[1]");

        // ===========================
        // Save Booking Methods
        // ===========================

        public void clickSaveBooking()
        {
            WaitUtil.WaitForElementToBeClickable(driver, SaveBooking);
            driver.FindElement(SaveBooking).Click();
        }
    }
}
