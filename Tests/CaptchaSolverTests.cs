using Cargoflash.nGen.DTD.Automation.Configuration;
using Cargoflash.nGen.DTD.Automation.Utilities;
using NUnit.Framework;
using Tesseract;

namespace Cargoflash.nGen.DTD.Automation.Tests
{
    [TestFixture]
    public sealed class CaptchaSolverTests
    {
        [TestCase("10 + 8 = ?", 18)]
        [TestCase("12-5=?", 7)]
        [TestCase("6 x 7 = ?", 42)]
        [TestCase("20 / 4 = ?", 5)]
        public void CalculateExpression_WithSupportedOperator_ReturnsExpectedAnswer(
            string expression,
            int expectedAnswer)
        {
            int answer = CaptchaSolver.CalculateExpression(expression);

            Assert.That(answer, Is.EqualTo(expectedAnswer));
        }

        [Test]
        public void CalculateExpression_WithUnreadableText_ThrowsClearError()
        {
            Assert.That(
                () => CaptchaSolver.CalculateExpression("unreadable"),
                Throws.TypeOf<InvalidDataException>());
        }

        [Test]
        public void OcrEngine_WithBundledTrainingData_InitializesSuccessfully()
        {
            Assert.That(
                () =>
                {
                    using TesseractEngine engine = new TesseractEngine(
                        TestSettings.TessDataPath,
                        "eng",
                        EngineMode.LstmOnly);
                },
                Throws.Nothing);
        }
    }
}
