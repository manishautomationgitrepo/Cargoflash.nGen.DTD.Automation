using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PracticeProject.Utilities;
using ExcelDataReader;
using System.Data;
using System.Text;

namespace PracticeProject.Drivers
{
    public abstract class Driver
    {
        protected IWebDriver driver = null!;

        [SetUp]
        public void OpenBrowser()
        {
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("https://ngend2d-test.cargoflash.com/");

            WaitUtil.WaitForPageLoad(driver);
        }

        public static DataTableCollection ReadExcel(string filePath)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            
            using FileStream stream = File.Open(filePath,FileMode.Open,FileAccess.Read);

            using IExcelDataReader reader =ExcelReaderFactory.CreateReader(stream);

            DataSet result =reader.AsDataSet(new ExcelDataSetConfiguration
            {
                        ConfigureDataTable = (_) =>new ExcelDataTableConfiguration
                        {
                                UseHeaderRow = true
                        }
                    }
                );

            return result.Tables;
        }
    }
}