using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace TestProject1
{
    [TestClass]
    public sealed class Test1
    {
        private const string WinAppDriverUrl = "http://127.0.0.1:4723";
        private WindowsDriver<WindowsElement>? _session;

        [TestInitialize]
        public void Setup()
        {
            var appOptions = new AppiumOptions();
            appOptions.AddAdditionalCapability("app",
                @"C:\github\ST-9\src\bin\Debug\net9.0-windows\WinFormsApp1.exe");
            appOptions.AddAdditionalCapability("ms:waitForAppLaunch", 5);
            appOptions.AddAdditionalCapability("deviceName", "WindowsPC");

            _session = new WindowsDriver<WindowsElement>(
                new Uri(WinAppDriverUrl), appOptions);
            Assert.IsNotNull(_session);
            _session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }

        [TestCleanup]
        public void TearDown()
        {
            if (_session != null)
            {
                _session.Quit();
                _session = null;
            }
        }

        private void EnterInputs(
            string days,
            string category,
            string places,
            string safe,
            string breakfast)
        {
            _session!.FindElementByAccessibilityId("textboxDays").Clear();
            _session.FindElementByAccessibilityId("textboxDays").SendKeys(days);

            _session.FindElementByAccessibilityId("textboxCategory").Clear();
            _session.FindElementByAccessibilityId("textboxCategory").SendKeys(category);

            _session.FindElementByAccessibilityId("textboxPlaces").Clear();
            _session.FindElementByAccessibilityId("textboxPlaces").SendKeys(places);

            _session.FindElementByAccessibilityId("textboxSafe").Clear();
            _session.FindElementByAccessibilityId("textboxSafe").SendKeys(safe);

            _session.FindElementByAccessibilityId("textboxBreakfast").Clear();
            _session.FindElementByAccessibilityId("textboxBreakfast").SendKeys(breakfast);
        }

        private int ClickCalculateAndGetTotal()
        {
            _session!.FindElementByAccessibilityId("buttonCalculate").Click();
            var totalText = _session.FindElementByAccessibilityId("textboxTotal").Text;
            Assert.IsTrue(int.TryParse(totalText, out int total));
            return total;
        }

        [TestMethod]
        public void Calculate_NoExtras_ShouldReturn9500()
        {
            EnterInputs("1", "1", "1", "нет", "нет");
            int actual = ClickCalculateAndGetTotal();
            Assert.AreEqual(9500, actual);
        }

        [TestMethod]
        public void Calculate_WithSafeOnly_ShouldReturn9800()
        {
            EnterInputs("1", "1", "1", "да", "нет");
            int actual = ClickCalculateAndGetTotal();
            Assert.AreEqual(9800, actual);
        }

        [TestMethod]
        public void Calculate_WithBreakfastOnly_ShouldReturn10700()
        {
            EnterInputs("1", "1", "1", "нет", "да");
            int actual = ClickCalculateAndGetTotal();
            Assert.AreEqual(10700, actual);
        }

        [TestMethod]
        public void Calculate_WithBothExtras_ShouldReturn11000()
        {
            EnterInputs("1", "1", "1", "да", "да");
            int actual = ClickCalculateAndGetTotal();
            Assert.AreEqual(11000, actual);
        }

        [TestMethod]
        public void Calculate_TwoDays_NoExtras_Category2_Places2_ShouldReturn14000()
        {
            EnterInputs("2", "2", "2", "нет", "нет");
            int actual = ClickCalculateAndGetTotal();
            Assert.AreEqual(14000, actual);
        }

        [TestMethod]
        public void Calculate_ThreeDays_AllMaxExtras_ShouldReturn18000()
        {
            EnterInputs("3", "3", "3", "да", "да");
            int actual = ClickCalculateAndGetTotal();
            Assert.AreEqual(18000, actual);
        }
    }
}
