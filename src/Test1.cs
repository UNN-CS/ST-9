using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace HotelBookingTests
{
    [TestClass]
    public class HotelBookingPriceCalculationTests
    {
        public const string DriveURL = "http://127.0.0.1:4723/";
        public WindowsDriver<WindowsElement> driver;

        [TestInitialize]
        public void Setup()
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", @"""C:\Users\User\source\repos\WinFormsApp2\WinFormsApp1\bin\Debug\net8.0-windows\WinFormsApp1.exe""");
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
            }
        }

        private void CalculateAndVerifyBookingPrice(string days, string roomCategory, string guestsCount,
                                                  string safeRequired, string breakfastRequired, string expectedPrice)
        {
            driver.FindElementByAccessibilityId("textBox1").SendKeys(days);
            driver.FindElementByAccessibilityId("textBox2").SendKeys(roomCategory);
            driver.FindElementByAccessibilityId("textBox3").SendKeys(guestsCount);
            driver.FindElementByAccessibilityId("textBox4").SendKeys(safeRequired);
            driver.FindElementByAccessibilityId("textBox5").SendKeys(breakfastRequired);

            driver.FindElementByAccessibilityId("button1").Click();

            var actualResult = driver.FindElementByAccessibilityId("textBox6").Text;
            Assert.IsTrue(actualResult.Contains(expectedPrice),
                         $"Expected price: {expectedPrice}, Actual: {actualResult}");
        }

        [TestMethod]
        public void PremiumSingleRoom_NoAdditionalServices_ShouldCalculateCorrectPrice()
        {
            CalculateAndVerifyBookingPrice("2", "1", "1", "нет", "нет", "11000,00 руб.");
        }




        [TestMethod]
        public void StandardSingleRoom_WithSafeAndBreakfast_ShouldCalculateCorrectPrice()
        {
            CalculateAndVerifyBookingPrice("5", "2", "1", "да", "да", "19500,00 руб.");
        }

        [TestMethod]
        public void StandardDoubleRoom_NoAdditionalServices_ShouldCalculateCorrectPrice()
        {
            CalculateAndVerifyBookingPrice("2", "2", "2", "нет", "нет", "9450,00 руб.");
        }

        [TestMethod]
        public void StandardTripleRoom_WithBreakfast_ShouldCalculateCorrectPrice()
        {
            CalculateAndVerifyBookingPrice("4", "2", "3", "нет", "да", "23100,00 руб.");
        }

        [TestMethod]
        public void EconomySingleRoom_WithSafe_ShouldCalculateCorrectPrice()
        {
            CalculateAndVerifyBookingPrice("1", "3", "1", "да", "нет", "2050,00 руб.");
        }

        [TestMethod]
        public void EconomyDoubleRoom_WithSafeAndBreakfast_ShouldCalculateCorrectPrice()
        {
            CalculateAndVerifyBookingPrice("3", "3", "2", "да", "да", "8590,00 руб.");
        }

        [TestMethod]
        public void EconomyTripleRoom_NoAdditionalServices_ShouldCalculateCorrectPrice()
        {
            CalculateAndVerifyBookingPrice("2", "3", "3", "нет", "нет", "5580,00 руб.");
        }
    }
}