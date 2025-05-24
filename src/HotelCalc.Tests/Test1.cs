using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace HotelCalc.Tests
{
    [TestClass]
    [DoNotParallelize]
    public sealed class Test1
    {
        public const string DriveURL = "http://127.0.0.1:4723/";
        private WindowsDriver<WindowsElement> driver;

        [TestInitialize]
        public void Setup()
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", @"C:\Users\rerar\source\repos\ST-9\src\HotelCalc\bin\Debug\HotelCalc.exe");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

            driver = new WindowsDriver<WindowsElement>(new Uri(DriveURL), appCapabilities);
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Quit();
        }

        private void FillForm(string days, string category, string capacity, string safe, string breakfast)
        {
            var daysBox = driver.FindElementByAccessibilityId("daysTextBox");
            daysBox.Clear();
            daysBox.SendKeys(days);

            var categoryBox = driver.FindElementByAccessibilityId("categoryTextBox");
            categoryBox.Clear();
            categoryBox.SendKeys(category);

            var capacityBox = driver.FindElementByAccessibilityId("capacityTextBox");
            capacityBox.Clear();
            capacityBox.SendKeys(capacity);

            var safeBox = driver.FindElementByAccessibilityId("safeTextBox");
            safeBox.Clear();
            safeBox.SendKeys(safe);

            var breakfastBox = driver.FindElementByAccessibilityId("breakfastTextBox");
            breakfastBox.Clear();
            breakfastBox.SendKeys(breakfast);

            driver.FindElementByAccessibilityId("calculateButton").Click();
        }


        private void AssertResult(string expected)
        {
            var result = driver.FindElementByAccessibilityId("resultTextBox").Text;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test1_ОдниСутки_Базовый()
        {
            FillForm("1", "1", "1", "нет", "нет");
            AssertResult("1500");
        }

        [TestMethod]
        public void Test2_СейфИЗавтрак()
        {
            FillForm("1", "1", "1", "да", "да");
            AssertResult("2000");
        }

        [TestMethod]
        public void Test3_МаксимальнаяКатегорияИВместимость()
        {
            FillForm("2", "3", "3", "да", "да");
            AssertResult("10000");
        }

        [TestMethod]
        public void Test4_СредниеЗначения()
        {
            FillForm("3", "2", "2", "да", "нет");
            AssertResult("9600");
        }

        [TestMethod]
        public void Test5_НольДней()
        {
            FillForm("0", "2", "2", "да", "да");
            AssertResult("0");
        }
    }
}
