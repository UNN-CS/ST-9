using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;


namespace stT9
{
    [TestClass]
    public class UnitTest1
    {
        private static WindowsDriver<WindowsElement> driver;
        private static Process winAppDriverProcess;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // Запуск WinAppDriver перед всеми тестами
            winAppDriverProcess = Process.Start(@"C:\Program Files (x86)\Windows Application Driver\WinAppDriver.exe");
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            // Остановка WinAppDriver после всех тестов
            winAppDriverProcess?.Kill();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var app = new AppiumOptions();
            app.AddAdditionalCapability("app", @"D:\Labs\St9\St9\bin\Debug\net7.0-windows\St9.exe");
            app.AddAdditionalCapability("ms:waitForAppLaunch", "10");
            app.AddAdditionalCapability("deviceName", "WindowsPC");
            driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), app);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Закрытие приложения и драйвера после каждого теста
            driver?.CloseApp();
            driver?.Quit();
            driver?.Dispose();
            driver = null;
        }

        [TestMethod]
        [Priority(1)]
        public void TestEconomySingleRoomNoExtras()
        {
            FillAndCalculate("1", "Эконом", "1", "нет", "нет", "нет");
            AssertResult("10000");
        }

        [TestMethod]
        [Priority(2)]
        public void TestStandardDoubleRoomWithBreakfast()
        {
            FillAndCalculate("2", "Стандарт", "2", "да", "нет", "нет");
            AssertResult("51000");
        }

        [TestMethod]
        [Priority(3)]
        public void TestLuxSingleRoomWithAllExtras()
        {
            FillAndCalculate("1", "Люкс", "1", "да", "да", "да");
            AssertResult("44000");
        }

        [TestMethod]
        [Priority(4)]
        public void TestEconomyThreeDaysThreePlacesWithCleaning()
        {
            FillAndCalculate("3", "Эконом", "3", "нет", "да", "нет");
            AssertResult("42000");
        }

        [TestMethod]
        [Priority(5)]
        public void TestUltimateLuxuryPackage()
        {
            FillAndCalculate("10", "Люкс", "4", "да", "да", "да");
            AssertResult("620000");
        }

        [TestMethod]
        [Priority(6)]
        public void TestWeirdEconomyCase()
        {
            FillAndCalculate("1", "Эконом", "5", "нет", "нет", "да");
            AssertResult("19000");
        }

        private void FillAndCalculate(string days, string roomType, string places,
                                    string breakfast, string cleaning, string parking)
        {
            ClearAndSendKeys("txtDays", days);
            ClearAndSendKeys("txtRoomType", roomType);
            ClearAndSendKeys("txtPlaces", places);
            ClearAndSendKeys("txtBreakfast", breakfast);
            ClearAndSendKeys("txtCleaning", cleaning);
            ClearAndSendKeys("txtParking", parking);

            driver.FindElementByAccessibilityId("btnCalculate").Click();
            Thread.Sleep(500); // Небольшая задержка для стабильности
        }

        private void ClearAndSendKeys(string elementId, string value)
        {
            var element = driver.FindElementByAccessibilityId(elementId);
            element.Clear();
            element.SendKeys(value);
        }

        private void AssertResult(string expected)
        {
            var result = driver.FindElementByAccessibilityId("txtResult");
            Assert.AreEqual(expected, result.Text);
        }
    }
}