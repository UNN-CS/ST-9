using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Xml.Linq;

namespace CalculatorTest
{
    [TestClass]
    public sealed class Test1
    {
        public const string DriverUrl = "http://127.0.0.1:4723/";
        private WindowsDriver<WindowsElement> _driver;
        [TestInitialize]
        public void TestInitialize()
        {
            AppiumOptions appOptions = new AppiumOptions();
            appOptions.AddAdditionalCapability("app", @"D:\learning\ST-8\HotelCalculator\HotelCalculator\bin\Debug\net9.0-windows\HotelCalculator.exe");
            appOptions.AddAdditionalCapability("deviceName", "WindowsPC");
            appOptions.AddAdditionalCapability("ms:waitForAppLaunch", "3");
            _driver = new WindowsDriver<WindowsElement>(new Uri(DriverUrl), appOptions);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver?.Quit();
        }

        [TestMethod]
        [DoNotParallelize]
        public void TestEconomyRoomWithoutExtras()
        {
            SetNumericValue("numericDays", 1);
            SetComboBoxValue("comboCategoryBox", 3);
            SetComboBoxValue("comboCapacity", 1);
            UncheckAllOptions();
            CalculateAndAssert("2 000 R");
        }

        [TestMethod]
        [DoNotParallelize]
        public void TestStandardRoomWithCapacity()
        {
            SetNumericValue("numericDays", 3);
            SetComboBoxValue("comboCategoryBox", 2);
            SetComboBoxValue("comboCapacity", 2);
            UncheckAllOptions();
            CalculateAndAssert("13 500 R");
        }

        [TestMethod]
        [DoNotParallelize]
        public void TestLuxWithSafe()
        {
            SetNumericValue("numericDays", 2);
            SetComboBoxValue("comboCategoryBox", 1);
            SetComboBoxValue("comboCapacity", 1);
            CheckSafe();
            CalculateAndAssert("11 000 R");
        }

        [TestMethod]
        [DoNotParallelize]
        public void TestRoomWithBreakfast()
        {
            SetNumericValue("numericDays", 1);
            SetComboBoxValue("comboCategoryBox", 1);
            SetComboBoxValue("comboCapacity", 3);
            CheckBreakfast();
            CalculateAndAssert("11 400 R");
        }

        [TestMethod]
        [DoNotParallelize]
        public void TestAllOptions()
        {
            SetNumericValue("numericDays", 3);
            SetComboBoxValue("comboCategoryBox", 1);
            SetComboBoxValue("comboCapacity", 2);
            CheckSafe();
            CheckBreakfast();
            CalculateAndAssert("27 300 R");
        }

        private void SetNumericValue(string elementId, int value)
        {
            var numericUpDown = _driver.FindElementByAccessibilityId(elementId);

            // Установка значения через стрелки
            for (int i = 0; i < value; i++)
            {
                numericUpDown.SendKeys(Keys.ArrowUp);
                Thread.Sleep(100);
            }
        }

        private void SetComboBoxValue(string elementId, int targetIndex)
        {
            var comboBox = _driver.FindElementByAccessibilityId(elementId);

            // Раскрытие списка
            comboBox.Click();
            Thread.Sleep(200);

            // Выбор значения
            for (int i = 0; i < targetIndex-1; i++)
            {
                comboBox.SendKeys(Keys.ArrowDown);
                Thread.Sleep(100);
            }

            comboBox.SendKeys(Keys.Enter);
        }

        private void CheckSafe()
        {
            var safeCheckbox = _driver.FindElementByAccessibilityId("checkSafe");
            if (!safeCheckbox.Selected)
                safeCheckbox.Click();
        }

        private void CheckBreakfast()
        {
            var breakfastCheckbox = _driver.FindElementByAccessibilityId("checkBreakfast");
            if (!breakfastCheckbox.Selected)
                breakfastCheckbox.Click();
        }

        private void UncheckAllOptions()
        {
            var safeCheckbox = _driver.FindElementByAccessibilityId("checkSafe");
            if (safeCheckbox.Selected)
                safeCheckbox.Click();

            var breakfastCheckbox = _driver.FindElementByAccessibilityId("checkBreakfast");
            if (breakfastCheckbox.Selected)
                breakfastCheckbox.Click();
        }

        private void CalculateAndAssert(string expected)
        {
            _driver.FindElementByAccessibilityId("btnCalculate").Click();

            var resultText = _driver.FindElementByAccessibilityId("txtSum").Text;
            // Удалить все нецифровые символы
            string actualNumber = new string(resultText.Where(char.IsDigit).ToArray());
            string expectedNumber = new string(expected.Where(char.IsDigit).ToArray());

            Assert.AreEqual(expectedNumber, actualNumber);
        }
    }
}
