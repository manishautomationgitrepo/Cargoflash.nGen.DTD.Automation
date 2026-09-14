using ExcelDataReader;
using System.Data;
using System.Text;

namespace Cargoflash.nGen.DTD.Automation.Utilities
{
    public static class ExcelReader
    {
        public static DataTable ReadWorksheet(
            string filePath,
            string worksheetName,
            params string[] requiredColumns)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(
                    $"Excel test data was not found at '{filePath}'.",
                    filePath);
            }

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            using IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);

            DataSet workbook = reader.AsDataSet(new ExcelDataSetConfiguration
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration
                {
                    UseHeaderRow = true
                }
            });

            DataTable worksheet = workbook.Tables[worksheetName]
                ?? throw new InvalidDataException(
                    $"Worksheet '{worksheetName}' was not found in '{filePath}'.");

            foreach (string column in requiredColumns)
            {
                if (!worksheet.Columns.Contains(column))
                {
                    throw new InvalidDataException(
                        $"Required column '{column}' was not found in worksheet '{worksheetName}'.");
                }
            }

            return worksheet;
        }

        public static string GetRequiredText(DataRow row, string columnName)
        {
            string value = row[columnName]?.ToString()?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidDataException(
                    $"A value is required in column '{columnName}'.");
            }

            return value;
        }
    }
}
