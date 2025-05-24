using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace HotelCalculatorTests
{
    [TestClass]
    public class HotelCalculatorTests
    {
        private const string WindowsDriverUri = "http://127.0.0.1:4723/";
        private const string ApplicationPath = @"C:\Users\artem\source\repos\HotelCalculator\bin\Debug\net8.0-windows\HotelCalculator.exe";

        private WindowsDriver<WindowsElement>? _driver;

        [TestInitialize]
        public void TestInitialize()
        {
            var appiumOptions = new AppiumOptions
            {
                App = ApplicationPath,
                DeviceName = "WindowsPC"
            };
            appiumOptions.AddAdditionalCapability("ms:waitForAppLaunch", "5");

            _driver = new WindowsDriver<WindowsElement>(new Uri(WindowsDriverUri), appiumOptions);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver?.Quit();
        }

        private void FillCalculatorInputs(int days, int category, int capacity, string safe, string breakfast)
        {
            FillInputField("tbDays", days.ToString());
            FillInputField("tbCategory", category.ToString());
            FillInputField("tbCapacity", capacity.ToString());
            FillInputField("tbSafe", safe);
            FillInputField("tbBreakfast", breakfast);
        }

        private void FillInputField(string controlId, string value)
        {
            _driver!.FindElementByAccessibilityId(controlId).SendKeys(value);
        }

        private void CalculateAndVerifyResult(string expectedResult)
        {
            _driver!.FindElementByAccessibilityId("btnCalculate").Click();
            var actualResult = _driver.FindElementByAccessibilityId("tbTotal").Text;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void EconomySingleNoExtras_ShouldBeCorrect()
        {
            FillCalculatorInputs(1, 1, 1, "нет", "нет");
            CalculateAndVerifyResult("950");
        }

        [TestMethod]
        public void StandardDoubleWithoutExtras_ShouldBeCorrect()
        {
            FillCalculatorInputs(2, 2, 2, "нет", "нет");
            CalculateAndVerifyResult("4500");
        }

        [TestMethod]
        public void LuxTripleWithSafeOnly_ShouldBeCorrect()
        {
            FillCalculatorInputs(1, 3, 3, "да", "нет");
            CalculateAndVerifyResult("3900");
        }

        [TestMethod]
        public void EconomyWithBreakfastOnly_ShouldBeCorrect()
        {
            FillCalculatorInputs(3, 1, 1, "нет", "да");
            CalculateAndVerifyResult("3660");
        }

        [TestMethod]
        public void LuxDoubleWithExtras_TwoDays_ShouldBeCorrect()
        {
            FillCalculatorInputs(2, 3, 2, "да", "да");
            CalculateAndVerifyResult("7440");
        }

        [TestMethod]
        public void StandardTripleNoExtras_ShouldBeCorrect()
        {
            FillCalculatorInputs(1, 2, 3, "нет", "нет");
            CalculateAndVerifyResult("2700");
        }

        [TestMethod]
        public void EmptyInputs_ShouldShowNothing()
        {
            _driver!.FindElementByAccessibilityId("btnCalculate").Click();
            var result = _driver.FindElementByAccessibilityId("tbTotal").Text;
            Assert.IsTrue(string.IsNullOrEmpty(result));
        }

        [TestMethod]
        public void InvalidDaysInput_ShouldNotCrash()
        {
            FillCalculatorInputs(int.MinValue, 1, 1, "нет", "нет");
            FillInputField("tbDays", "abc");
            CalculateAndVerifyResult("");
        }

        [TestMethod]
        public void InvalidCategory_ShouldCalculateAsZeroBase()
        {
            FillCalculatorInputs(1, 9, 2, "да", "да");
            CalculateAndVerifyResult("920");
        }

        [TestMethod]
        public void YesInputs_ShouldWorkCorrectly()
        {
            FillCalculatorInputs(1, 1, 1, "yes", "yes");
            CalculateAndVerifyResult("1470");
        }

        [TestMethod]
        public void NoInputs_ShouldAlsoWorkCorrectly()
        {
            FillCalculatorInputs(2, 2, 2, "no", "no");
            CalculateAndVerifyResult("4500");
        }

        [TestMethod]
        public void Mixed_YesNo_ShouldBeHandled()
        {
            FillCalculatorInputs(3, 2, 1, "yes", "no");
            CalculateAndVerifyResult("6300");
        }

        [TestMethod]
        public void Mixed_NoYes_ShouldBeHandled()
        {
            FillCalculatorInputs(2, 1, 2, "no", "yes");
            CalculateAndVerifyResult("3240");
        }

        [TestMethod]
        public void YesNoUppercase_ShouldBeAccepted()
        {
            FillCalculatorInputs(1, 3, 3, "YES", "NO");
            CalculateAndVerifyResult("3900");
        }
    }
}