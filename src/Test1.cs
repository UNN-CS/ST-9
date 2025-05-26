using System;
using System.Globalization;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

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
            appCapabilities.AddAdditionalCapability("app", @"D:\уник\ST-9\WinFormsApp\WinFormsApp\bin\Debug\net8.0-windows\WinFormsApp.exe");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

            driver = new WindowsDriver<WindowsElement>(new Uri(DriveURL), appCapabilities);
      
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

        private void EnterInputs(string days, string category, string capacity, bool safe, bool breakfast)
        {
            var tbDays = driver.FindElementByAccessibilityId("txtDays");
            tbDays.Clear(); tbDays.SendKeys(days);

            var tbCategory = driver.FindElementByAccessibilityId("txtCategory");
            tbCategory.Clear(); tbCategory.SendKeys(category);

            var tbCapacity = driver.FindElementByAccessibilityId("txtCapacity");
            tbCapacity.Clear(); tbCapacity.SendKeys(capacity);

            var chkSafe = driver.FindElementByAccessibilityId("chkSafe");
            if (chkSafe.Selected != safe)
                chkSafe.Click();

            var chkBreakfast = driver.FindElementByAccessibilityId("chkBreakfast");
            if (chkBreakfast.Selected != breakfast)
                chkBreakfast.Click();
        }

        private string CalculateResult()
        {
            var btnCalc = driver.FindElementByAccessibilityId("btnCalc");
            btnCalc.Click();

            var tbSum = driver.FindElementByAccessibilityId("txtSum");
            return tbSum.Text;
        }

        [TestMethod]
        public void Test_Calculation_NoOptions()
        {
            EnterInputs("2", "1", "1", false, false);
            var result = CalculateResult();
            Assert.AreEqual("2 000 ₽", result);
        }

        [TestMethod]
        public void Test_Calculation_WithSafe()
        {
            EnterInputs("1", "2", "2", true, false);
            var result = CalculateResult();
            Assert.AreEqual("4 100 ₽", result);
        }

        [TestMethod]
        public void Test_Calculation_WithBreakfast()
        {
            EnterInputs("3", "3", "1", false, true);
            var result = CalculateResult();
            Assert.AreEqual("9 600 ₽", result);
        }

        [TestMethod]
        public void Test_Calculation_WithAllOptions()
        {
            EnterInputs("2", "1", "3", true, true);
            var result = CalculateResult();
            Assert.AreEqual("6 600 ₽", result);
        }

        [TestMethod]
        public void Test_LargerCapacity()
        {
            EnterInputs("5", "2", "3", false, true);
            var result = CalculateResult();
            Assert.AreEqual("31 000 ₽", result);
        }
    }
}
