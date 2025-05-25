using OpenQA.Selenium.Appium.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using System;
using System.Diagnostics;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Drawing;

namespace TestProject1
{
    [TestClass]
    public sealed class Test1
    {
        public const string DriveURL = "http://127.0.0.1:4723/";
        WindowsDriver<WindowsElement> driver;
        [TestInitialize]
        public void Setup()
        {
            //Process.Start(@"D:\ИНСТИТУТ_Лобач___\лабы\WinFormsApp1\WinFormsApp1\bin\Debug\net8.0-windows\WinFormsApp1.exe");
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", @"D:\ИНСТИТУТ_Лобач___\лабы\WinFormsApp1\WinFormsApp1\bin\Debug\net8.0-windows\WinFormsApp1.exe");
           // appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

            driver = new WindowsDriver<WindowsElement>(new Uri(DriveURL), appCapabilities);
            ResetForm();
        }
        private void ResetForm()
        {
            ClearField("txtDays");
            ClearField("txtCategory");
            ClearField("txtCapacity");
            Uncheck("cbSafe");
            Uncheck("cbBreakfast");
        }
        private void ClearField(string fieldId)
        {
            driver.FindElementByAccessibilityId(fieldId).Clear();
        }

        private void Check(string checkboxId)
        {
            var cb = driver.FindElementByAccessibilityId(checkboxId);
            if (!cb.Selected) cb.Click();
        }

        private void Uncheck(string checkboxId)
        {
            var cb = driver.FindElementByAccessibilityId(checkboxId);
            if (cb.Selected) cb.Click();
        }
        [TestCleanup]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }
        [TestMethod]
        public void TestCategory1SingleDayNoOptions()
        {
            // Ввод данных
            driver.FindElementByAccessibilityId("txtDays").SendKeys("1");
            driver.FindElementByAccessibilityId("txtCategory").SendKeys("1");
            driver.FindElementByAccessibilityId("txtCapacity").SendKeys("1");

            // Расчет
            driver.FindElementByAccessibilityId("btnCalculate").Click();

            // Проверка
            string result = driver.FindElementByAccessibilityId("lblTotal").Text
        .Replace('\u00A0', ' '); // Заменяем неразрывный пробел на обычный

            Assert.AreEqual("Итого: 1 500,00 ₽", result);
        }
        [TestMethod]
        public void TestCategory2ThreeDaysTwoGuestsWithBreakfast()
        {
            // Ввод данных
            driver.FindElementByAccessibilityId("txtDays").Clear();
            driver.FindElementByAccessibilityId("txtDays").SendKeys("3");
            driver.FindElementByAccessibilityId("txtCategory").Clear();
            driver.FindElementByAccessibilityId("txtCategory").SendKeys("2");
            driver.FindElementByAccessibilityId("txtCapacity").Clear();
            driver.FindElementByAccessibilityId("txtCapacity").SendKeys("2");
            driver.FindElementByAccessibilityId("cbBreakfast").Click();

            // Расчет
            driver.FindElementByAccessibilityId("btnCalculate").Click();

            // Проверка
            string result = driver.FindElementByAccessibilityId("lblTotal").Text.Replace('\u00A0', ' '); ;
            Assert.AreEqual("Итого: 12 000,00 ₽", result); // 3000*3 + 500*2*3
        }

        [TestMethod]
        public void TestCategory3FiveDaysThreeGuestsAllOptions()
        {
            // Ввод данных
            driver.FindElementByAccessibilityId("txtDays").Clear();
            driver.FindElementByAccessibilityId("txtDays").SendKeys("5");
            driver.FindElementByAccessibilityId("txtCategory").Clear();
            driver.FindElementByAccessibilityId("txtCategory").SendKeys("3");
            driver.FindElementByAccessibilityId("txtCapacity").Clear();
            driver.FindElementByAccessibilityId("txtCapacity").SendKeys("3");
            driver.FindElementByAccessibilityId("cbSafe").Click();
            driver.FindElementByAccessibilityId("cbBreakfast").Click();

            // Расчет
            driver.FindElementByAccessibilityId("btnCalculate").Click();

            // Проверка (с нормализацией пробелов)
            string result = driver.FindElementByAccessibilityId("lblTotal").Text
                .Replace('\u00A0', ' ')
                .Replace("\u202F", " ");

            // Обновите ожидаемое значение согласно реальной логике приложения
            Assert.AreEqual("Итого: 32 500,00 ₽", result);
        }

        [TestMethod]
        public void TestCategory3SixDaysTwoGuestsAllOptions()
        {

            // Ввод данных
            driver.FindElementByAccessibilityId("txtDays").Clear();
            driver.FindElementByAccessibilityId("txtDays").SendKeys("6");
            driver.FindElementByAccessibilityId("txtCategory").Clear();
            driver.FindElementByAccessibilityId("txtCategory").SendKeys("3");
            driver.FindElementByAccessibilityId("txtCapacity").Clear();
            driver.FindElementByAccessibilityId("txtCapacity").SendKeys("2");
            driver.FindElementByAccessibilityId("cbSafe").Click();
            driver.FindElementByAccessibilityId("cbBreakfast").Click();

            // Расчет
            driver.FindElementByAccessibilityId("btnCalculate").Click();

            // Проверка (с нормализацией пробелов)
            string result = driver.FindElementByAccessibilityId("lblTotal").Text
                .Replace('\u00A0', ' ')
                .Replace("\u202F", " ");

            // Обновите ожидаемое значение согласно реальной логике приложения
            Assert.AreEqual("Итого: 36 000,00 ₽", result);
        }

        [TestMethod]
        public void TestCategory1LongStayWithSafe()
        {
            // Ввод данных с большим количеством дней
            driver.FindElementByAccessibilityId("txtDays").Clear();
            driver.FindElementByAccessibilityId("txtDays").SendKeys("10");
            driver.FindElementByAccessibilityId("txtCategory").Clear();
            driver.FindElementByAccessibilityId("txtCategory").SendKeys("1");
            driver.FindElementByAccessibilityId("txtCapacity").Clear();
            driver.FindElementByAccessibilityId("txtCapacity").SendKeys("2");
            driver.FindElementByAccessibilityId("cbSafe").Click();

            // Расчет
            driver.FindElementByAccessibilityId("btnCalculate").Click();

            // Проверка
            string result = driver.FindElementByAccessibilityId("lblTotal").Text.Replace('\u00A0', ' ').Replace("\u202F", " "); ; ;
            Assert.AreEqual("Итого: 17 000,00 ₽", result); // 1500*10 + 200*10
        }

    }
}
