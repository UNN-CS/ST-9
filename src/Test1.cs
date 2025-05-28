using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using System;
using System.Threading;

namespace HotelCalculatorTests
{
    [TestClass]
    public class HotelCalculatorTests
    {
        public const string DriverUrl = "http://127.0.0.1:4723/";
        public WindowsDriver<WindowsElement> driver;

        [TestInitialize]
        public void Setup()
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", @"C:\Users\ivank\Desktop\testing\ST-9\WinFormsApp1\WinFormsApp1\bin\Debug\net7.0-windows\WinFormsApp1.exe");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

            driver = new WindowsDriver<WindowsElement>(new Uri(DriverUrl), appCapabilities);
            Thread.Sleep(1500);
        }

        [TestCleanup]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }

        [TestMethod]
        public void TestEconomySingleRoomNoExtras()
        {
            driver.FindElementByAccessibilityId("txtDays").SendKeys("2");
            driver.FindElementByAccessibilityId("txtCategory").SendKeys("1");
            driver.FindElementByAccessibilityId("txtCapacity").SendKeys("1");
            driver.FindElementByAccessibilityId("txtSafe").SendKeys("нет");
            driver.FindElementByAccessibilityId("txtBreakfast").SendKeys("нет");

            driver.FindElementByAccessibilityId("btnCalculate").Click();
            Thread.Sleep(500); 

            var resultText = driver.FindElementByAccessibilityId("lblSum").Text;
            var actualNumber = decimal.Parse(resultText.Replace("—умма:", "").Replace("руб.", "").Trim());
            var expectedNumber = 2000.00m;
            Assert.AreEqual(expectedNumber, actualNumber);
        }
        [TestMethod]
        public void TestLuxuryTripleRoomWithAllExtras()
        {
            driver.FindElementByAccessibilityId("txtDays").SendKeys("3");
            driver.FindElementByAccessibilityId("txtCategory").SendKeys("3"); 
            driver.FindElementByAccessibilityId("txtCapacity").SendKeys("3");
            driver.FindElementByAccessibilityId("txtSafe").SendKeys("да");
            driver.FindElementByAccessibilityId("txtBreakfast").SendKeys("да");

            driver.FindElementByAccessibilityId("btnCalculate").Click();
            Thread.Sleep(500);

            var resultText = driver.FindElementByAccessibilityId("lblSum").Text;
            var actualNumber = decimal.Parse(resultText.Replace("—умма:", "").Replace("руб.", "").Trim());

            decimal basePrice = 5000;
            decimal discount = 0.8m;
            int days = 3;
            decimal expected = basePrice * discount * days + 200 * days + 300 * days; 
            Assert.AreEqual(expected, actualNumber);
        }

        [TestMethod]
        public void TestStandardDoubleRoomWithSafeOnly()
        {
            driver.FindElementByAccessibilityId("txtDays").SendKeys("5");
            driver.FindElementByAccessibilityId("txtCategory").SendKeys("2"); 
            driver.FindElementByAccessibilityId("txtCapacity").SendKeys("2"); 
            driver.FindElementByAccessibilityId("txtSafe").SendKeys("да");
            driver.FindElementByAccessibilityId("txtBreakfast").SendKeys("нет");

            driver.FindElementByAccessibilityId("btnCalculate").Click();
            Thread.Sleep(500);

            var resultText = driver.FindElementByAccessibilityId("lblSum").Text;
            var actualNumber = decimal.Parse(resultText.Replace("—умма:", "").Replace("руб.", "").Trim());

            decimal basePrice = 2000;
            decimal discount = 0.9m;
            int days = 5;
            decimal expected = basePrice * discount * days + 200 * days;
            Assert.AreEqual(expected, actualNumber);
        }

        [TestMethod]
        public void TestInvalidDaysInput()
        {
            driver.FindElementByAccessibilityId("txtDays").SendKeys("abc");
            driver.FindElementByAccessibilityId("txtCategory").SendKeys("1");
            driver.FindElementByAccessibilityId("txtCapacity").SendKeys("1");
            driver.FindElementByAccessibilityId("txtSafe").SendKeys("нет");
            driver.FindElementByAccessibilityId("txtBreakfast").SendKeys("да");

            driver.FindElementByAccessibilityId("btnCalculate").Click();
            Thread.Sleep(500);

            var resultText = driver.FindElementByAccessibilityId("lblSum").Text;
            Assert.AreEqual("—умма: 0 руб.", resultText); 
        }

        [TestMethod]
        public void TestEconomyDoubleRoomWithBreakfastOnly()
        {
            driver.FindElementByAccessibilityId("txtDays").SendKeys("4");
            driver.FindElementByAccessibilityId("txtCategory").SendKeys("1");
            driver.FindElementByAccessibilityId("txtCapacity").SendKeys("2"); 
            driver.FindElementByAccessibilityId("txtSafe").SendKeys("нет");
            driver.FindElementByAccessibilityId("txtBreakfast").SendKeys("да");

            driver.FindElementByAccessibilityId("btnCalculate").Click();
            Thread.Sleep(500);

            var resultText = driver.FindElementByAccessibilityId("lblSum").Text;
            var actualNumber = decimal.Parse(resultText.Replace("—умма:", "").Replace("руб.", "").Trim());

            decimal basePrice = 1000;
            decimal discount = 0.9m;
            int days = 4;
            decimal expected = basePrice * discount * days + 300 * days; 
            Assert.AreEqual(expected, actualNumber);
        }
    }
}
