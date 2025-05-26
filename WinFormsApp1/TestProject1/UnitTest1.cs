using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Text.RegularExpressions;

namespace TestProject1
{
    [TestClass]
    public class HotelCalculatorTests
    {
        public const string DriverUrl = "http://127.0.0.1:4723/";
        public WindowsDriver<WindowsElement> Driver;

        [TestInitialize]
        public void Setup()
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", @"C:\sema\ST-9\WinFormsApp1\WinFormsApp1\bin\Debug\net6.0-windows\WinFormsApp1.exe");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

            Driver = new WindowsDriver<WindowsElement>(new Uri(DriverUrl), appCapabilities);
        }

        [TestCleanup]
        public void TearDown()
        {
            Driver?.Quit();
        }

        [TestMethod]
        public void TestEconomySingleRoomWithoutExtras()
        {
            // Arrange
            EnterDays("5");
            SelectCategory("1 - Эконом");
            SelectCapacity("1");
            UncheckAllExtras();

            // Act
            ClickCalculate();

            // Assert
            Assert.IsTrue(Regex.IsMatch(GetTotalSum(), @"5\s*000[,.]00 ₽"));
        }

        [TestMethod]
        public void TestStandardDoubleRoomWithBreakfast()
        {
            // Arrange
            EnterDays("3");
            SelectCategory("2 - Стандарт");
            SelectCapacity("2");
            CheckBreakfast();
            UncheckSafe();

            // Act
            ClickCalculate();

            // Assert
            Assert.IsTrue(Regex.IsMatch(GetTotalSum(), @"9\s*600[,.]00 ₽"));
        }

        [TestMethod]
        public void TestLuxuryTripleRoomWithAllExtras()
        {
            // Arrange
            EnterDays("2");
            SelectCategory("3 - Люкс");
            SelectCapacity("3");
            CheckAllExtras();

            // Act
            ClickCalculate();

            // Assert
            Assert.IsTrue(Regex.IsMatch(GetTotalSum(), @"17\s*600[,.]00 ₽"));
        }

        [TestMethod]
        public void TestOneDayEconomySingleRoomWithSafe()
        {
            // Arrange
            EnterDays("1");
            SelectCategory("1 - Эконом");
            SelectCapacity("1");
            CheckSafe();
            UncheckBreakfast();

            // Act
            ClickCalculate();

            // Assert
            Assert.IsTrue(Regex.IsMatch(GetTotalSum(), @"1\s*500[,.]00 ₽"));
        }

        [TestMethod]
        public void TestThreeDayEconomySingleRoomWithSafe()
        {
            // Arrange
            EnterDays("3");
            SelectCategory("1 - Эконом");
            SelectCapacity("1");
            CheckSafe();
            UncheckBreakfast();

            // Act
            ClickCalculate();

            // Assert
            Assert.IsTrue(Regex.IsMatch(GetTotalSum(), @"4\s*500[,.]00 ₽"));
        }

        // Вспомогательные методы
        private void EnterDays(string days)
        {
            var daysTextBox = Driver.FindElementByAccessibilityId("daysTextBox");
            daysTextBox.Clear();
            daysTextBox.SendKeys(days);
        }

        private void SelectCategory(string category)
        {
            var categoryComboBox = Driver.FindElementByAccessibilityId("categoryComboBox");
            categoryComboBox.SendKeys(category);
        }

        private void SelectCapacity(string capacity)
        {
            var capacityComboBox = Driver.FindElementByAccessibilityId("capacityComboBox");
            capacityComboBox.SendKeys(capacity);
        }

        private void CheckSafe()
        {
            var safeCheckBox = Driver.FindElementByAccessibilityId("safeCheckBox");
            if (!safeCheckBox.Selected) safeCheckBox.Click();
        }

        private void UncheckSafe()
        {
            var safeCheckBox = Driver.FindElementByAccessibilityId("safeCheckBox");
            if (safeCheckBox.Selected) safeCheckBox.Click();
        }

        private void CheckBreakfast()
        {
            var breakfastCheckBox = Driver.FindElementByAccessibilityId("breakfastCheckBox");
            if (!breakfastCheckBox.Selected) breakfastCheckBox.Click();
        }

        private void UncheckBreakfast()
        {
            var breakfastCheckBox = Driver.FindElementByAccessibilityId("breakfastCheckBox");
            if (breakfastCheckBox.Selected) breakfastCheckBox.Click();
        }

        private void CheckAllExtras()
        {
            CheckSafe();
            CheckBreakfast();
        }

        private void UncheckAllExtras()
        {
            UncheckSafe();
            UncheckBreakfast();
        }

        private void ClickCalculate()
        {
            var calculateButton = Driver.FindElementByAccessibilityId("calculateButton");
            calculateButton.Click();
        }

        private string GetTotalSum()
        {
            var sumTextBox = Driver.FindElementByAccessibilityId("sumTextBox");
            return sumTextBox.Text;
        }

        private void ClearAllFields()
        {
            var clearButton = Driver.FindElementByAccessibilityId("clearButton");
            clearButton.Click();
        }
    }
}