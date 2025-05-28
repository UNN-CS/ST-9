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
        private const string ApplicationPath = @"C:\Users\artem\source\repos\HotelCalculator\HotelCalculator\bin\Debug\net8.0-windows\HotelCalculator.exe";
        
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

        private void FillInputField(string controlId, string value)
        {
            var element = _driver!.FindElementByAccessibilityId(controlId);
            element.Clear();
            if (!string.IsNullOrEmpty(value))
            {
                element.SendKeys(value);
            }
        }

        private void FillCalculatorInputs(int days, int category, int capacity, string safe, string breakfast)
        {
            FillInputField("tbDays", days.ToString());
            FillInputField("tbCategory", category.ToString());
            FillInputField("tbCapacity", capacity.ToString());
            FillInputField("tbSafe", safe);
            FillInputField("tbBreakfast", breakfast);
        }

        private void CalculateAndVerifyResult(string expectedResult)
        {
            _driver!.FindElementByAccessibilityId("btnCalculate").Click();
            var actualResult = _driver.FindElementByAccessibilityId("tbTotal").Text;
            Assert.AreEqual(expectedResult, actualResult);
        }

        private void CalculateAndExpectError()
        {
            _driver!.FindElementByAccessibilityId("btnCalculate").Click();
            // Verify that total field is empty on error
            var result = _driver.FindElementByAccessibilityId("tbTotal").Text;
            Assert.IsTrue(string.IsNullOrEmpty(result));
        }

        [TestMethod]
        public void EconomySingleNoExtras()
        {
            FillCalculatorInputs(1, 1, 1, "нет", "нет");
            CalculateAndVerifyResult("950");
        }

        [TestMethod]
        public void StandardDoubleWithoutExtras()
        {
            FillCalculatorInputs(2, 2, 2, "нет", "нет");
            CalculateAndVerifyResult("4500");
        }

        [TestMethod]
        public void LuxTripleWithSafeOnly()
        {
            FillCalculatorInputs(1, 3, 3, "да", "нет");
            CalculateAndVerifyResult("3900");
        }

        [TestMethod]
        public void EconomyWithBreakfastOnly()
        {
            FillCalculatorInputs(3, 1, 1, "нет", "да");
            CalculateAndVerifyResult("3660");
        }

        [TestMethod]
        public void LuxDoubleWithExtras()
        {
            FillCalculatorInputs(2, 3, 2, "да", "да");
            CalculateAndVerifyResult("7440");
        }

        [TestMethod]
        public void StandardTripleNoExtras()
        {
            FillCalculatorInputs(1, 2, 3, "нет", "нет");
            CalculateAndVerifyResult("2700");
        }

        [TestMethod]
        public void EmptyInputs_ShouldShowError()
        {
            FillCalculatorInputs(0, 0, 0, "", "");
            CalculateAndExpectError();
        }

        [TestMethod]
        public void InvalidInputs_ShouldShowError()
        {
            FillInputField("tbDays", "abc");
            FillInputField("tbCategory", "xyz");
            FillInputField("tbCapacity", "-5");
            FillInputField("tbSafe", "maybe");
            FillInputField("tbBreakfast", "perhaps");
            
            CalculateAndExpectError();
        }

        [TestMethod]
        public void YesInputsInEnglish()
        {
            FillCalculatorInputs(1, 1, 1, "yes", "yes");
            CalculateAndVerifyResult("1470");
        }

        [TestMethod]
        public void NoInputsInEnglish()
        {
            FillCalculatorInputs(2, 2, 2, "no", "no");
            CalculateAndVerifyResult("4500");
        }

        [TestMethod]
        public void MixedYesNoInputs()
        {
            FillCalculatorInputs(3, 2, 1, "yes", "no");
            CalculateAndVerifyResult("6300");
        }

        [TestMethod]
        public void MixedNoYesInputs()
        {
            FillCalculatorInputs(2, 1, 2, "no", "yes");
            CalculateAndVerifyResult("3240");
        }

        [TestMethod]
        public void UppercaseYesNoInputs()
        {
            FillCalculatorInputs(1, 3, 3, "YES", "NO");
            CalculateAndVerifyResult("3900");
        }
    }
}
