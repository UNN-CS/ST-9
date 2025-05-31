using System;
using System.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
namespace TestProject1
{
    [TestClass]
    public class UnitTests1
    {
        public const string DriveURL = "http://127.0.0.1:4723/";
        public WindowsDriver<WindowsElement> driver;

        [TestInitialize]
        public void Setup()
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", @"D:\Учёба\program\WinFormsApp1\bin\Debug\net8.0-windows\WinFormsApp1.exe");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

            driver = new WindowsDriver<WindowsElement>(new Uri(DriveURL), appCapabilities);
        }
        [TestCleanup]
        public void Cleanup()
        {
            driver?.Quit();
        }
        [TestMethod]
        public void TestStandardCaseWithoutExtras()
        {
            var daysTextBox = driver.FindElementByAccessibilityId("days_of_stay_tb");
            var categoryTextBox = driver.FindElementByAccessibilityId("category_tb");
            var capacityTextBox = driver.FindElementByAccessibilityId("capacity_tb");
            var safeTextBox = driver.FindElementByAccessibilityId("safe_tb");
            var breakfastTextBox = driver.FindElementByAccessibilityId("breakfast_tb");
            var calculateButton = driver.FindElementByAccessibilityId("calculate");
            var sumTextBox = driver.FindElementByAccessibilityId("sum_tb");

            daysTextBox.Clear();
            daysTextBox.SendKeys("3");

            categoryTextBox.Clear();
            categoryTextBox.SendKeys("2");

            capacityTextBox.Clear();
            capacityTextBox.SendKeys("1");

            safeTextBox.Clear();
            breakfastTextBox.Clear();

            calculateButton.Click();

            Assert.AreEqual("30000", sumTextBox.Text);
        }

        [TestMethod]
        public void TestWithAllAdditionalServices()
        {
            var daysTextBox = driver.FindElementByAccessibilityId("days_of_stay_tb");
            var categoryTextBox = driver.FindElementByAccessibilityId("category_tb");
            var capacityTextBox = driver.FindElementByAccessibilityId("capacity_tb");
            var safeTextBox = driver.FindElementByAccessibilityId("safe_tb");
            var breakfastTextBox = driver.FindElementByAccessibilityId("breakfast_tb");
            var calculateButton = driver.FindElementByAccessibilityId("calculate");
            var sumTextBox = driver.FindElementByAccessibilityId("sum_tb");

            daysTextBox.Clear();
            daysTextBox.SendKeys("5");

            categoryTextBox.Clear();
            categoryTextBox.SendKeys("3");

            capacityTextBox.Clear();
            capacityTextBox.SendKeys("2");

            safeTextBox.Clear();
            safeTextBox.SendKeys("да");

            breakfastTextBox.Clear();
            breakfastTextBox.SendKeys("да");

            calculateButton.Click();

            Assert.AreEqual("151500", sumTextBox.Text);
        }

        [TestMethod]
        public void TestOnlySafeService()
        {
            var daysTextBox = driver.FindElementByAccessibilityId("days_of_stay_tb");
            var categoryTextBox = driver.FindElementByAccessibilityId("category_tb");
            var capacityTextBox = driver.FindElementByAccessibilityId("capacity_tb");
            var safeTextBox = driver.FindElementByAccessibilityId("safe_tb");
            var breakfastTextBox = driver.FindElementByAccessibilityId("breakfast_tb");
            var calculateButton = driver.FindElementByAccessibilityId("calculate");
            var sumTextBox = driver.FindElementByAccessibilityId("sum_tb");

            daysTextBox.Clear();
            daysTextBox.SendKeys("1");

            categoryTextBox.Clear();
            categoryTextBox.SendKeys("1");

            capacityTextBox.Clear();
            capacityTextBox.SendKeys("1");

            safeTextBox.Clear();
            safeTextBox.SendKeys("да");

            calculateButton.Click();

            Assert.AreEqual("5500", sumTextBox.Text);
        }

        [TestMethod]
        public void TestMinimalValues()
        {
            var daysTextBox = driver.FindElementByAccessibilityId("days_of_stay_tb");
            var categoryTextBox = driver.FindElementByAccessibilityId("category_tb");
            var capacityTextBox = driver.FindElementByAccessibilityId("capacity_tb");
            var calculateButton = driver.FindElementByAccessibilityId("calculate");
            var sumTextBox = driver.FindElementByAccessibilityId("sum_tb");

            daysTextBox.Clear();
            daysTextBox.SendKeys("1");

            categoryTextBox.Clear();
            categoryTextBox.SendKeys("1");

            capacityTextBox.Clear();
            capacityTextBox.SendKeys("1");

            calculateButton.Click();

            Assert.AreEqual("5000", sumTextBox.Text);
        }
        [TestMethod]
        public void TestMaximumValues()
        {
            // Находим элементы
            var daysTextBox = driver.FindElementByAccessibilityId("days_of_stay_tb");
            var categoryTextBox = driver.FindElementByAccessibilityId("category_tb");
            var capacityTextBox = driver.FindElementByAccessibilityId("capacity_tb");
            var safeTextBox = driver.FindElementByAccessibilityId("safe_tb");
            var breakfastTextBox = driver.FindElementByAccessibilityId("breakfast_tb");
            var calculateButton = driver.FindElementByAccessibilityId("calculate");
            var sumTextBox = driver.FindElementByAccessibilityId("sum_tb");

            daysTextBox.Clear();
            daysTextBox.SendKeys("3");

            categoryTextBox.Clear();
            categoryTextBox.SendKeys("3");

            capacityTextBox.Clear();
            capacityTextBox.SendKeys("3");

            safeTextBox.Clear();
            safeTextBox.SendKeys("да");

            breakfastTextBox.Clear();
            breakfastTextBox.SendKeys("да");

            calculateButton.Click();

            Assert.AreEqual("136500", sumTextBox.Text);
        }
    }
}