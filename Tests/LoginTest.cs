using NUnit.Framework;
using PracticeProject.Drivers;
using PracticeProject.ExcelFiles;
using PracticeProject.Pages;
using System.Data;

namespace PracticeProject.Tests
{
    public class LoginTests : Driver
    {
        [Test]
        public void LoginTest()
        {
            DataTableCollection data=Driver.ReadExcel(ExcelPath.excelFilePath);
            DataTable loginData = data["Sheet1"];
            DataRow row = loginData.Rows[0];
            string username = row["Username"].ToString().Trim();
            string password = row["Password"].ToString().Trim(); 

            LoginPage loginPage =new LoginPage(driver);
            loginPage.EnterUsername(username);
            loginPage.EnterPassword(password);
            loginPage.ClickLogin(); 
        }

        [Test]
        [Ignore("Under maintenance: Refactoring the payment gateway integration.")]
        public void ReadExcelTest()
        {
            DataTableCollection data =Driver.ReadExcel(ExcelPath.excelFilePath);

            Console.WriteLine("Total Sheets: " + data.Count);

            DataTable loginData = data["Sheet1"];

            Console.WriteLine("Login Sheet Rows: " + loginData.Rows.Count);

            DataRow row =loginData.Rows[0];

            string username =row["Username"].ToString().Trim();

            string password =row["Password"].ToString().Trim();

            Console.WriteLine("Username: " + username);

            Console.WriteLine("Password successfully read from Excel.");
        }

    }
}