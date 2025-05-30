using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        public const string DriveURL = "http://127.0.0.1:4723/";
        public WindowsDriver<WindowsElement> driver;

        [TestInitialize]
        public void Setup()
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", 
                @"F:\Programming\WinFormsApp1\WinFormsApp1\bin\Debug\net8.0-windows\WinFormsApp1.exe");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

            driver = new WindowsDriver<WindowsElement>(new Uri(DriveURL), appCapabilities);
            Assert.IsNotNull(driver);
            Assert.IsNotNull(driver.SessionId);
        }

        [TestCleanup]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }

        private void PerformCalculation(string days, string category, string capacity, string safe, string breakfast)
        {
            var tbDays = driver.FindElementByAccessibilityId("textBox1");
            tbDays.Clear();
            tbDays.SendKeys(days);

            var tbCategory = driver.FindElementByAccessibilityId("textBox2");
            tbCategory.Clear();
            tbCategory.SendKeys(category);

            var tbCapacity = driver.FindElementByAccessibilityId("textBox3");
            tbCapacity.Clear();
            tbCapacity.SendKeys(capacity);

            var tbSafe = driver.FindElementByAccessibilityId("textBox4");
            tbSafe.Clear();
            tbSafe.SendKeys(safe);

            var tbBreakfast = driver.FindElementByAccessibilityId("textBox5");
            tbBreakfast.Clear();
            tbBreakfast.SendKeys(breakfast);

            var btnCalculate = driver.FindElementByAccessibilityId("button1");
            btnCalculate.Click();
        }

        [TestMethod]
        public void TestScenario_Basic() // Test 1
        {
            PerformCalculation("1", "1", "1", "нет", "нет");
            var tbSum = driver.FindElementByAccessibilityId("textBox6");
            Assert.AreEqual("1000.00", tbSum.Text);
        }

        [TestMethod]
        public void TestScenario_Category2() // Test 2
        {
            PerformCalculation("2", "2", "1", "нет", "нет");
            var tbSum = driver.FindElementByAccessibilityId("textBox6");
            Assert.AreEqual("2400.00", tbSum.Text);
        }

        [TestMethod]
        public void TestScenario_Category3_WithSafe() // Test 3
        {
            PerformCalculation("3", "3", "1", "да", "нет");
            var tbSum = driver.FindElementByAccessibilityId("textBox6");
            Assert.AreEqual("4800.00", tbSum.Text);
        }

        [TestMethod]
        public void TestScenario_Capacity2() // Test 4
        {
            PerformCalculation("1", "1", "2", "нет", "нет");
            var tbSum = driver.FindElementByAccessibilityId("textBox6");
            Assert.AreEqual("1200.00", tbSum.Text);
        }

        [TestMethod]
        public void TestScenario_WithBreakfast() // Test 5
        {
            PerformCalculation("1", "1", "1", "нет", "да");
            var tbSum = driver.FindElementByAccessibilityId("textBox6");
            Assert.AreEqual("1150.00", tbSum.Text);
        }

        [TestMethod]
        public void TestScenario_AllFeaturesCombined() // Test 6
        {
            PerformCalculation("5", "3", "2", "да", "да");
            var tbSum = driver.FindElementByAccessibilityId("textBox6");
            Assert.AreEqual("10500.00", tbSum.Text);
        }

        [TestMethod]
        public void TestScenario_HighCapacity_WithBreakfast() // Test 7
        {
            PerformCalculation("2", "1", "4", "нет", "да");
            var tbSum = driver.FindElementByAccessibilityId("textBox6");
            Assert.AreEqual("4200.00", tbSum.Text);
        }
    }
}