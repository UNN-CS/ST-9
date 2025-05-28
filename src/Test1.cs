using System;
using System.Threading;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace HotelCalculatorTests
{
    [TestClass]
    public class CalculatorUiTests
    {
        private const string DriveUrl = "http://127.0.0.1:4723/";
        private WindowsDriver<WindowsElement> driver;

        [TestInitialize]
        public void Setup()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", @"C:\Users\kudko\source\repos\HotelCalculatorApp\HotelCalculatorApp\bin\Debug\HotelCalculatorApp.exe");
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            options.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            driver = new WindowsDriver<WindowsElement>(new Uri(DriveUrl), options);
            Thread.Sleep(500);
        }

        [TestCleanup]
        public void TearDown()
        {
            driver?.Quit();
        }

        [TestMethod]
        public void Test_Economy_1Day_1Person_NoExtras()
        {
            // base=1500; total=1500*1
            FillForm("1", "1", "1", "нет", "нет");
            Assert.AreEqual("1500", GetResult());
        }

        [TestMethod]
        public void Test_Standard_3Days_2Persons_WithBreakfast()
        {
            // (2200+300+250)*3 = 2750*3 = 8250
            FillForm("3", "2", "2", "нет", "да");
            Assert.AreEqual("8250", GetResult());
        }

        [TestMethod]
        public void Test_Luxury_5Days_3Persons_WithSafe()
        {
            // (2900+600+150)*5 = 3650*5 = 18250
            FillForm("5", "3", "3", "да", "нет");
            Assert.AreEqual("18250", GetResult());
        }

        [TestMethod]
        public void Test_FullOptions_10Days_3Category_3Capacity()
        {
            // (2900+600+400)*10 = 3900*10 = 39000
            FillForm("10", "3", "3", "да", "да");
            Assert.AreEqual("39000", GetResult());
        }

        [TestMethod]
        public void Test_NoDiscount_6Days_Standard_NoExtras()
        {
            // (2200+300)*6 = 2500*6 = 15000
            FillForm("6", "2", "2", "нет", "нет");
            Assert.AreEqual("15000", GetResult());
        }

        [TestMethod]
        public void Test_InvalidDays_NonNumeric_ShowsNoResult()
        {
            // days = "abc" → не распарсится
            FillForm("abc", "1", "1", "нет", "нет");
            Assert.AreEqual(string.Empty, driver.FindElementByAccessibilityId("tbResult").Text);
        }

        [TestMethod]
        public void Test_InvalidCategory_TooHigh_ShowsNoResult()
        {
            // category = "5" (допустимо 1–3)
            FillForm("1", "5", "1", "нет", "нет");
            Assert.AreEqual(string.Empty, driver.FindElementByAccessibilityId("tbResult").Text);
        }

        [TestMethod]
        public void Test_InvalidCapacity_TooLow_ShowsNoResult()
        {
            // capacity = "0" (допустимо 1–4)
            FillForm("1", "1", "0", "нет", "нет");
            Assert.AreEqual(string.Empty, driver.FindElementByAccessibilityId("tbResult").Text);
        }

        private void FillForm(string days, string category, string capacity, string safe, string breakfast)
        {
            driver.FindElementByAccessibilityId("tbDays").Clear();
            driver.FindElementByAccessibilityId("tbDays").SendKeys(days);
            driver.FindElementByAccessibilityId("tbCategory").Clear();
            driver.FindElementByAccessibilityId("tbCategory").SendKeys(category);
            driver.FindElementByAccessibilityId("tbCapacity").Clear();
            driver.FindElementByAccessibilityId("tbCapacity").SendKeys(capacity);
            driver.FindElementByAccessibilityId("tbSafe").Clear();
            driver.FindElementByAccessibilityId("tbSafe").SendKeys(safe);
            driver.FindElementByAccessibilityId("tbBreakfast").Clear();
            driver.FindElementByAccessibilityId("tbBreakfast").SendKeys(breakfast);
            driver.FindElementByAccessibilityId("btnCalculate").Click();
            Thread.Sleep(150);
        }

        private string GetResult()
        {
            var raw = driver.FindElementByAccessibilityId("tbResult").Text.Trim();
            var value = Decimal.Parse(
                raw,
                NumberStyles.Currency,
                CultureInfo.GetCultureInfo("ru-RU")
            );
            return ((int)value).ToString();
        }
    }
}
