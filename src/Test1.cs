using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

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
            appCapabilities.AddAdditionalCapability("app", @"""C:\Users\User\source\repos\WinFormsApp1\WinFormsApp1\bin\Debug\net8.0-windows\WinFormsApp1.exe""");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

            driver = new WindowsDriver<WindowsElement>(new Uri(DriveURL), appCapabilities);
        }

        [TestCleanup]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }

        private void CalculatePrice(string days, string category, string capacity, string safe, string breakfast, string expected)
        {
            driver.FindElementByAccessibilityId("textBox1").SendKeys(days);
            driver.FindElementByAccessibilityId("textBox2").SendKeys(category);
            driver.FindElementByAccessibilityId("textBox3").SendKeys(capacity);
            driver.FindElementByAccessibilityId("textBox4").SendKeys(safe);
            driver.FindElementByAccessibilityId("textBox5").SendKeys(breakfast);

            driver.FindElementByAccessibilityId("button1").Click();

            var result = driver.FindElementByAccessibilityId("textBox6").Text;
            Assert.IsTrue(result.Contains(expected), $"Expected: {expected}, Actual: {result}");
        }

        [TestMethod]
        public void Test1_Category1_Single_NoOptions()
        {
            CalculatePrice("2", "1", "1", "нет", "нет", "10000,00 руб.");
        }

        [TestMethod]
        public void Test2_Category1_Double_WithBreakfast()
        {
            CalculatePrice("3", "1", "2", "нет", "да", "20400,00 руб.");
        }

        [TestMethod]
        public void Test3_Category1_Triple_WithSafe()
        {
            CalculatePrice("1", "1", "3", "да", "нет", "7700,00 руб.");
        }

        [TestMethod]
        public void Test4_Category2_Single_WithBothOptions()
        {
            CalculatePrice("5", "2", "1", "да", "да", "16700,00 руб.");
        }

        [TestMethod]
        public void Test5_Category2_Double_NoOptions()
        {
            CalculatePrice("2", "2", "2", "нет", "нет", "7800,00 руб.");
        }

        [TestMethod]
        public void Test6_Category2_Triple_WithBreakfast()
        {
            CalculatePrice("4", "2", "3", "нет", "да", "19200,00 руб.");
        }

        [TestMethod]
        public void Test7_Category3_Single_WithSafe()
        {
            CalculatePrice("1", "3", "1", "да", "нет", "1700,00 руб.");
        }

        [TestMethod]
        public void Test8_Category3_Double_WithBothOptions()
        {
            CalculatePrice("3", "3", "2", "да", "да", "6950,00 руб.");
        }

        [TestMethod]
        public void Test9_Category3_Triple_NoOptions()
        {
            CalculatePrice("2", "3", "3", "нет", "нет", "4500,00 руб.");
        }




    }
}