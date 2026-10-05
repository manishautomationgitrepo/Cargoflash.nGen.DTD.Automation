using ExcelDataReader;
using System;
using System.Data;
using System.IO;
using System.Text;

namespace Cargoflash.nGen.DTD.Automation.Utilities
{
    public static class ExcelReader
    {
        public static DataTable GetSheetData(string filePath, string sheetName)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (FileStream stream = File.Open(filePath,FileMode.Open,FileAccess.Read,FileShare.ReadWrite))
            {
                using (IExcelDataReader reader =ExcelReaderFactory.CreateReader(stream))
                {
                    DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration
                        {
                            ConfigureDataTable = _ =>
                                new ExcelDataTableConfiguration
                                {
                                    UseHeaderRow = true
                                }
                        });

                    return result.Tables[sheetName];
                }
            }
        }

        public static string GetRequiredText(DataRow row,string columnName)
        {
            if (!row.Table.Columns.Contains(columnName))
            {
                throw new ArgumentException(
                    $"Column '{columnName}' not found in Excel sheet.");
            }

            string value =row[columnName]?.ToString()?.Trim()
                ?? string.Empty;

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    $"Required Excel value is empty for column '{columnName}'.");
            }

            return value;
        }
    }
}