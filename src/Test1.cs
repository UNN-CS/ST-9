using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace HotelCalculatorTests
{
    [TestClass]
    public class RoomCalculationTests
    {
        private const string SeleniumUrl = "http://127.0.0.1:4723/";
        private const string ExecutablePath = @"C:\Users\venn2713\source\repos\HotelCalculator\bin\Debug\net8.0-windows\HotelCalculator.exe";
        private WindowsDriver<WindowsElement>? session;

        [TestInitialize]
        public void Setup()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", ExecutablePath);
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            options.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            session = new WindowsDriver<WindowsElement>(new Uri(SeleniumUrl), options);
        }

        private void InputValues(string days, string category, string capacity, string safe, string breakfast)
        {
            session!.FindElementByAccessibilityId("tbDays").Clear();
            session.FindElementByAccessibilityId("tbDays").SendKeys(days);
            session.FindElementByAccessibilityId("tbCategory").Clear();
            session.FindElementByAccessibilityId("tbCategory").SendKeys(category);
            session.FindElementByAccessibilityId("tbCapacity").Clear();
            session.FindElementByAccessibilityId("tbCapacity").SendKeys(capacity);
            session.FindElementByAccessibilityId("tbSafe").Clear();
            session.FindElementByAccessibilityId("tbSafe").SendKeys(safe);
            session.FindElementByAccessibilityId("tbBreakfast").Clear();
            session.FindElementByAccessibilityId("tbBreakfast").SendKeys(breakfast);
        }

        private string GetResult()
        {
            session!.FindElementByAccessibilityId("btnCalculate").Click();
            return session.FindElementByAccessibilityId("tbTotal").Text;
        }

        [DataTestMethod]
        [DataRow("3", "2", "2", "да", "да", "9000")]
        [DataRow("2", "1", "1", "нет", "нет", "2000")]
        [DataRow("1", "3", "3", "да", "да", "4500")]
        [DataRow("2", "2", "1", "нет", "да", "4600")]
        [DataRow("2", "3", "2", "нет", "нет", "7000")]
        [DataRow("4", "1", "1", "да", "нет", "4800")]
        public void Calculation_ValidInputs_ReturnsExpected(string days, string category, string capacity, string safe, string breakfast, string expected)
        {
            InputValues(days, category, capacity, safe, breakfast);
            var result = GetResult();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Calculation_InvalidInput_EmptyResult()
        {
            InputValues("abc", "1", "1", "нет", "нет");
            var result = GetResult();
            Assert.AreEqual("", result);
        }

        [TestCleanup]
        public void Teardown()
        {
            session?.Quit();
        }
    }
}
