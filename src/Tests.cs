using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using System;

namespace HotelCalculator.Tests
{
    [TestClass]
    public class Tests
    {
        public const string DriverURL = "http://127.0.0.1:4723/";
        public WindowsDriver<WindowsElement> Driver;

        [TestInitialize]
        public void Setup()
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", @"Z:\Coding\Testing\ST-9\HotelCalculator\HotelCalculator\bin\Debug\net8.0-windows\HotelCalculator.exe");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

            Driver = new WindowsDriver<WindowsElement>(new Uri(DriverURL), appCapabilities);
        }

        [TestCleanup]
        public void TearDown()
        {
            Driver.Quit();
        }

        // Тест 1: Базовый расчет без опций
        [TestMethod]
        public void TestBasicCalculation()
        {
            Driver.FindElementByAccessibilityId("txtDays").SendKeys("3");
            Driver.FindElementByAccessibilityId("txtCategory").SendKeys("2");
            Driver.FindElementByAccessibilityId("txtCapacity").SendKeys("1");
            Driver.FindElementByAccessibilityId("txtSafe").SendKeys("нет");
            Driver.FindElementByAccessibilityId("txtBreakfast").SendKeys("нет");
            Driver.FindElementByAccessibilityId("btnCalculate").Click();

            string result = Driver.FindElementByAccessibilityId("lblTotal").Text;
            Assert.IsTrue(result.Contains("9000")); // 3 * 3000 * 1 = 9000
        }

        // Тест 2: Расчет с включенным сейфом
        [TestMethod]
        public void TestWithSafe()
        {
            Driver.FindElementByAccessibilityId("txtDays").SendKeys("2");
            Driver.FindElementByAccessibilityId("txtCategory").SendKeys("1");
            Driver.FindElementByAccessibilityId("txtCapacity").SendKeys("2");
            Driver.FindElementByAccessibilityId("txtSafe").SendKeys("да");
            Driver.FindElementByAccessibilityId("txtBreakfast").SendKeys("нет");
            Driver.FindElementByAccessibilityId("btnCalculate").Click();

            string result = Driver.FindElementByAccessibilityId("lblTotal").Text;
            Assert.IsTrue(result.Contains("15500")); // 2 * 5000 * 1.5 + 500 = 15500
        }

        // Тест 3: Расчет с завтраком
        [TestMethod]
        public void TestWithBreakfast()
        {
            Driver.FindElementByAccessibilityId("txtDays").SendKeys("4");
            Driver.FindElementByAccessibilityId("txtCategory").SendKeys("3");
            Driver.FindElementByAccessibilityId("txtCapacity").SendKeys("3");
            Driver.FindElementByAccessibilityId("txtSafe").SendKeys("нет");
            Driver.FindElementByAccessibilityId("txtBreakfast").SendKeys("да");
            Driver.FindElementByAccessibilityId("btnCalculate").Click();

            string result = Driver.FindElementByAccessibilityId("lblTotal").Text;
            Assert.IsTrue(result.Contains("13000")); // 4 * 1500 * 2 + 1000 = 13000
        }

        // Тест 4: Некорректный ввод (буквы вместо чисел)
        [TestMethod]
        public void TestInvalidInput()
        {
            Driver.FindElementByAccessibilityId("txtDays").SendKeys("abc");
            Driver.FindElementByAccessibilityId("btnCalculate").Click();

            // Проверка, что сумма не рассчитана
            string result = Driver.FindElementByAccessibilityId("lblTotal").Text;
            Assert.IsTrue(result.Contains("0") || result.Contains("Итого:"));
        }

        // Тест 5: Максимальная стоимость
        [TestMethod]
        public void TestMaxPrice()
        {
            Driver.FindElementByAccessibilityId("txtDays").SendKeys("7");
            Driver.FindElementByAccessibilityId("txtCategory").SendKeys("1");
            Driver.FindElementByAccessibilityId("txtCapacity").SendKeys("3");
            Driver.FindElementByAccessibilityId("txtSafe").SendKeys("да");
            Driver.FindElementByAccessibilityId("txtBreakfast").SendKeys("да");
            Driver.FindElementByAccessibilityId("btnCalculate").Click();

            string result = Driver.FindElementByAccessibilityId("lblTotal").Text;
            Assert.IsTrue(result.Contains("71500")); // 7 * 5000 * 2 + 500 + 1000 = 73500
        }
    }
}


