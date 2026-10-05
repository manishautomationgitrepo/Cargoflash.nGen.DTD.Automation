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

        private By SuccessMessage => By.XPath("//div[@class='cfMessage cfMessage-success']");


        public void ClickArrivedBtn()
        {
            ElementActions.Click(driver, ArrivedBtn);
        }

        public string GetArrivedSuccessMessage()
        {
            IWebElement message =WaitUtils.WaitForElementToBeVisible(driver, SuccessMessage);

            return message.Text.Trim();
        }

        private By PaymentModeField => By.Id("Text_PaymentMode");
        private By PaymentModeList => By.Id("Text_PaymentMode-list");
        private By PaymentModeOptions => By.XPath("//div[@id='Text_PaymentMode-list']//li");

        public void selectPaymentOption(string paymentOption)
        {
            ElementActions.SelectFromAutoComplete(driver, PaymentModeField, PaymentModeList, PaymentModeOptions, paymentOption);
        }

        private By PaymentBtn => By.Id("btnPayment");

        public void ClickPaymentBtn()
        {
            ElementActions.Click(driver, PaymentBtn);
        }

        public string GetPaymentSuccessMessage()
        {
            IWebElement message = WaitUtils.WaitForElementToBeVisible(driver, SuccessMessage);

            return message.Text.Trim();
        }
        private By ReceivedBtn => By.Id("btnReceived");

        public void ClickReceivedBtn()
        {
            ElementActions.Click(driver, ReceivedBtn);
        }

        public string GetReceivedSuccessMessage()
        {
            IWebElement message = WaitUtils.WaitForElementToBeVisible(driver, SuccessMessage);

            return message.Text.Trim();
        }

        private By ConNoteLabel => By.Id("lblConNote");


        public string StoreGenerateConnote()
        {
            IWebElement connote = WaitUtils.WaitForElementToBeVisible(driver, ConNoteLabel);

            return connote.Text.Trim();
            
        }

    }
}

