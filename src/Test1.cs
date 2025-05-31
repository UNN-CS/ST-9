using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using System;

namespace ST_9Tests
{
    [TestClass]
    [TestCategory("NoParallel")]
    [DoNotParallelize]
    public class Tests
    {
        public const string DriveURL = "http://127.0.0.1:4723/";
        public WindowsDriver<WindowsElement> driver;

        [TestInitialize]
        public void Setup()
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", @"C:\Users\salae\OneDrive\Desktop\Projects\st\WinFormsApp1\WinFormsApp1\bin\Debug\net9.0-windows\WinFormsApp1.exe");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            driver = new WindowsDriver<WindowsElement>(new Uri(DriveURL), appCapabilities);
        }

        [TestCleanup]
        public void TearDown()
        {
            driver?.Quit();
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
            RunTest("1", "1", "1", "нет", "нет", "3500");
        }

        [TestMethod]
        public void Test2_BaseWithSafe()
        {
            RunTest("1", "1", "1", "да", "нет", "3700");
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
        public void Test6_TwoDayEconom()
        {
            RunTest("2", "3", "3", "нет", "нет", "5000");
        }

        [TestMethod]
        public void Test7_OneWeekEconom()
        {
            RunTest("7", "3", "3", "нет", "нет", "17500");
        }

        [TestMethod]
        public void Test8_OneDayAllInClusive()
        {
            RunTest("1", "1", "1", "да", "да", "4000");
        }

        [TestMethod]
        public void Test9_WeekAllInClusive()
        {
            RunTest("7", "1", "1", "да", "да", "28000");
        }
    }
}