using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace st9_tests
{
    [TestClass]
    public sealed class Test1
    {
        private const string WindowsDriverUri = "http://127.0.0.1:4723/";
        private const string ApplicationPath = @"C:\c# projects\ST-9\bin\Debug\net8.0-windows\ST-9.exe";

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
        public void TestCalculate1()
        {
            FillCalculatorInputs(3, 2, 2, "yes", "no");
            CalculateAndVerifyResult("6450");
        }

        [TestMethod]
        public void TestCalculate2()
        {
            FillCalculatorInputs(2, 1, 3, "yes", "yes");
            CalculateAndVerifyResult("4100");
        }

        [TestMethod]
        public void TestCalculate3()
        {
            FillCalculatorInputs(1, 3, 2, "no", "no");
            CalculateAndVerifyResult("3450");
        }

        [TestMethod]
        public void TestCalculate4()
        {
            FillCalculatorInputs(1, 1, 1, "no", "no");
            CalculateAndVerifyResult("800");
        }

        [TestMethod]
        public void TestCalculate5()
        {
            FillCalculatorInputs(3, 3, 3, "yes", "yes");
            CalculateAndVerifyResult("12750");
        }

    }
}
