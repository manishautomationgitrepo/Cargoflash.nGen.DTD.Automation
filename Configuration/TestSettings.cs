namespace Cargoflash.nGen.DTD.Automation.Configuration
{
    public static class TestSettings
    {
        public static string BaseUrl =>
            Environment.GetEnvironmentVariable("DTD_BASE_URL")
            ?? "https://ngenecomtestnew.cargoflash.com/Login.aspx";

        public static int DefaultTimeoutSeconds
        {
            get
            {
                string? configuredTimeout =
                    Environment.GetEnvironmentVariable("DTD_TIMEOUT_SECONDS");

                return int.TryParse(configuredTimeout, out int timeout) && timeout > 0
                    ? timeout
                    : 30;
            }
        }

        public static bool Headless =>
            bool.TryParse(
                Environment.GetEnvironmentVariable("DTD_HEADLESS"),
                out bool headless)
            && headless;

        public static string LoginDataPath =>
            Path.Combine(AppContext.BaseDirectory, "ExcelFiles", "LoginData.xlsx");

        public static string TessDataPath =>
            Path.Combine(AppContext.BaseDirectory, "tessdata");
    }
}
