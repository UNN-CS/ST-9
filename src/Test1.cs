using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using System;

namespace HotelCalculatorTests
{
    [TestClass]
    public class Tests
    {
        public const string DriverURL = "http://127.0.0.1:4723/";
        public WindowsDriver<WindowsElement> driver;

        [TestInitialize]
        public void Setup()
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", @"C:\Users\kobych\Desktop\ST9\HotelCalculator\HotelCalculator\bin\Debug\net8.0-windows\HotelCalculator.exe");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            driver = new WindowsDriver<WindowsElement>(new Uri(DriverURL), appCapabilities);
        }

        [TestCleanup]
        public void TearDown()
        {
            driver.Quit();
        }

        [TestMethod]
        public void TestLuxuryRoom_2Days_1Guest_BreakfastOnly()
        {
            driver.FindElementByAccessibilityId("txtDays").Clear();
            driver.FindElementByAccessibilityId("txtDays").SendKeys("2");
            driver.FindElementByAccessibilityId("txtGuests").Clear();
            driver.FindElementByAccessibilityId("txtGuests").SendKeys("1");
            driver.FindElementByAccessibilityId("cmbCategory").SendKeys("Люкс");
            driver.FindElementByAccessibilityId("chkBreakfast").Click();

            driver.FindElementByAccessibilityId("btnCalculate").Click();
            Thread.Sleep(1000);

            string actual = driver.FindElementByAccessibilityId("lblTotal").Text;
            Assert.AreEqual("₽11,000", actual);
        }

        [TestMethod]
        public void TestLuxuryRoom_3Days_2Guests_CleaningOnly()
        {
            driver.FindElementByAccessibilityId("txtDays").Clear();
            driver.FindElementByAccessibilityId("txtDays").SendKeys("3");
            driver.FindElementByAccessibilityId("txtGuests").Clear();
            driver.FindElementByAccessibilityId("txtGuests").SendKeys("2");
            driver.FindElementByAccessibilityId("cmbCategory").SendKeys("Люкс");
            driver.FindElementByAccessibilityId("chkCleaning").Click();

            driver.FindElementByAccessibilityId("btnCalculate").Click();
            Thread.Sleep(1000);

            string actual = driver.FindElementByAccessibilityId("lblTotal").Text;
            Assert.AreEqual("₽30,900", actual);
        }

        [TestMethod]
        public void TestLuxuryRoom_2Days_3Guests_AllOptions()
        {
            driver.FindElementByAccessibilityId("txtDays").Clear();
            driver.FindElementByAccessibilityId("txtDays").SendKeys("2");
            driver.FindElementByAccessibilityId("txtGuests").Clear();
            driver.FindElementByAccessibilityId("txtGuests").SendKeys("3");
            driver.FindElementByAccessibilityId("cmbCategory").SendKeys("Люкс");
            driver.FindElementByAccessibilityId("chkBreakfast").Click();
            driver.FindElementByAccessibilityId("chkCleaning").Click();

            driver.FindElementByAccessibilityId("btnCalculate").Click();
            Thread.Sleep(1000);

            string actual = driver.FindElementByAccessibilityId("lblTotal").Text;
            Assert.AreEqual("₽33,600", actual);
        }

        [TestMethod]
        public void TestStandardRoom_6Days_3Guests_NoOptions()
        {
            driver.FindElementByAccessibilityId("txtDays").Clear();
            driver.FindElementByAccessibilityId("txtDays").SendKeys("6");
            driver.FindElementByAccessibilityId("txtGuests").Clear();
            driver.FindElementByAccessibilityId("txtGuests").SendKeys("3");
            driver.FindElementByAccessibilityId("cmbCategory").SendKeys("Стандарт");

            driver.FindElementByAccessibilityId("btnCalculate").Click();
            Thread.Sleep(1000);

            string actual = driver.FindElementByAccessibilityId("lblTotal").Text;
            Assert.AreEqual("₽54,000", actual);
        }

        [TestMethod]
        public void TestStandardRoom_2Days_5Guests_CleaningOnly()
        {
            driver.FindElementByAccessibilityId("txtDays").Clear();
            driver.FindElementByAccessibilityId("txtDays").SendKeys("2");
            driver.FindElementByAccessibilityId("txtGuests").Clear();
            driver.FindElementByAccessibilityId("txtGuests").SendKeys("5");
            driver.FindElementByAccessibilityId("cmbCategory").SendKeys("Стандарт");
            driver.FindElementByAccessibilityId("chkCleaning").Click();

            driver.FindElementByAccessibilityId("btnCalculate").Click();
            Thread.Sleep(1000);

            string actual = driver.FindElementByAccessibilityId("lblTotal").Text;
            Assert.AreEqual("₽30,600", actual);
        }

        [TestMethod]
        public void TestLuxuryRoom_1Day_1Guest_AllOptions()
        {
            driver.FindElementByAccessibilityId("txtDays").SendKeys("1");
            driver.FindElementByAccessibilityId("txtGuests").SendKeys("1");
            driver.FindElementByAccessibilityId("cmbCategory").SendKeys("Люкс");
            driver.FindElementByAccessibilityId("chkBreakfast").Click();
            driver.FindElementByAccessibilityId("chkCleaning").Click();
            driver.FindElementByAccessibilityId("btnCalculate").Click();
            Thread.Sleep(1000);
            string actual = driver.FindElementByAccessibilityId("lblTotal").Text;
            Assert.AreEqual("₽5,800", actual);
        }

        [TestMethod]
        public void TestLuxuryRoom_7Days_2Guests_NoOptions()
        {
            driver.FindElementByAccessibilityId("txtDays").SendKeys("7");
            driver.FindElementByAccessibilityId("txtGuests").SendKeys("2");
            driver.FindElementByAccessibilityId("cmbCategory").SendKeys("Люкс");
            driver.FindElementByAccessibilityId("btnCalculate").Click();
            Thread.Sleep(1000);
            string actual = driver.FindElementByAccessibilityId("lblTotal").Text;
            Assert.AreEqual("₽70,000", actual);
        }

        [TestMethod]
        public void TestEconomyRoom_3Days_4Guests_CleaningOnly()
        {
            driver.FindElementByAccessibilityId("txtDays").SendKeys("3");
            driver.FindElementByAccessibilityId("txtGuests").SendKeys("4");
            driver.FindElementByAccessibilityId("cmbCategory").SendKeys("Эконом");
            driver.FindElementByAccessibilityId("chkCleaning").Click();
            driver.FindElementByAccessibilityId("btnCalculate").Click();
            Thread.Sleep(1000);
            string actual = driver.FindElementByAccessibilityId("lblTotal").Text;
            Assert.AreEqual("₽12,900", actual);
        }

    }
}