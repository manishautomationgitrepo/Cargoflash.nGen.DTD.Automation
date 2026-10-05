using Cargoflash.nGen.DTD.Automation.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargoflash.nGen.DTD.Automation.Pages
{
    public class MenuPage
    {
        private readonly IWebDriver driver;

        public MenuPage(IWebDriver driver)
        { 
            this.driver = driver; 
        }

        // =========================
        // Navigation Locators (Shipment > C2C-Booking)
        // =========================

        private By HomeIcon => By.CssSelector("img[src='dashboard/images/home-icon.png']");

        private By ShipmentMenu => By.XPath("//span[normalize-space()='Shipment']");

        private By C2CBookingMenu => By.XPath("//a[normalize-space()='C2C-Booking']");

        private By switchShipmentFrame => By.Id("iMasterFrame");

        // =========================
        // Navigation Method (Shipment > C2C-Booking)
        // =========================

        public void OpenC2CBooking()
        {
            ElementActions.Click(driver, ShipmentMenu);
            ElementActions.Click(driver, C2CBookingMenu);
            ElementActions.SwitchToFrame(driver, switchShipmentFrame);

        }


        // =========================
        // Navigation Locators (Operations > Connote Acceptance)
        // =========================

        private By OperationMenu => By.XPath("//span[normalize-space()='Operations']");
        private By ConnoteAcceptanceMenu => By.CssSelector("a[href*='Apps=CONNoteListing']");

        // =========================
        // Navigation Method (Shipment > C2C-Booking)
        // =========================

        public void OpenConnoteAcceptance()
        {
            ElementActions.Click(driver, OperationMenu);
            ElementActions.Click(driver, ConnoteAcceptanceMenu);
            ElementActions.SwitchToFrame(driver, switchShipmentFrame);
        }


        // =========================
        // Navigation Locators (Shipment > B2B Booking)
        // =========================

        private By B2BBookingPage => By.XPath("//a[normalize-space()='B2B-Booking']");

        // =========================
        // Navigation Method (Shipment > B2B-Booking)
        // =========================

        public void OpenB2BBooking()
        {
            ElementActions.Click(driver, ShipmentMenu);
            ElementActions.Click(driver, B2BBookingPage);
            ElementActions.SwitchToFrame(driver, switchShipmentFrame);

        }

    }
}
