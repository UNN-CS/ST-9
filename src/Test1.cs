using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace ST9_Test
{
    [TestClass]
    [DoNotParallelize]
    public sealed class Test1
    {
        private const string AppiumUrl = "http://127.0.0.1:4723/";
        private WindowsDriver<WindowsElement> driver;

        [TestInitialize]
        public void Initialize()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", @"C:\Users\Administrator\ST-9\bin\Debug\net8.0-windows\ST9.exe");
            options.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            options.AddAdditionalCapability("deviceName", "WindowsPC");

            driver = new WindowsDriver<WindowsElement>(new Uri(AppiumUrl), options);
        }

        [TestCleanup]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }

        private void Prepare(string days, string category, string capacity, string safe, string breakfast)
        {
            var daysInput = driver.FindElementByAccessibilityId("daysTextBox");
            daysInput.Clear();
            daysInput.SendKeys(days);

            var categoryInput = driver.FindElementByAccessibilityId("categoryTextBox");
            categoryInput.Clear();
            categoryInput.SendKeys(category);

            var capacityInput = driver.FindElementByAccessibilityId("capacityTextBox");
            capacityInput.Clear();
            capacityInput.SendKeys(capacity);

            var safeInput = driver.FindElementByAccessibilityId("safeTextBox");
            safeInput.Clear();
            safeInput.SendKeys(safe);

            var breakfastInput = driver.FindElementByAccessibilityId("breakfastTextBox");
            breakfastInput.Clear();
            breakfastInput.SendKeys(breakfast);

            driver.FindElementByAccessibilityId("resBtn").calcBtn.Click();
        }

        private void Assert(string expectedPrice)
        {
            Assert.AreEqual(expectedPrice, driver.FindElementByAccessibilityId("resultTextBox").Text);
        }

        [TestMethod]
        public void MinOneDay()
        {
            EnterFormData("1", "1", "1", "нет", "нет");
            VerifyPrice("340");
        }

        [TestMethod]
        public void MinThreeDays()
        {
            EnterFormData("7", "1", "1", "нет", "нет");
            VerifyPrice("2380");
        }

        [TestMethod]
        public void MidThreeDays()
        {
            EnterFormData("3", "2", "2", "нет", "да");
            VerifyPrice("2340");
        }

        [TestMethod]
        public void MaxOneWeek()
        {
            EnterFormData("7", "3", "3", "да", "да");
            VerifyPrice("8120");
        }

        [TestMethod]
        public void MinOneWeek()
        {
            EnterFormData("7", "3", "3", "нет", "нет");
            VerifyPrice("7140");
        }
    }
}

