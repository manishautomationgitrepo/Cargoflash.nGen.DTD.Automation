using Cargoflash.nGen.DTD.Automation.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using Tesseract;

namespace Cargoflash.nGen.DTD.Automation.Utilities
{
    public static partial class CaptchaSolver
    {
        public static int CalculateAnswer(byte[] imageBytes)
        {
            if (imageBytes.Length == 0)
            {
                throw new InvalidDataException("The CAPTCHA image is empty.");
            }

            using TesseractEngine engine = new TesseractEngine(
                TestSettings.TessDataPath,
                "eng",
                EngineMode.LstmOnly);
            engine.SetVariable(
                "tessedit_char_whitelist",
                "0123456789+-xX*/=?");

            using Pix originalImage = Pix.LoadFromMemory(imageBytes);
            using Pix enlargedImage = originalImage.Scale(4, 4);
            using Page page = engine.Process(enlargedImage, PageSegMode.SingleLine);

            return CalculateExpression(page.GetText());
        }

        internal static int CalculateExpression(string recognizedText)
        {
            string normalizedText = recognizedText
                .Replace('×', 'x')
                .Replace('÷', '/');
            Match expression = CaptchaExpression().Match(normalizedText);

            if (!expression.Success)
            {
                throw new InvalidDataException(
                    $"OCR did not recognize a valid mathematical expression: '{recognizedText.Trim()}'.");
            }

            int leftValue = int.Parse(expression.Groups["left"].Value);
            int rightValue = int.Parse(expression.Groups["right"].Value);
            string operation = expression.Groups["operator"].Value;

            return operation switch
            {
                "+" => leftValue + rightValue,
                "-" => leftValue - rightValue,
                "x" or "X" or "*" => leftValue * rightValue,
                "/" when rightValue != 0 && leftValue % rightValue == 0 => leftValue / rightValue,
                "/" when rightValue == 0 => throw new InvalidDataException(
                    "The CAPTCHA expression attempted division by zero."),
                "/" => throw new InvalidDataException(
                    "The CAPTCHA division does not have a whole-number answer."),
                _ => throw new InvalidDataException(
                    $"Unsupported CAPTCHA operator '{operation}'.")
            };
        }

        [GeneratedRegex(
            @"(?<left>\d+)\s*(?<operator>[+\-xX*/])\s*(?<right>\d+)",
            RegexOptions.CultureInvariant)]
        private static partial Regex CaptchaExpression();
    }
}
