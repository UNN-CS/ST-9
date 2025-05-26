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
        private const string AppPath = @"D:\Test\HotelCalculator\HotelCalculator\bin\Debug\HotelCalculator.exe";
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
        public void EconomNothingInclude_OnePers()
        {
            driver.FindElementByAccessibilityId("Days").SendKeys("1");
            driver.FindElementByAccessibilityId("Category").SendKeys("1");
            driver.FindElementByAccessibilityId("Capacity").SendKeys("1");
            driver.FindElementByAccessibilityId("Safe").SendKeys("нет");
            driver.FindElementByAccessibilityId("Breakfast").SendKeys("нет");
            driver.FindElementByAccessibilityId("Calculate").Click();
            var total = driver.FindElementByAccessibilityId("Total").Text;
            Assert.AreEqual("1000", total);
        }
        [TestMethod]
        public void EconomNothingInclude_TwoPers()
        {
            driver.FindElementByAccessibilityId("Days").SendKeys("1");
            driver.FindElementByAccessibilityId("Category").SendKeys("1");
            driver.FindElementByAccessibilityId("Capacity").SendKeys("2");
            driver.FindElementByAccessibilityId("Safe").SendKeys("нет");
            driver.FindElementByAccessibilityId("Breakfast").SendKeys("нет");
            driver.FindElementByAccessibilityId("Calculate").Click();
            var total = driver.FindElementByAccessibilityId("Total").Text;
            Assert.AreEqual("1500", total);
        }
        [TestMethod]
        public void EconomNothingInclude_ThreePers()
        {
            driver.FindElementByAccessibilityId("Days").SendKeys("1");
            driver.FindElementByAccessibilityId("Category").SendKeys("1");
            driver.FindElementByAccessibilityId("Capacity").SendKeys("3");
            driver.FindElementByAccessibilityId("Safe").SendKeys("нет");
            driver.FindElementByAccessibilityId("Breakfast").SendKeys("нет");
            driver.FindElementByAccessibilityId("Calculate").Click();
            var total = driver.FindElementByAccessibilityId("Total").Text;
            Assert.AreEqual("2000", total);
        }
        [TestMethod]
        public void EconomAllInclude_ThreePers()
        {
            driver.FindElementByAccessibilityId("Days").SendKeys("1");
            driver.FindElementByAccessibilityId("Category").SendKeys("1");
            driver.FindElementByAccessibilityId("Capacity").SendKeys("3");
            driver.FindElementByAccessibilityId("Safe").SendKeys("да");
            driver.FindElementByAccessibilityId("Breakfast").SendKeys("да");
            driver.FindElementByAccessibilityId("Calculate").Click();
            var total = driver.FindElementByAccessibilityId("Total").Text;
            Assert.AreEqual("2940", total);
        }
        [TestMethod]
        public void EconomAllInclude_OnePers()
        {
            driver.FindElementByAccessibilityId("Days").SendKeys("1");
            driver.FindElementByAccessibilityId("Category").SendKeys("1");
            driver.FindElementByAccessibilityId("Capacity").SendKeys("1");
            driver.FindElementByAccessibilityId("Safe").SendKeys("да");
            driver.FindElementByAccessibilityId("Breakfast").SendKeys("да");
            driver.FindElementByAccessibilityId("Calculate").Click();
            var total = driver.FindElementByAccessibilityId("Total").Text;
            Assert.AreEqual("1940", total);
        }
        [TestMethod]
        public void LuxAllInclude_OnePers()
        {
            driver.FindElementByAccessibilityId("Days").SendKeys("1");
            driver.FindElementByAccessibilityId("Category").SendKeys("3");
            driver.FindElementByAccessibilityId("Capacity").SendKeys("1");
            driver.FindElementByAccessibilityId("Safe").SendKeys("да");
            driver.FindElementByAccessibilityId("Breakfast").SendKeys("да");
            driver.FindElementByAccessibilityId("Calculate").Click();
            var total = driver.FindElementByAccessibilityId("Total").Text;
            Assert.AreEqual("3940", total);
        }
        [TestMethod]
        public void LuxAllInclude_FiveDays_OnePers()
        {
            driver.FindElementByAccessibilityId("Days").SendKeys("5");
            driver.FindElementByAccessibilityId("Category").SendKeys("3");
            driver.FindElementByAccessibilityId("Capacity").SendKeys("1");
            driver.FindElementByAccessibilityId("Safe").SendKeys("да");
            driver.FindElementByAccessibilityId("Breakfast").SendKeys("да");
            driver.FindElementByAccessibilityId("Calculate").Click();
            var total = driver.FindElementByAccessibilityId("Total").Text;
            Assert.AreEqual("19700", total);
        }
        [TestMethod]
        public void MediumSafeInclude_ThreeDays_TwoPers()
        {
            driver.FindElementByAccessibilityId("Days").SendKeys("3");
            driver.FindElementByAccessibilityId("Category").SendKeys("2");
            driver.FindElementByAccessibilityId("Capacity").SendKeys("2");
            driver.FindElementByAccessibilityId("Safe").SendKeys("да");
            driver.FindElementByAccessibilityId("Breakfast").SendKeys("нет");
            driver.FindElementByAccessibilityId("Calculate").Click();
            var total = driver.FindElementByAccessibilityId("Total").Text;
            Assert.AreEqual("9060", total);
        }
        [TestMethod]
        public void MediumBreakfastInclude_TenDays_ThreePers()
        {
            driver.FindElementByAccessibilityId("Days").SendKeys("10");
            driver.FindElementByAccessibilityId("Category").SendKeys("2");
            driver.FindElementByAccessibilityId("Capacity").SendKeys("3");
            driver.FindElementByAccessibilityId("Safe").SendKeys("нет");
            driver.FindElementByAccessibilityId("Breakfast").SendKeys("да");
            driver.FindElementByAccessibilityId("Calculate").Click();
            var total = driver.FindElementByAccessibilityId("Total").Text;
            Assert.AreEqual("34200", total);
        }
        [TestMethod]
        public void LuxAlltInclude_SevenDays_ThreePers()
        {
            driver.FindElementByAccessibilityId("Days").SendKeys("7");
            driver.FindElementByAccessibilityId("Category").SendKeys("3");
            driver.FindElementByAccessibilityId("Capacity").SendKeys("3");
            driver.FindElementByAccessibilityId("Safe").SendKeys("да");
            driver.FindElementByAccessibilityId("Breakfast").SendKeys("да");
            driver.FindElementByAccessibilityId("Calculate").Click();
            var total = driver.FindElementByAccessibilityId("Total").Text;
            Assert.AreEqual("34580", total);
        }
        [TestMethod]
        public void EconomBreakfastInclude_FourteenDays_TwoPers()
        {
            driver.FindElementByAccessibilityId("Days").SendKeys("14");
            driver.FindElementByAccessibilityId("Category").SendKeys("1");
            driver.FindElementByAccessibilityId("Capacity").SendKeys("2");
            driver.FindElementByAccessibilityId("Safe").SendKeys("нет");
            driver.FindElementByAccessibilityId("Breakfast").SendKeys("да");
            driver.FindElementByAccessibilityId("Calculate").Click();
            var total = driver.FindElementByAccessibilityId("Total").Text;
            Assert.AreEqual("26880", total);
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