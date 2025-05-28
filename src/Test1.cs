using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Threading;

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
            appCapabilities.AddAdditionalCapability("app", @"C:\Users\misha\Documents\Tests\HotelCalculator\HotelCalculator\bin\Debug\net8.0-windows\HotelCalculator.exe");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

            driver = new WindowsDriver<WindowsElement>(new Uri(DriveURL), appCapabilities);
            Thread.Sleep(2000); // Время на запуск
        }

        [TestCleanup]
        public void TearDown()
        {
            driver?.Quit();
        }

        [TestMethod]
        public void TestEconomRoomCalculation()
        {
            // Тест расчета для номера эконом-класса
            var tbDays = driver.FindElementByAccessibilityId("tbDays");
            tbDays.Clear();
            tbDays.SendKeys("3");

            var cbRoomType = driver.FindElementByAccessibilityId("cbRoomType");
            cbRoomType.Click();
            Thread.Sleep(500);
            cbRoomType.SendKeys("Эконом");

            var tbGuests = driver.FindElementByAccessibilityId("tbGuests");
            tbGuests.Clear();
            tbGuests.SendKeys("1");

            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            btnCalculate.Click();

            var tbResult = driver.FindElementByAccessibilityId("tbResult");
            Thread.Sleep(1000);

            // Ожидаемый результат: 1500 * 3 дня * 1 гость = 4500 руб.
            Assert.AreEqual("4500 руб.", tbResult.Text);
        }

        [TestMethod]
        public void TestStandardRoomWithAdditionalGuests()
        {
            // Тест расчета для номера стандарт с дополнительными гостями
            var tbDays = driver.FindElementByAccessibilityId("tbDays");
            tbDays.Clear();
            tbDays.SendKeys("2");

            var cbRoomType = driver.FindElementByAccessibilityId("cbRoomType");
            cbRoomType.Click();
            Thread.Sleep(500);
            cbRoomType.SendKeys("Стандарт");

            var tbGuests = driver.FindElementByAccessibilityId("tbGuests");
            tbGuests.Clear();
            tbGuests.SendKeys("2");

            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            btnCalculate.Click();

            var tbResult = driver.FindElementByAccessibilityId("tbResult");
            Thread.Sleep(1000);

            // Ожидаемый результат: 2500 * 2 дня * 1.5 (коэффициент за 2 гостей) = 7500 руб.
            Assert.AreEqual("7500 руб.", tbResult.Text);
        }

        [TestMethod]
        public void TestLuxuryRoomWithAllServices()
        {
            // Тест расчета для номера люкс со всеми дополнительными услугами
            var tbDays = driver.FindElementByAccessibilityId("tbDays");
            tbDays.Clear();
            tbDays.SendKeys("1");

            var cbRoomType = driver.FindElementByAccessibilityId("cbRoomType");
            cbRoomType.Click();
            Thread.Sleep(500);
            cbRoomType.SendKeys("Люкс");

            var tbGuests = driver.FindElementByAccessibilityId("tbGuests");
            tbGuests.Clear();
            tbGuests.SendKeys("1");

            // Включаем все дополнительные услуги
            var chkBreakfast = driver.FindElementByAccessibilityId("chkBreakfast");
            if (!chkBreakfast.Selected)
                chkBreakfast.Click();

            var chkWiFi = driver.FindElementByAccessibilityId("chkWiFi");
            if (!chkWiFi.Selected)
                chkWiFi.Click();

            var chkParking = driver.FindElementByAccessibilityId("chkParking");
            if (!chkParking.Selected)
                chkParking.Click();

            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            btnCalculate.Click();

            var tbResult = driver.FindElementByAccessibilityId("tbResult");
            Thread.Sleep(1000);

            // Ожидаемый результат: 4000 + 500 + 200 + 300 = 5000 руб.
            Assert.AreEqual("5000 руб.", tbResult.Text);
        }

        [TestMethod]
        public void TestMultipleDaysWithBreakfast()
        {
            // Тест расчета на несколько дней с завтраком
            var tbDays = driver.FindElementByAccessibilityId("tbDays");
            tbDays.Clear();
            tbDays.SendKeys("5");

            var cbRoomType = driver.FindElementByAccessibilityId("cbRoomType");
            cbRoomType.Click();
            Thread.Sleep(500);
            cbRoomType.SendKeys("Стандарт");

            var tbGuests = driver.FindElementByAccessibilityId("tbGuests");
            tbGuests.Clear();
            tbGuests.SendKeys("2");

            var chkBreakfast = driver.FindElementByAccessibilityId("chkBreakfast");
            if (!chkBreakfast.Selected)
                chkBreakfast.Click();

            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            btnCalculate.Click();

            var tbResult = driver.FindElementByAccessibilityId("tbResult");
            Thread.Sleep(1000);

            // Ожидаемый результат: 2500*5*1.5 + 500*5*2 = 18750 + 5000 = 23750 руб.
            Assert.AreEqual("23750 руб.", tbResult.Text);
        }

        [TestMethod]
        public void TestEconomRoomWithParkingOnly()
        {
            // Тест расчета для номера эконом только с парковкой
            var tbDays = driver.FindElementByAccessibilityId("tbDays");
            tbDays.Clear();
            tbDays.SendKeys("4");

            var cbRoomType = driver.FindElementByAccessibilityId("cbRoomType");
            cbRoomType.Click();
            Thread.Sleep(500);
            cbRoomType.SendKeys("Эконом");

            var tbGuests = driver.FindElementByAccessibilityId("tbGuests");
            tbGuests.Clear();
            tbGuests.SendKeys("1");

            // Снимаем все галочки (если они есть)
            var chkBreakfast = driver.FindElementByAccessibilityId("chkBreakfast");
            if (chkBreakfast.Selected)
                chkBreakfast.Click();

            var chkWiFi = driver.FindElementByAccessibilityId("chkWiFi");
            if (chkWiFi.Selected)
                chkWiFi.Click();

            // Включаем только парковку
            var chkParking = driver.FindElementByAccessibilityId("chkParking");
            if (!chkParking.Selected)
                chkParking.Click();

            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            btnCalculate.Click();

            var tbResult = driver.FindElementByAccessibilityId("tbResult");
            Thread.Sleep(1000);

            // Ожидаемый результат: 1500*4 + 300*4 = 6000 + 1200 = 7200 руб.
            Assert.AreEqual("7200 руб.", tbResult.Text);
        }

        [TestMethod]
        public void TestThreeGuestsInLuxuryRoom()
        {
            // Тест расчета для номера люкс с тремя гостями
            var tbDays = driver.FindElementByAccessibilityId("tbDays");
            tbDays.Clear();
            tbDays.SendKeys("2");

            var cbRoomType = driver.FindElementByAccessibilityId("cbRoomType");
            cbRoomType.Click();
            Thread.Sleep(500);
            cbRoomType.SendKeys("Люкс");

            var tbGuests = driver.FindElementByAccessibilityId("tbGuests");
            tbGuests.Clear();
            tbGuests.SendKeys("3");

            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            btnCalculate.Click();

            var tbResult = driver.FindElementByAccessibilityId("tbResult");
            Thread.Sleep(1000);

            // Ожидаемый результат: 4000*2*2 (коэффициент за 3 гостей = 1 + (3-1)*0.5 = 2) = 16000 руб.
            Assert.AreEqual("16000 руб.", tbResult.Text);
        }
    }
}