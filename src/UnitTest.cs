using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using System;

namespace HotelTests
{
    [TestClass]
    public class CalculatorTests
    {
        public const string AppiumUrl = "http://127.0.0.1:4723/";
        public WindowsDriver<WindowsElement> driver;

        private void FillInput(string id, string value)
        {
            var element = driver.FindElementByAccessibilityId(id);
            element.Clear();
            element.SendKeys(value);
        }

        private void ToggleOption(string id)
        {
            driver.FindElementByAccessibilityId(id).Click();
        }

        [TestInitialize]
        public void InitializeApp()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", @"D:\Projects\HotelCostCalculator\HotelCostCalculator\bin\Debug\net8.0-windows\HotelCostCalculator.exe");
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            options.AddAdditionalCapability("ms:waitForAppLaunch", "10");

            driver = new WindowsDriver<WindowsElement>(new Uri(AppiumUrl), options);
        }

        [TestCleanup]
        public void CloseApp()
        {
            driver?.Dispose();
        }

        [TestMethod]
        public void EconomyRoomOneGuestNoExtras()
        {
            FillInput("DaysTextBox", "3");
            FillInput("RoomTypeBox", "эконом");
            FillInput("GuestsTextBox", "1");

            driver.FindElementByAccessibilityId("CalculateButton").Click();

            string result = driver.FindElementByAccessibilityId("ResultLabel").Text.Replace('\u00A0', ' ').Trim();
            Assert.AreEqual("7 500,00 ₽", result);
        }

        [TestMethod]
        public void StandardRoomTwoWithMeal()
        {
            FillInput("DaysTextBox", "5");
            FillInput("RoomTypeBox", "стандарт");
            FillInput("GuestsTextBox", "2");
            ToggleOption("IncludeMealsCheckbox");

            driver.FindElementByAccessibilityId("CalculateButton").Click();

            string result = driver.FindElementByAccessibilityId("ResultLabel").Text.Replace('\u00A0', ' ').Trim();
            Assert.AreEqual("58 000,00 ₽", result);
        }

        [TestMethod]
        public void LuxuryThreeAllExtras()
        {
            FillInput("DaysTextBox", "2");
            FillInput("RoomTypeBox", "люкс");
            FillInput("GuestsTextBox", "3");
            ToggleOption("UseSafeCheckbox");
            ToggleOption("IncludeMealsCheckbox");

            driver.FindElementByAccessibilityId("CalculateButton").Click();

            string result = driver.FindElementByAccessibilityId("ResultLabel").Text.Replace('\u00A0', ' ').Replace(" ", "").Trim();
            Assert.AreEqual("65300,00 ₽", result);
        }

        [TestMethod]
        public void ZeroDays_ShowErrorMessage()
        {
            FillInput("DaysTextBox", "0");
            FillInput("RoomTypeBox", "стандарт");
            FillInput("GuestsTextBox", "1");

            driver.FindElementByAccessibilityId("CalculateButton").Click();

            Assert.IsNotNull(driver.FindElementByName("Ошибка"));
        }

        [TestMethod]
        public void InvalidRoomType_TriggerError()
        {
            FillInput("DaysTextBox", "1");
            FillInput("RoomTypeBox", "премиум");
            FillInput("GuestsTextBox", "1");

            driver.FindElementByAccessibilityId("CalculateButton").Click();

            Assert.IsNotNull(driver.FindElementByName("Ошибка"));
        }
    }
}

[TestMethod]
public void NegativeDaysInput_ShowsError()
{
    FillInput("DaysTextBox", "-2");
    FillInput("RoomTypeBox", "люкс");
    FillInput("GuestsTextBox", "2");

    driver.FindElementByAccessibilityId("CalculateButton").Click();

    Assert.IsNotNull(driver.FindElementByName("Ошибка"));
}

[TestMethod]
public void NonNumericInput_TriggerError()
{
    FillInput("DaysTextBox", "пять");
    FillInput("RoomTypeBox", "стандарт");
    FillInput("GuestsTextBox", "один");

    driver.FindElementByAccessibilityId("CalculateButton").Click();

    Assert.IsNotNull(driver.FindElementByName("Ошибка"));
}

