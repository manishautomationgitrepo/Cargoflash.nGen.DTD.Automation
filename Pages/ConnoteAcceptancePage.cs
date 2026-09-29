using Cargoflash.nGen.DTD.Automation.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargoflash.nGen.DTD.Automation.Pages
{
    public class ConnoteAcceptancePage
    {
        private readonly IWebDriver driver;

        public ConnoteAcceptancePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // ==============================================
        // Locators Navigate to Connote Acceptace Page
        // ==============================================

        private By HomeIcon => By.CssSelector("img[src='dashboard/images/home-icon.png']");
        private By OperationMenu => By.XPath("//span[normalize-space()='Operations']");
        private By ConnoteAcceptanceMenu => By.CssSelector("a[href*='Apps=CONNoteListing']");

        // ==============================================
        // Methods Navigate to Connote Acceptace Page
        // ==============================================

        public void OpenConnoteAcceptance()
        {
            ElementActions.Click(driver, HomeIcon);
            ElementActions.Click(driver, OperationMenu);
            ElementActions.Click(driver, ConnoteAcceptanceMenu);    
        }

        // ==============================================
        // Swtich to the Connote Acceptance Frame
        // ==============================================

        private By switchShipmentFrame => By.Id("iMasterFrame");

        public void switchFrameConnoteAcceptance()
        {
            ElementActions.SwitchToFrame(driver, switchShipmentFrame);
        }

        // =========================
        // Locators 
        // =========================

        private By ConnoteReferenceNotxt =>By.Id("WaybillNo");
        private By SearchButton => By.Id("btnSearch");


        // =========================
        // Methods
        // =========================

        public void EnterShipmentNumber(string shipmentNo)
        {
            WaitUtils.WaitForElementToBeInvisible(driver, ConnoteReferenceNotxt);
            driver.FindElement(ConnoteReferenceNotxt).SendKeys(shipmentNo); 
        }

        public void ClickSearch()
        {
            ElementActions.Click(driver, SearchButton);
        }


        private By ArrivedBtn => By.Id("btnArrived");

    }
}

