using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace ST9_T
{
    [TestClass]
    public sealed class Test1
    {
        private WindowsDriver<WindowsElement> driver;

        [TestInitialize]
        public void Initialize()
        {
            string appPath = @"C:\Users\Reaper\source\repos\ST-9\bin\Debug\net8.0-windows\ST9-WF.exe";
            string driverURL = @"http://127.0.0.1:4723";

            AppiumOptions options = new AppiumOptions();
            options.AddAdditionalCapability("app", appPath);
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            options.AddAdditionalCapability("ms:waitForAppLaunch", "5");

            this.driver = new WindowsDriver<WindowsElement>(new Uri(driverURL), options);
        }

        [TestCleanup]
        public void Cleanup() { driver?.Quit(); }

        private string Calculate()
        {
            driver.FindElementByAccessibilityId("btnCalculate").Click();
            System.Threading.Thread.Sleep(500); // Ожидание расчета
            return driver.FindElementByAccessibilityId("textBoxTotal").Text;
        }

        private void FillForm(string days, string category, string guests, bool safe, bool breakfast)
        {
            // Очистка полей
            driver.FindElementByAccessibilityId("textBoxDays").Clear();
            driver.FindElementByAccessibilityId("textBoxCategory").Clear();
            driver.FindElementByAccessibilityId("textBoxGuests").Clear();

            // Заполнение полей
            driver.FindElementByAccessibilityId("textBoxDays").SendKeys(days);
            driver.FindElementByAccessibilityId("textBoxCategory").SendKeys(category);
            driver.FindElementByAccessibilityId("textBoxGuests").SendKeys(guests);

            // Установка чекбоксов
            SetCheckbox("checkBoxSafe", safe);
            SetCheckbox("checkBoxBreakfast", breakfast);
        }

        private void SetCheckbox(string elementId, bool state)
        {
            var checkbox = driver.FindElementByAccessibilityId(elementId);
            if (checkbox.Selected != state)
            {
                checkbox.Click();
            }
        }

        [TestMethod]
        public void TestEconomySingleGuest()
        {
            FillForm("2", "3", "1", false, false);
            Assert.AreEqual("2 000,00 ₽", Calculate());
        }

        [TestMethod]
        public void TestLuxTwoGuestsWithOptions()
        {
            FillForm("3", "1", "2", true, true);
            Assert.AreEqual("15 900,00 ₽", Calculate());
        }

        [TestMethod]
        public void TestStandardThreeGuests()
        {
            FillForm("5", "2", "3", false, false);
            Assert.AreEqual("14 000,00 ₽", Calculate());
        }

        [TestMethod]
        public void TestOptionsOnly()
        {
            FillForm("1", "3", "1", true, true);
            Assert.AreEqual("1 800,00 ₽", Calculate());
        }

        [TestMethod]
        public void TestInvalidCategory()
        {
            FillForm("2", "4", "1", false, false);
            var result = Calculate();
            Assert.IsTrue(result.Contains("Ошибка"), "Ожидалось сообщение об ошибке");
        }

        [TestMethod]
        public void TestNegativeDays()
        {
            FillForm("-1", "1", "1", false, false);
            var result = Calculate();
            Assert.IsTrue(result.Contains("Ошибка"), "Ожидалось сообщение об ошибке");
        }
        [TestMethod]
        public void TestMaximumDaysLimit()
        {
            FillForm("365", "1", "2", true, true);
            Assert.AreEqual("1 971 900,00 ₽", Calculate());
        }

        [TestMethod]
        public void TestSingleDayWithOptions()
        {
            FillForm("1", "2", "1", true, true);
            Assert.AreEqual("3 500,00 ₽", Calculate());
        }

        [TestMethod]
        public void TestNonNumericInput()
        {
            FillForm("два", "1", "три", false, false);
            var result = Calculate();
            Assert.IsTrue(result.Contains("Ошибка"), "Ожидалась ошибка формата");
        }

        [TestMethod]
        public void TestOnlySafeOption()
        {
            FillForm("2", "3", "1", true, false);
            Assert.AreEqual("3 400,00 ₽", Calculate());
        }

        [TestMethod]
        public void TestOnlyBreakfastOption()
        {
            FillForm("2", "2", "1", false, true);
            Assert.AreEqual("5 000,00 ₽", Calculate());
        }

        [TestMethod]
        public void TestEmptyFields()
        {
            FillForm("", "", "", false, false);
            var result = Calculate();
            Assert.IsTrue(result.Contains("Ошибка"), "Ожидалась ошибка пустых полей");
        }

        [TestMethod]
        public void TestMixedCaseInput()
        {
            FillForm("3", "ЛюКс", "2", true, true);
            var result = Calculate();
            Assert.IsTrue(result.Contains("Ошибка") || result.Contains("10 200,00 ₽"),
                "Проверка чувствительности к регистру");
        }

        [TestMethod]
        public void TestZeroGuests()
        {
            FillForm("2", "1", "0", false, false);
            var result = Calculate();
            Assert.IsTrue(result.Contains("Ошибка"), "Ожидалась ошибка нулевых гостей");
        }
    }
}
