using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Threading;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private const string WinAppDriverUrl = "http://127.0.0.1:4723/";
        private const string AppPath = @"C:\Users\Admin\source\repos\WindowsFormsApp2\WindowsFormsApp2\bin\Debug\WindowsFormsApp2.exe";

        private WindowsDriver<WindowsElement> driver;

        [TestInitialize]
        public void Setup()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", AppPath);
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            options.AddAdditionalCapability("ms:waitForAppLaunch", "5");

            driver = new WindowsDriver<WindowsElement>(new Uri(WinAppDriverUrl), options);
        }

        [TestCleanup]
        public void TearDown()
        {
            driver?.Quit();
        }

        private void FillAndCalculate(string days, string category, string places, string safe, string breakfast)
        {
            driver.FindElementByAccessibilityId("tbDays").Clear();
            driver.FindElementByAccessibilityId("tbDays").SendKeys(days);

            driver.FindElementByAccessibilityId("tbCategory").Clear();
            driver.FindElementByAccessibilityId("tbCategory").SendKeys(category);

            driver.FindElementByAccessibilityId("tbPlaces").Clear();
            driver.FindElementByAccessibilityId("tbPlaces").SendKeys(places);

            driver.FindElementByAccessibilityId("tbSafe").Clear();
            driver.FindElementByAccessibilityId("tbSafe").SendKeys(safe);

            driver.FindElementByAccessibilityId("tbBreakfast").Clear();
            driver.FindElementByAccessibilityId("tbBreakfast").SendKeys(breakfast);

            Thread.Sleep(200);
            driver.FindElementByAccessibilityId("btnCalculate").Click();
        }

        [TestMethod]
        public void Test_Calculation_Correct()
        {
            FillAndCalculate("2", "1", "2", "да", "да");
            string result = driver.FindElementByAccessibilityId("tbResult").Text;
            Assert.AreEqual("21700", result);
        }

        [TestMethod]
        public void Test_WithoutOptions()
        {
            FillAndCalculate("1", "2", "1", "нет", "нет");
            string result = driver.FindElementByAccessibilityId("tbResult").Text;
            Assert.AreEqual("3000", result);
        }

        [TestMethod]
        public void Test_OnlySafe()
        {
            FillAndCalculate("3", "3", "2", "да", "нет");
            string result = driver.FindElementByAccessibilityId("tbResult").Text;
            Assert.AreEqual("9500", result);
        }

        [TestMethod]
        public void Test_OnlyBreakfast()
        {
            FillAndCalculate("2", "2", "2", "нет", "да");
            string result = driver.FindElementByAccessibilityId("tbResult").Text;
            Assert.AreEqual("13200", result);
        }

        [TestMethod]
        public void Test_InvalidInput()
        {
            FillAndCalculate("abc", "1", "2", "да", "да");
            string result = driver.FindElementByAccessibilityId("tbResult").Text;
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void Test_MinValues()
        {
            FillAndCalculate("1", "3", "1", "нет", "нет");
            string result = driver.FindElementByAccessibilityId("tbResult").Text;
            Assert.AreEqual("1500", result);
        }

        [TestMethod]
        public void Test_MaxValues()
        {
            FillAndCalculate("10", "1", "3", "да", "да");
            string result = driver.FindElementByAccessibilityId("tbResult").Text;
            Assert.AreEqual("159500", result);
        }

        [TestMethod]
        public void Test_SafeWithUppercase()
        {
            FillAndCalculate("1", "2", "1", "Да", "нет");
            string result = driver.FindElementByAccessibilityId("tbResult").Text;
            Assert.AreEqual("3500", result);
        }

        [TestMethod]
        public void Test_BreakfastWithUppercase()
        {
            FillAndCalculate("2", "2", "1", "нет", "Да");
            string result = driver.FindElementByAccessibilityId("tbResult").Text;
            Assert.AreEqual("6600", result);
        }

        [TestMethod]
        public void Test_ZeroDays()
        {
            FillAndCalculate("0", "1", "2", "да", "да");
            string result = driver.FindElementByAccessibilityId("tbResult").Text;
            Assert.AreEqual("500", result);
        }
    }
}
