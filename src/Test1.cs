using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using System;

namespace TestProject1
{
    [TestClass]
    public class HotelCalculatorTests
    {
        private const string DriverUrl = "http://127.0.0.1:4723/";
        private WindowsDriver<WindowsElement> _driver;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", @"D:\project\ST\ST-9\WinFormsApp1\WinFormsApp1\bin\Debug\net8.0-windows\WinFormsApp1.exe");
            options.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            options.AddAdditionalCapability("deviceName", "WindowsPC");

            _driver = new WindowsDriver<WindowsElement>(new Uri(DriverUrl), options);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver?.Quit();
        }

        [TestMethod]
        public void TestBasicCalculation()
        {
            SetInputs(days: 3, category: 1, capacity: 1);
            ClickCalculate();
            Assert.AreEqual("4 500,00 ₽", GetTotal());
        }

        [TestMethod]
        public void TestWithSafe()
        {
            SetInputs(days: 2, category: 2, capacity: 1);
            _driver.FindElementByAccessibilityId("checkBoxSafe").Click();
            ClickCalculate();
            Assert.AreEqual("5 400,00 ₽", GetTotal()); 
        }

        [TestMethod]
        public void TestWithBreakfast()
        {
            SetInputs(days: 1, category: 3, capacity: 2);
            _driver.FindElementByAccessibilityId("checkBoxBreakfast").Click();
            ClickCalculate();
            Assert.AreEqual("4 300,00 ₽", GetTotal()); 
        }

        [TestMethod]
        public void TestAllServices()
        {
            SetInputs(days: 4, category: 2, capacity: 3);
            _driver.FindElementByAccessibilityId("checkBoxSafe").Click();
            _driver.FindElementByAccessibilityId("checkBoxBreakfast").Click();
            ClickCalculate();
            Assert.AreEqual("16 000,00 ₽", GetTotal());
        }

        [TestMethod]
        public void TestInvalidInput()
        {
            _driver.FindElementByAccessibilityId("textBoxDays").SendKeys("abc");

            ClickCalculate();

            Assert.AreEqual("0,00 ₽", GetTotal());
        }

        private void SetInputs(int days, int category, int capacity)
        {
            var daysInput = _driver.FindElementByAccessibilityId("textBoxDays");
            daysInput.Clear();
            daysInput.SendKeys(days.ToString());

            SelectComboBox("comboBoxCategory", category);
            SelectComboBox("comboBoxCapacity", capacity);
        }

        private void SelectComboBox(string comboBoxId, int value)
        {
            var comboBox = _driver.FindElementByAccessibilityId(comboBoxId);
            comboBox.Click();
            _driver.FindElementByName(value.ToString()).Click();
        }

        private void ClickCalculate()
        {
            _driver.FindElementByAccessibilityId("buttonCalculate").Click();
        }

        private string GetTotal()
        {
            return _driver.FindElementByAccessibilityId("textBoxTotal").Text;
        }
    }
}