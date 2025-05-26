using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace HotleCalculatorTests
{
    [TestClass]
    public class Test1
    {
        private const string DriverUrl = "http://127.0.0.1:4723/";
        private const string AppPath = @"D:\Laba_Java\HotelCalculator\HotelCalculator\bin\Debug\HotelCalculator.exe";
        private WindowsDriver<WindowsElement> driver;
        /// <summary>
        /// Initialize WinAppDriver session before each test.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", AppPath);
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            options.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            driver = new WindowsDriver<WindowsElement>(new Uri(DriverUrl), options);
        }
        [TestMethod]
        public void EconomOneWithoutAll()
        {
            driver.FindElementByAccessibilityId("Days").SendKeys("1");
            driver.FindElementByAccessibilityId("Category").SendKeys("1");
            driver.FindElementByAccessibilityId("Capacity").SendKeys("1");
            driver.FindElementByAccessibilityId("Safe").SendKeys("нет");
            driver.FindElementByAccessibilityId("Breakfast").SendKeys("нет");
            driver.FindElementByAccessibilityId("Calc").Click();
            var total = driver.FindElementByAccessibilityId("Total").Text;
            Assert.AreEqual("900", total);
        }
        [TestMethod]
        public void StandardDoubleWithSafe()
        {
            driver.FindElementByAccessibilityId("Days").SendKeys("2");
            driver.FindElementByAccessibilityId("Category").SendKeys("2");
            driver.FindElementByAccessibilityId("Capacity").SendKeys("2");
            driver.FindElementByAccessibilityId("Safe").SendKeys("да");
            driver.FindElementByAccessibilityId("Breakfast").SendKeys("нет");
            driver.FindElementByAccessibilityId("Calc").Click();
            var total = driver.FindElementByAccessibilityId("Total").Text;
            Assert.AreEqual("6200", total);
        }
        [TestMethod]
        public void LuxTripleWithBreakfast()
        {
            driver.FindElementByAccessibilityId("Days").SendKeys("1");
            driver.FindElementByAccessibilityId("Category").SendKeys("3");
            driver.FindElementByAccessibilityId("Capacity").SendKeys("3");
            driver.FindElementByAccessibilityId("Safe").SendKeys("нет");
            driver.FindElementByAccessibilityId("Breakfast").SendKeys("да");
            driver.FindElementByAccessibilityId("Calc").Click();
            var total = driver.FindElementByAccessibilityId("Total").Text;
            Assert.AreEqual("5950", total);
        }
        [TestMethod]
        public void EconomySingleWithSafeAndBreakfast()
        {
            driver.FindElementByAccessibilityId("Days").SendKeys("3");
            driver.FindElementByAccessibilityId("Category").SendKeys("1");
            driver.FindElementByAccessibilityId("Capacity").SendKeys("1");
            driver.FindElementByAccessibilityId("Safe").SendKeys("да");
            driver.FindElementByAccessibilityId("Breakfast").SendKeys("да");
            driver.FindElementByAccessibilityId("Calc").Click();
            var total = driver.FindElementByAccessibilityId("Total").Text;
            Assert.AreEqual("4650", total);
        }
        [TestMethod]
        public void StandardTripleWithoutAllTwoNights()
        {
            driver.FindElementByAccessibilityId("Days").SendKeys("2");
            driver.FindElementByAccessibilityId("Category").SendKeys("2");
            driver.FindElementByAccessibilityId("Capacity").SendKeys("3");
            driver.FindElementByAccessibilityId("Safe").SendKeys("нет");
            driver.FindElementByAccessibilityId("Breakfast").SendKeys("нет");
            driver.FindElementByAccessibilityId("Calc").Click();
            var total = driver.FindElementByAccessibilityId("Total").Text;
            Assert.AreEqual("7600", total);
        }
        [TestMethod]
        public void LuxDoubleWithAll()
        {
            driver.FindElementByAccessibilityId("Days").SendKeys("1");
            driver.FindElementByAccessibilityId("Category").SendKeys("3");
            driver.FindElementByAccessibilityId("Capacity").SendKeys("2");
            driver.FindElementByAccessibilityId("Safe").SendKeys("да");
            driver.FindElementByAccessibilityId("Breakfast").SendKeys("да");
            driver.FindElementByAccessibilityId("Calc").Click();
            var total = driver.FindElementByAccessibilityId("Total").Text;
            Assert.AreEqual("5250", total);
        }
        [TestMethod]
        public void LuxDoubleWithAllFiveNights()
        {
            driver.FindElementByAccessibilityId("Days").SendKeys("5");
            driver.FindElementByAccessibilityId("Category").SendKeys("3");
            driver.FindElementByAccessibilityId("Capacity").SendKeys("2");
            driver.FindElementByAccessibilityId("Safe").SendKeys("да");
            driver.FindElementByAccessibilityId("Breakfast").SendKeys("да");
            driver.FindElementByAccessibilityId("Calc").Click();
            var total = driver.FindElementByAccessibilityId("Total").Text;
            Assert.AreEqual("26250", total);
        }
        [TestMethod]
        public void LuxDoubleWithoutAllFiveNights()
        {
            driver.FindElementByAccessibilityId("Days").SendKeys("5");
            driver.FindElementByAccessibilityId("Category").SendKeys("3");
            driver.FindElementByAccessibilityId("Capacity").SendKeys("2");
            driver.FindElementByAccessibilityId("Safe").SendKeys("нет");
            driver.FindElementByAccessibilityId("Breakfast").SendKeys("нет");
            driver.FindElementByAccessibilityId("Calc").Click();
            var total = driver.FindElementByAccessibilityId("Total").Text;
            Assert.AreEqual("23000", total);
        }
        [TestMethod]
        public void LuxThreeWithBreakfastSevenNights()
        {
            driver.FindElementByAccessibilityId("Days").SendKeys("7");
            driver.FindElementByAccessibilityId("Category").SendKeys("3");
            driver.FindElementByAccessibilityId("Capacity").SendKeys("3");
            driver.FindElementByAccessibilityId("Safe").SendKeys("нет");
            driver.FindElementByAccessibilityId("Breakfast").SendKeys("да");
            driver.FindElementByAccessibilityId("Calc").Click();
            var total = driver.FindElementByAccessibilityId("Total").Text;
            Assert.AreEqual("41650", total);
        }
        [TestMethod]
        public void EconomSingleWithoutAllTenNights()
        {
            driver.FindElementByAccessibilityId("Days").SendKeys("10");
            driver.FindElementByAccessibilityId("Category").SendKeys("1");
            driver.FindElementByAccessibilityId("Capacity").SendKeys("1");
            driver.FindElementByAccessibilityId("Safe").SendKeys("нет");
            driver.FindElementByAccessibilityId("Breakfast").SendKeys("нет");
            driver.FindElementByAccessibilityId("Calc").Click();
            var total = driver.FindElementByAccessibilityId("Total").Text;
            Assert.AreEqual("9000", total);
        }
        [TestCleanup]
        public void CleanUp()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}