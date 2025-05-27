using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace HotelCalculatorTests
{
    [TestClass]
    public class HotelCalculatorTests
    {
        private const string DriverUrl = "http://127.0.0.1:4723/";
        private const string AppPath = @"C:\Users\Администратор\source\repos\HotelCalculator\HotelCalculator\bin\Debug\net8.0-windows\HotelCalculator.exe";
        private WindowsDriver<WindowsElement>? driver;

        [TestInitialize]
        public void Setup()
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", AppPath);
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");

            driver = new WindowsDriver<WindowsElement>(new Uri(DriverUrl), appCapabilities);
        }

        [TestMethod]
        public void Calculate_SimpleCase_ReturnsCorrectSum()
        {
            driver.FindElementByAccessibilityId("tbDays").SendKeys("1");
            driver.FindElementByAccessibilityId("tbCategory").SendKeys("2");
            driver.FindElementByAccessibilityId("tbCapacity").SendKeys("3");
            driver.FindElementByAccessibilityId("tbSafe").SendKeys("да");
            driver.FindElementByAccessibilityId("tbBreakfast").SendKeys("нет");

            driver.FindElementByAccessibilityId("btnCalculate").Click();

            string result = driver.FindElementByAccessibilityId("tbTotal").Text;
            Assert.AreEqual("3200", result);
        }

        [TestMethod]
        public void Calculate_NoExtras_ReturnsCorrectSum()
        {
            driver.FindElementByAccessibilityId("tbDays").SendKeys("2");
            driver.FindElementByAccessibilityId("tbCategory").SendKeys("1");
            driver.FindElementByAccessibilityId("tbCapacity").SendKeys("1");
            driver.FindElementByAccessibilityId("tbSafe").SendKeys("нет");
            driver.FindElementByAccessibilityId("tbBreakfast").SendKeys("нет");

            driver.FindElementByAccessibilityId("btnCalculate").Click();

            string result = driver.FindElementByAccessibilityId("tbTotal").Text;
            Assert.AreEqual("2000", result);
        }

        [TestMethod]
        public void Calculate_LuxFullExtras_ReturnsCorrectSum()
        {
            driver.FindElementByAccessibilityId("tbDays").SendKeys("1");
            driver.FindElementByAccessibilityId("tbCategory").SendKeys("3");
            driver.FindElementByAccessibilityId("tbCapacity").SendKeys("3");
            driver.FindElementByAccessibilityId("tbSafe").SendKeys("да");
            driver.FindElementByAccessibilityId("tbBreakfast").SendKeys("да");

            driver.FindElementByAccessibilityId("btnCalculate").Click();

            string result = driver.FindElementByAccessibilityId("tbTotal").Text;
            Assert.AreEqual("4500", result);
        }

        [TestMethod]
        public void Calculate_InvalidInput_ShowsError()
        {
            driver.FindElementByAccessibilityId("tbDays").SendKeys("abc");
            driver.FindElementByAccessibilityId("tbCategory").SendKeys("1");
            driver.FindElementByAccessibilityId("tbCapacity").SendKeys("1");
            driver.FindElementByAccessibilityId("tbSafe").SendKeys("нет");
            driver.FindElementByAccessibilityId("tbBreakfast").SendKeys("нет");

            driver.FindElementByAccessibilityId("btnCalculate").Click();

            string result = driver.FindElementByAccessibilityId("tbTotal").Text;
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void Calculate_WithBreakfastOnly_ReturnsCorrectSum()
        {
            driver.FindElementByAccessibilityId("tbDays").SendKeys("2");
            driver.FindElementByAccessibilityId("tbCategory").SendKeys("2");
            driver.FindElementByAccessibilityId("tbCapacity").SendKeys("1");
            driver.FindElementByAccessibilityId("tbSafe").SendKeys("нет");
            driver.FindElementByAccessibilityId("tbBreakfast").SendKeys("да");

            driver.FindElementByAccessibilityId("btnCalculate").Click();

            string result = driver.FindElementByAccessibilityId("tbTotal").Text;
            Assert.AreEqual("4600", result);
        }

        [TestCleanup]
        public void TearDown()
        {
            driver?.Quit();
        }
    }
}