using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace HotelCalculatorTests
{
    [TestClass]
    public class HotelCalculatorTests
    {
        public const string DriveURL = "http://127.0.0.1:4723/";
        public WindowsDriver<WindowsElement> driver;

        [TestInitialize]
        public void Setup()
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", @"C:\Users\Ofuus\source\repos\WinFormsAppium-2\bin\Debug\net8.0-windows\WinFormsAppium-2.exe");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

            driver = new WindowsDriver<WindowsElement>(new Uri(DriveURL), appCapabilities);
            Thread.Sleep(2000); // Дополнительная задержка
        }

        [TestCleanup]
        public void Teardown()
        {
            driver?.Quit();
        }

        private void FillAndCalculate(string days, string category, string capacity, string safe, string breakfast)
        {
            driver.FindElementByAccessibilityId("txtDays").Clear();
            driver.FindElementByAccessibilityId("txtDays").SendKeys(days);

            driver.FindElementByAccessibilityId("txtCategory").Clear();
            driver.FindElementByAccessibilityId("txtCategory").SendKeys(category);

            driver.FindElementByAccessibilityId("txtCapacity").Clear();
            driver.FindElementByAccessibilityId("txtCapacity").SendKeys(capacity);

            driver.FindElementByAccessibilityId("txtSafe").Clear();
            driver.FindElementByAccessibilityId("txtSafe").SendKeys(safe);

            driver.FindElementByAccessibilityId("txtBreakfast").Clear();
            driver.FindElementByAccessibilityId("txtBreakfast").SendKeys(breakfast);

            driver.FindElementByAccessibilityId("btnCalculate").Click();
            Thread.Sleep(1000); // Задержка для обновления UI
        }

        [TestMethod]
        public void EconomySingleNoExtras()
        {
            FillAndCalculate("1", "1", "1", "нет", "нет");
            var totalSum = driver.FindElementByAccessibilityId("txtSum").Text;
            // 1 день * (1000 (эконом) + (1-1)*500) = 1000.00
            Assert.AreEqual("1000,00", totalSum, "Базовый расчет для эконома не совпадает");
        }

        [TestMethod]
        public void StandardDoubleWithSafe()
        {
            FillAndCalculate("2", "2", "2", "да", "нет");
            var totalSum = driver.FindElementByAccessibilityId("txtSum").Text;
            // 2 дня * (2000 (стандарт) + (2-1)*500 + 200 (сейф)) = 2 * (2000 + 500 + 200) = 2 * 2700 = 5400.00
            Assert.AreEqual("5400,00", totalSum, "Расчет для стандарта с сейфом не совпадает");
        }

        [TestMethod]
        public void LuxTripleWithBreakfast()
        {
            FillAndCalculate("3", "3", "3", "нет", "да");
            var totalSum = driver.FindElementByAccessibilityId("txtSum").Text;
            // 3 дня * (3000 (люкс) + (3-1)*500 + 300 (завтрак)) = 3 * (3000 + 1000 + 300) = 3 * 4300 = 12900.00
            Assert.AreEqual("12900,00", totalSum, "Расчет для люкса с завтраком не совпадает");
        }

        [TestMethod]
        public void EconomySingleAllExtras()
        {
            FillAndCalculate("5", "1", "1", "да", "да");
            var totalSum = driver.FindElementByAccessibilityId("txtSum").Text;
            Assert.AreEqual("7500,00", totalSum, "Расчет для эконома со всеми опциями не совпадает");
        }

        [TestMethod]
        public void InvalidCategoryInput()
        {
            FillAndCalculate("1", "4", "1", "нет", "нет");
            var totalSum = driver.FindElementByAccessibilityId("txtSum").Text;
            // При некорректной категории поле должно остаться пустым
            Assert.AreEqual("", totalSum, "Ожидалась пустая строка при некорректной категории");
        }
    }
} // swaga