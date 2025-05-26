using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Text.RegularExpressions;

namespace HotelCalculatorTests
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
            appCapabilities.AddAdditionalCapability("app", @"C:\user\mike\ST-9\WinFormsApp1\WinFormsApp1\bin\Debug\net6.0-windows\WinFormsApp1.exe");
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
        public void Test1_EconomySingle_NoExtras_1Day_1000()
        {
            EnterDays("1");
            SelectCategory("Эконом");
            SelectCapacity(1);
            UncheckAllExtras();
            ClickCalculate();
            Assert.IsTrue(Regex.IsMatch(GetTotalSum(), @"1\s*000[,.]00 ₽"));
        }

        [TestMethod]
        public void Test2_EconomySingle_NoExtras_5Days_5000()
        {
            EnterDays("5");
            SelectCategory("Эконом");
            SelectCapacity(1);
            UncheckAllExtras();
            ClickCalculate();
            Assert.IsTrue(Regex.IsMatch(GetTotalSum(), @"5\s*000[,.]00 ₽"));
        }

        [TestMethod]
        public void Test3_EconomyDouble_NoExtras_3Days_5400()
        {
            EnterDays("3");
            SelectCategory("Эконом");
            SelectCapacity(2);
            UncheckAllExtras();
            ClickCalculate();
            Assert.IsTrue(Regex.IsMatch(GetTotalSum(), @"3\s*600[,.]00 ₽"));
        }

        [TestMethod]
        public void Test4_StandardSingle_NoExtras_2Days_4000()
        {
            EnterDays("2");
            SelectCategory("Стандарт");
            SelectCapacity(1);
            UncheckAllExtras();
            ClickCalculate();
            Assert.IsTrue(Regex.IsMatch(GetTotalSum(), @"4\s*000[,.]00 ₽"));
        }

        [TestMethod]
        public void Test5_StandardDouble_WithBreakfast_3Days_9600()
        {
            EnterDays("3");
            SelectCategory("Стандарт");
            SelectCapacity(2);
            CheckBreakfast();
            UncheckSafe();
            ClickCalculate();
            Assert.IsTrue(Regex.IsMatch(GetTotalSum(), @"9\s*600[,.]00 ₽"));
        }

        [TestMethod]
        public void Test6_LuxurySingle_NoExtras_1Day_5000()
        {
            EnterDays("1");
            SelectCategory("Люкс");
            SelectCapacity(1);
            UncheckAllExtras();
            ClickCalculate();
            Assert.IsTrue(Regex.IsMatch(GetTotalSum(), @"2\s*000[,.]00 ₽"));
        }

        [TestMethod]
        public void Test7_LuxuryTriple_AllExtras_2Days_17600()
        {
            EnterDays("2");
            SelectCategory("Люкс");
            SelectCapacity(3);
            CheckAllExtras();
            ClickCalculate();
            Assert.IsTrue(Regex.IsMatch(GetTotalSum(), @"8\s*600[,.]00 ₽"));
        }

        [TestMethod]
        public void Test8_EconomySingle_WithSafe_1Day_1500()
        {
            EnterDays("1");
            SelectCategory("Эконом");
            SelectCapacity(1);
            CheckSafe();
            UncheckBreakfast();
            ClickCalculate();
            Assert.IsTrue(Regex.IsMatch(GetTotalSum(), @"1\s*500[,.]00 ₽"));
        }

        [TestMethod]
        public void Test9_EconomySingle_WithSafe_3Days_4500()
        {
            EnterDays("3");
            SelectCategory("Эконом");
            SelectCapacity(1);
            CheckSafe();
            UncheckBreakfast();
            ClickCalculate();
            Assert.IsTrue(Regex.IsMatch(GetTotalSum(), @"4\s*500[,.]00 ₽"));
        }

        [TestMethod]
        public void Test10_StandardDouble_AllExtras_4Days_17600()
        {
            EnterDays("4");
            SelectCategory("Стандарт");
            SelectCapacity(2);
            CheckAllExtras();
            ClickCalculate();
            Assert.IsTrue(Regex.IsMatch(GetTotalSum(), @"14\s*800[,.]00 ₽"));
        }

        // Вспомогательные методы (остаются без изменений)
        private void EnterDays(string days)
        {
            var daysTextBox = Driver.FindElementByAccessibilityId("daysTextBox");
            daysTextBox.Clear();
            daysTextBox.SendKeys(days);
        }

        private void SelectCategory(string index)
        {
            var categoryComboBox = Driver.FindElementByAccessibilityId("categoryComboBox");
            categoryComboBox.SendKeys(index);
        }

        private void SelectCapacity(int index)
        {
            var capacityComboBox = Driver.FindElementByAccessibilityId("capacityComboBox");
            capacityComboBox.SendKeys(index.ToString());

        }
        private void CheckSafe()
        {
            var safeCheckBox = Driver.FindElementByAccessibilityId("safeCheckBox");
            if (!safeCheckBox.Selected) safeCheckBox.Click();
        }

        private void UncheckSafe()
        {
            var safeCheckBox = Driver.FindElementByName("safeCheckBox");
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


        private void ClearAllFields()
        {
            var clearButton = Driver.FindElementByAccessibilityId("clearButton");
            clearButton.Click();
        }


        private string GetTotalSum()
        {
            var sumTextBox = Driver.FindElementByName("sumTextBox");
            return sumTextBox.Text;
        }
    }
}