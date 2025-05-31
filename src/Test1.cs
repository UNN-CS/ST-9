using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Diagnostics;

namespace HotelCalculatorTests
{
    [TestClass]
    [DoNotParallelize]
    public class Test1
    {
        public const string DriveURL = "http://127.0.0.1:4723/";
        private WindowsDriver<WindowsElement> driver;
        private Process appProcess;
        [TestInitialize]
        public void Setup()
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", @"C:\ST-9\HotelCalculator\bin\Debug\HotelCalculator.exe");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

            driver = new WindowsDriver<WindowsElement>(new Uri(DriveURL), appCapabilities);
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Quit();
        }
        private string FillFormAndCalculate(string days, string category, string capacity, string safe, string breakfast)
        {
            driver.FindElementByAccessibilityId("txtDays").SendKeys(days);
            driver.FindElementByAccessibilityId("txtCategory").SendKeys(category);
            driver.FindElementByAccessibilityId("txtCapacity").SendKeys(capacity);
            driver.FindElementByAccessibilityId("txtSafe").SendKeys(safe);
            driver.FindElementByAccessibilityId("txtBreakfast").SendKeys(breakfast);
            driver.FindElementByAccessibilityId("btnCalculate").Click();

            return driver.FindElementByAccessibilityId("txtTotal").Text;
        }

        [TestMethod]
        public void WithSafeAndBreakfast()
        {
            string result = FillFormAndCalculate("2", "1", "2", "да", "да");
            Assert.AreEqual("13400", result);
        }

        [TestMethod]
        public void OnlySafe()
        {
            string result = FillFormAndCalculate("1", "3", "2", "да", "нет");
            Assert.AreEqual("3500", result);
        }

        [TestMethod]
        public void MinValues()
        {
            string result = FillFormAndCalculate("1", "1", "1", "нет", "нет");
            Assert.AreEqual("5500", result); 
        }

        [TestMethod]
        public void MaxOptions()
        {
            string result = FillFormAndCalculate("5", "1", "3", "да", "да");
            Assert.AreEqual("31000", result); 
        }

        [TestMethod]
        public void OnlyBreakfast()
        {
            string result = FillFormAndCalculate("2", "3", "1", "нет", "да");
            Assert.AreEqual("4900", result);
        }

        [TestMethod]
        public void BasePriceOnly()
        {
            string result = FillFormAndCalculate("3", "2", "1", "нет", "нет");
            Assert.AreEqual("9500", result); 
        }

    }
}
