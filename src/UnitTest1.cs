using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using System;

namespace TestProject2
{
    [TestClass]
    public class UnitTest1
    {
        public const string DriverUrl = "http://127.0.0.1:4723/";
        public WindowsDriver<WindowsElement> Driver;

        [TestInitialize]
        public void Setup()
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", @"C:\Users\thunderbird\Desktop\st9\WinFormsApp1\WinFormsApp1\bin\Debug\net8.0-windows\WinFormsApp1.exe");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

            Driver = new WindowsDriver<WindowsElement>(new Uri(DriverUrl), appCapabilities);
        }

        [TestCleanup]
        public void TearDown()
        {
            if (Driver != null)
            {
                Driver.CloseApp();
                Driver.Quit();
            }
        }

        [TestMethod]
        public void TestEconomyRoomSingleWithoutExtras()
        {
            // Arrange
            Driver.FindElementByAccessibilityId("DaysTB").SendKeys("3");
            Driver.FindElementByAccessibilityId("ClassTB").SendKeys("1");
            Driver.FindElementByAccessibilityId("CapacityTB").SendKeys("1");

            // Act
            Driver.FindElementByAccessibilityId("SolveBtn").Click();

            // Assert
            var result = Driver.FindElementByAccessibilityId("SummaryTB").Text.Replace('\u00A0', ' ');
            Assert.AreEqual("7 500,00 ₽", result); // 2500 * 3 * 1
        }

        [TestMethod]
        public void TestStandardRoomDoubleWithBreakfast()
        {
            // Arrange
            Driver.FindElementByAccessibilityId("DaysTB").SendKeys("5");
            Driver.FindElementByAccessibilityId("ClassTB").SendKeys("2");
            Driver.FindElementByAccessibilityId("CapacityTB").SendKeys("2");
            Driver.FindElementByAccessibilityId("BreakfastCB").Click();

            // Act
            Driver.FindElementByAccessibilityId("SolveBtn").Click();

            // Assert
            var result = Driver.FindElementByAccessibilityId("SummaryTB").Text.Replace('\u00A0', ' ');
            Assert.AreEqual("58 000,00 ₽", result); // (5000 * 5 * 2) + (800 * 5 * 2)
        }

        [TestMethod]
        public void TestLuxuryRoomTripleWithAllExtras()
        {
            // Arrange
            Driver.FindElementByAccessibilityId("DaysTB").SendKeys("2");
            Driver.FindElementByAccessibilityId("ClassTB").SendKeys("3");
            Driver.FindElementByAccessibilityId("CapacityTB").SendKeys("3");
            Driver.FindElementByAccessibilityId("SafeCB").Click();
            Driver.FindElementByAccessibilityId("BreakfastCB").Click();

            // Act
            Driver.FindElementByAccessibilityId("SolveBtn").Click();

            // Assert
            var result = Driver.FindElementByAccessibilityId("SummaryTB").Text.Replace('\u00A0', ' ');
            Assert.AreEqual("65 300,00 ₽".Replace(" ", ""), result.Replace(" ", "").Replace(" ", "")); // (10000 * 2 * 3) + 500 + (800 * 2 * 3)
        }

        [TestMethod]
        public void TestInvalidDaysInput()
        {
            // Arrange
            Driver.FindElementByAccessibilityId("DaysTB").SendKeys("0");
            Driver.FindElementByAccessibilityId("ClassTB").SendKeys("2");
            Driver.FindElementByAccessibilityId("CapacityTB").SendKeys("1");

            // Act
            Driver.FindElementByAccessibilityId("SolveBtn").Click();

            // Assert
            var messageBox = Driver.FindElementByName("Ошибка");
            Assert.IsNotNull(messageBox);
        }

        [TestMethod]
        public void TestInvalidRoomClass()
        {
            // Arrange
            Driver.FindElementByAccessibilityId("DaysTB").SendKeys("1");
            Driver.FindElementByAccessibilityId("ClassTB").SendKeys("4");
            Driver.FindElementByAccessibilityId("CapacityTB").SendKeys("1");

            // Act
            Driver.FindElementByAccessibilityId("SolveBtn").Click();

            // Assert
            var messageBox = Driver.FindElementByName("Ошибка");
            Assert.IsNotNull(messageBox);
        }
    }
}