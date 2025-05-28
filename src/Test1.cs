using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using System;

namespace HotelCalculatorTests
{
    [TestClass]
    public class Tests
    {
        public const string DriveURL = "http://127.0.0.1:4723/";
        public WindowsDriver<WindowsElement> driver;

        [TestInitialize]
        public void Setup()
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", @"C:\Users\Vitaliy\source\repos\HotelCalculator\HotelCalculator\bin\Debug\net8.0-windows\HotelCalculator.exe");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            driver = new WindowsDriver<WindowsElement>(new Uri(DriveURL), appCapabilities);
        }

        [TestCleanup]
        public void TearDown()
        {
            driver.Quit();
        }

        public void RunTest(string days, string category, string capacity, string safe, string breakfast, string expected)
        {
            driver.FindElementByAccessibilityId("tbDays").SendKeys(days);
            driver.FindElementByAccessibilityId("tbCategory").SendKeys(category);
            driver.FindElementByAccessibilityId("tbCapacity").SendKeys(capacity);
            driver.FindElementByAccessibilityId("tbSafe").SendKeys(safe);
            driver.FindElementByAccessibilityId("tbBreakfast").SendKeys(breakfast);
            driver.FindElementByAccessibilityId("btnCalculate").Click();

            string result = driver.FindElementByAccessibilityId("tbResult").Text;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test1_BaseCalculation()
        {
            RunTest("2", "1", "1", "нет", "нет", "7000");
        }

        [TestMethod]
        public void Test2_WithSafe()
        {
            RunTest("2", "1", "1", "да", "нет", "7400");
        }

        [TestMethod]
        public void Test3_WithBreakfast()
        {
            RunTest("2", "1", "1", "нет", "да", "7600");
        }

        [TestMethod]
        public void Test4_FullOptions()
        {
            RunTest("2", "1", "1", "да", "да", "8000");
        }

        [TestMethod]
        public void Test5_CategoryAndCapacity()
        {
            RunTest("2", "2", "3", "нет", "нет", "7000");
        }

        [TestMethod]
        public void Test6_AllHigh()
        {
            RunTest("3", "3", "3", "да", "да", "9000");
        }

        [TestMethod]
        public void Test7_OneDayEconom()
        {
            RunTest("1", "1", "1", "нет", "нет", "3500");
        }

        [TestMethod]
        public void Test8_OneDayFullOptions()
        {
            RunTest("1", "1", "1", "да", "да", "4000");
        }
    }
}
