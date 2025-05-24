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
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", @"C:\Users\mixan\source\repos\HotelCalculator\HotelCalculator\bin\Debug\net7.0-windows\HotelCalculator.exe");
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            options.AddAdditionalCapability("ms:waitForAppLaunch", "5");

            driver = new WindowsDriver<WindowsElement>(new Uri(DriveURL), options);
        }

        [TestCleanup]
        public void TearDown()
        {
            driver.Quit();
        }

        [TestMethod]
        public void Test_Simple_OneDay_WithoutExtras()
        {
            FillForm("1", "1", "1", "нет", "нет");
            Assert.AreEqual("3000", GetResult());
        }

        [TestMethod]
        public void Test_ThreeDays_Suite_TwoGuests_WithBreakfast()
        {
            FillForm("3", "3", "2", "нет", "да");
            Assert.AreEqual("28500", GetResult());
        }

        [TestMethod]
        public void Test_FiveDays_MidRoom_ThreeGuests_WithSafe()
        {
            FillForm("5", "2", "3", "да", "нет");
            Assert.AreEqual("36000", GetResult());
        }

        [TestMethod]
        public void Test_OneDay_AllExtras()
        {
            FillForm("1", "3", "3", "да", "да");
            Assert.AreEqual("10700", GetResult());
        }

        [TestMethod]
        public void Test_InvalidRoomType_ShouldShowError()
        {
            FillForm("2", "5", "2", "нет", "да");
            string result = GetResult();
            Assert.IsTrue(string.IsNullOrEmpty(result), "Результат должен быть пустым при неверном типе номера.");
        }

        [TestMethod]
        public void Test_ZeroDays_ShouldShowErrorOrEmpty()
        {
            FillForm("0", "1", "1", "нет", "нет");
            string result = GetResult();
            Assert.IsTrue(string.IsNullOrEmpty(result) || result == "0", "Результат должен быть пустым или 0 при 0 днях.");
        }

        [TestMethod]
        public void Test_OneDay_MaxRoom_OneGuest_NoExtras()
        {
            FillForm("1", "3", "1", "нет", "нет");
            Assert.AreEqual("8000", GetResult());
        }

        private void FillForm(string days, string roomType, string guests, string safe, string breakfast)
        {
            var tbDays = driver.FindElementByAccessibilityId("inputDays");
            tbDays.Clear();
            tbDays.SendKeys(days);

            var tbRoomType = driver.FindElementByAccessibilityId("inputRoomType");
            tbRoomType.Clear();
            tbRoomType.SendKeys(roomType);

            var tbGuests = driver.FindElementByAccessibilityId("inputGuestCount");
            tbGuests.Clear();
            tbGuests.SendKeys(guests);

            var tbSafe = driver.FindElementByAccessibilityId("inputSafeOption");
            tbSafe.Clear();
            tbSafe.SendKeys(safe);

            var tbBreakfast = driver.FindElementByAccessibilityId("inputBreakfastOption");
            tbBreakfast.Clear();
            tbBreakfast.SendKeys(breakfast);

            driver.FindElementByAccessibilityId("btnCompute").Click();
        }

        private string GetResult()
        {
            var output = driver.FindElementByAccessibilityId("outputTotal").Text;
            return output.Trim();
        }
    }
}
