using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Globalization;
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
            appCapabilities.AddAdditionalCapability("app", @"C:\Users\Knight\source\repos\GuestHotelCalc\GuestHotelCalc\bin\Debug\net8.0-windows\GuestHotelCalc.exe");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

            driver = new WindowsDriver<WindowsElement>(new Uri(DriveURL), appCapabilities);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TestCleanup]
        public void Teardown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }

        // Вспомогательный метод для заполнения полей и расчета
        private void FillAndCalculate(string days, string category, string capacity, string safe, string breakfast)
        {
            driver.FindElementByAccessibilityId("textBoxDays").Clear();
            driver.FindElementByAccessibilityId("textBoxDays").SendKeys(days);

            driver.FindElementByAccessibilityId("textBoxCategory").Clear();
            driver.FindElementByAccessibilityId("textBoxCategory").SendKeys(category);

            driver.FindElementByAccessibilityId("textBoxCapacity").Clear();
            driver.FindElementByAccessibilityId("textBoxCapacity").SendKeys(capacity);

            driver.FindElementByAccessibilityId("textBoxSafe").Clear();
            driver.FindElementByAccessibilityId("textBoxSafe").SendKeys(safe);

            driver.FindElementByAccessibilityId("textBoxBreakfast").Clear();
            driver.FindElementByAccessibilityId("textBoxBreakfast").SendKeys(breakfast);

            driver.FindElementByAccessibilityId("buttonCalculate").Click();
            Thread.Sleep(500); // Небольшая задержка для обновления UI
        }

        private decimal GetActualTotalSum()
        {
            var totalSumTextBox = driver.FindElementByAccessibilityId("textBoxTotalSum");
            string sumText = totalSumTextBox.Text;

            sumText = sumText.Replace(" ", "").Replace("$", "").Trim();

            if (decimal.TryParse(sumText, NumberStyles.Any, CultureInfo.CurrentCulture, out decimal parsedSum))
            {
                return parsedSum;
            }
            else if (decimal.TryParse(sumText, NumberStyles.Any, CultureInfo.InvariantCulture, out parsedSum))
            {
                return parsedSum;
            }
            else if (decimal.TryParse(sumText, NumberStyles.Any, new CultureInfo("ru-RU"), out parsedSum))
            {
                return parsedSum;
            }
            else
            {
                throw new FormatException($"Не удалось преобразовать строку '{sumText}' в число.");
            }
        }


        [TestMethod]
        public void TestMethod_EconomySingleNoExtras()
        {
            FillAndCalculate("1", "3", "1", "нет", "нет");
            decimal expectedSum = 1500.00M;
            decimal actualSum = GetActualTotalSum();
            Assert.AreEqual(expectedSum, actualSum);
        }

        [TestMethod]
        public void TestMethod_StandardDoubleWithSafe()
        {
            FillAndCalculate("2", "2", "2", "да", "нет");
            decimal expectedSum = 7600.00M;
            decimal actualSum = GetActualTotalSum();
            Assert.AreEqual(expectedSum, actualSum);
        }

        [TestMethod]
        public void TestMethod_LuxTripleWithBreakfast()
        {
            FillAndCalculate("3", "1", "3", "нет", "да");
            decimal expectedSum = 21750.00M;
            decimal actualSum = GetActualTotalSum();
            Assert.AreEqual(expectedSum, actualSum);
        }

        [TestMethod]
        public void TestMethod_EconomySingleAllExtras()
        {
            FillAndCalculate("5", "3", "1", "да", "да");
            decimal expectedSum = 11000.00M;
            decimal actualSum = GetActualTotalSum();
            Assert.AreEqual(expectedSum, actualSum);
        }

        [TestMethod]
        public void TestMethod_StandardDoubleAllExtras()
        {
            FillAndCalculate("4", "2", "2", "да", "да");
            decimal expectedSum = 17200.00M;
            decimal actualSum = GetActualTotalSum();
            Assert.AreEqual(expectedSum, actualSum);
        }

        [TestMethod]
        public void TestMethod_LuxTripleNoExtras()
        {
            FillAndCalculate("10", "1", "3", "нет", "нет");
            decimal expectedSum = 67500.00M;
            decimal actualSum = GetActualTotalSum();
            Assert.AreEqual(expectedSum, actualSum);
        }

        [TestMethod]
        public void TestMethod_InvalidCategoryInput()
        {
            FillAndCalculate("1", "4", "1", "нет", "нет"); // Неверная категория
            var totalSumTextBox = driver.FindElementByAccessibilityId("textBoxTotalSum");
            string sumText = totalSumTextBox.Text;
            Assert.IsFalse(decimal.TryParse(sumText.Replace(" ", "").Replace("$", "").Trim(), NumberStyles.Any, CultureInfo.CurrentCulture, out _),
                           "Сумма не должна быть числовой при неверном вводе.");
        }
    }
}