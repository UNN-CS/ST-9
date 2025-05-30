using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System.Threading;

namespace AutomatedTestsProject
{
    [TestClass]
    [DoNotParallelize]
    public class HotelBookingTests
    {
        private const string DriverUrl = "http://127.0.0.1:4723/";
        private const string AppPath = @"D:\WORK ARTEM\QA_UNN\WinFormsApp1\WinFormsApp1\bin\Debug\net9.0-windows\WinFormsApp1.exe";
        private WindowsDriver<WindowsElement>? driver;

        [TestInitialize]
        public void InitializeTestEnvironment()
        {
            var appOptions = ConfigureApplicationOptions();
            driver = LaunchApplication(appOptions);
            WaitForApplicationStart();
        }

        private AppiumOptions ConfigureApplicationOptions()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", AppPath);
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            options.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            return options;
        }

        private WindowsDriver<WindowsElement> LaunchApplication(AppiumOptions options)
        {
            return new WindowsDriver<WindowsElement>(new Uri(DriverUrl), options);
        }

        private void WaitForApplicationStart()
        {
            Thread.Sleep(1000);
        }

        private void EnterBookingDetails(string days, string category, string capacity, string safe, string breakfast)
        {
            ClearAndEnterText("textBox_Days", days);
            ClearAndEnterText("textBox_Category", category);
            ClearAndEnterText("textBox_Capacity", capacity);
            ClearAndEnterText("textBox_Safe", safe);
            ClearAndEnterText("textBox_Breakfast", breakfast);
        }

        private void ClearAndEnterText(string elementId, string value)
        {
            var element = driver.FindElementByAccessibilityId(elementId);
            element.Clear();
            element.SendKeys(value);
        }

        private string CalculateTotalPrice()
        {
            driver.FindElementByAccessibilityId("button_Sum").Click();
            return driver.FindElementByAccessibilityId("textBox_Sum").Text;
        }

        [TestMethod]
        public void SingleDayPremiumBookingWithExtras()
        {
            EnterBookingDetails("1", "2", "3", "да", "да");
            string totalPrice = CalculateTotalPrice();
            Assert.AreEqual("12000", totalPrice);
            Thread.Sleep(1000);
        }

        [TestMethod]
        public void SingleDayStandardBookingWithoutExtras()
        {
            EnterBookingDetails("1", "2", "3", "нет", "нет");
            string totalPrice = CalculateTotalPrice();
            Assert.AreEqual("8000", totalPrice);
            Thread.Sleep(1000);
        }

        [TestMethod]
        public void SingleDayEconomyBooking()
        {
            EnterBookingDetails("1", "1", "1", "нет", "нет");
            string totalPrice = CalculateTotalPrice();
            Assert.AreEqual("3000", totalPrice);
            Thread.Sleep(1000);
        }

        [TestMethod]
        public void LongTermLuxuryBooking()
        {
            EnterBookingDetails("100", "3", "3", "да", "да");
            string totalPrice = CalculateTotalPrice();
            Assert.AreEqual("1700000", totalPrice);
            Thread.Sleep(1000);
        }

        [TestMethod]
        public void ZeroDayBookingShouldBeFree()
        {
            Thread.Sleep(1000);
            EnterBookingDetails("0", "3", "3", "да", "нет");
            string totalPrice = CalculateTotalPrice();
            Assert.AreEqual("0", totalPrice);
            Thread.Sleep(1000);
        }

        [DataTestMethod]
        [DataRow("abc", "1", "1", "нет", "нет")]
        [DataRow("1", "xyz", "1", "нет", "нет")]
        [DataRow("1", "1", "два", "нет", "нет")]
        public void ShouldHandleInvalidInputGracefully(string days, string category, string capacity, string safe, string breakfast)
        {
            EnterBookingDetails(days, category, capacity, safe, breakfast);
            var result = CalculateTotalPrice();
            Assert.IsTrue(string.IsNullOrEmpty(result) || result == "0");
        }

        [TestMethod]
        public void EmptyInputShouldReturnNoResult()
        {
            EnterBookingDetails("", "", "", "", "");
            var result = CalculateTotalPrice();
            Assert.IsTrue(string.IsNullOrEmpty(result));
        }

        [TestCleanup]
        public void TearDown()
        {
            try
            {
                // Закрываем приложение
                driver.CloseApp();
            }
            finally
            {
                // Всегда закрываем драйвер
                driver?.Quit();
            }
        }
    }
}