using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Diagnostics;

namespace HotelCalculator
{
    [TestClass]
    [DoNotParallelize]
    public class Tests
    {
        private static WindowsDriver<WindowsElement> driver;
        public const string DriveURL = "http://127.0.0.1:4723/";
        private const string AppPath = @"C:\Users\Artem\source\repos\HotelCalculatorForm\bin\Debug\net8.0-windows\HotelCalculatorForm.exe";

        [TestInitialize]
        public void Setup()
        {
            try
            {
                var appCapabilities = new AppiumOptions();
                appCapabilities.AddAdditionalCapability("app", AppPath);
                appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");
                appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

                driver = new WindowsDriver<WindowsElement>(new Uri(DriveURL), appCapabilities);
                if (driver == null)
                {
                    Assert.Fail("Не удалось инициализировать WindowsDriver.");
                }

                Thread.Sleep(500);
            }
            catch (Exception ex) {
                Assert.Fail($"Ошибка при инициализации WindowsDriver: {ex.Message}");
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            try
            {
                driver?.Quit();
            }
            finally
            {
                foreach (var process in Process.GetProcessesByName("HotelCalculatorForm"))
                {
                    process.Kill();
                    process.WaitForExit();
                }
            }
        }

        [TestMethod]
        public void Test_Luxury_2People_2Day_NoExtras()
        {
            try
            {
                var txtDays = driver.FindElementByAccessibilityId("txtDays");
                txtDays.Clear();
                txtDays.SendKeys("2");

                var cmbCategory = driver.FindElementByAccessibilityId("cmbCategory");
                cmbCategory.Click();
                Thread.Sleep(500);
                cmbCategory.SendKeys(OpenQA.Selenium.Keys.Enter);

                var cmbPeople = driver.FindElementByAccessibilityId("cmbPeople");
                cmbPeople.Click();
                Thread.Sleep(500);
                cmbPeople.SendKeys(OpenQA.Selenium.Keys.Down);
                cmbPeople.SendKeys(OpenQA.Selenium.Keys.Enter);

                var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
                btnCalculate.Click();

                var result = driver.FindElementByAccessibilityId("txtResult").Text;
                Assert.AreEqual("34000", result);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Ошибка в тесте: {ex.Message}");
            }
        }

        [TestMethod]
        public void Test_Luxury_1Person_1Day_NoExtras()
        {
            var txtDays = driver.FindElementByAccessibilityId("txtDays");
            txtDays.Clear();
            txtDays.SendKeys("1");

            var cmbCategory = driver.FindElementByAccessibilityId("cmbCategory");
            cmbCategory.Click();
            Thread.Sleep(500);
            cmbCategory.SendKeys(OpenQA.Selenium.Keys.Enter);

            var cmbPeople = driver.FindElementByAccessibilityId("cmbPeople");
            cmbPeople.Click();
            Thread.Sleep(500);
            cmbPeople.SendKeys(OpenQA.Selenium.Keys.Enter);

            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            btnCalculate.Click();

            var result = driver.FindElementByAccessibilityId("txtResult").Text;
            Assert.AreEqual("8500", result);
        }

        [TestMethod]
        public void Test_Standard_2People_2Days_NoExtras()
        {
            var txtDays = driver.FindElementByAccessibilityId("txtDays");
            txtDays.Clear();
            txtDays.SendKeys("2");

            var cmbCategory = driver.FindElementByAccessibilityId("cmbCategory");
            cmbCategory.Click();
            Thread.Sleep(500);
            cmbCategory.SendKeys(OpenQA.Selenium.Keys.Down);
            cmbCategory.SendKeys(OpenQA.Selenium.Keys.Enter);

            var cmbPeople = driver.FindElementByAccessibilityId("cmbPeople");
            cmbPeople.Click();
            Thread.Sleep(500);
            cmbPeople.SendKeys(OpenQA.Selenium.Keys.Down);
            cmbPeople.SendKeys(OpenQA.Selenium.Keys.Enter);

            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            btnCalculate.Click();

            var result = driver.FindElementByAccessibilityId("txtResult").Text;
            Assert.AreEqual("19600", result);
        }

        [TestMethod]
        public void Test_Econom_3People_3Days_NoExtras()
        {
            var txtDays = driver.FindElementByAccessibilityId("txtDays");
            txtDays.Clear();
            txtDays.SendKeys("3");

            var cmbCategory = driver.FindElementByAccessibilityId("cmbCategory");
            cmbCategory.Click();
            Thread.Sleep(500);
            cmbCategory.SendKeys(OpenQA.Selenium.Keys.Down);
            cmbCategory.SendKeys(OpenQA.Selenium.Keys.Down);
            cmbCategory.SendKeys(OpenQA.Selenium.Keys.Enter);

            var cmbPeople = driver.FindElementByAccessibilityId("cmbPeople");
            cmbPeople.Click();
            Thread.Sleep(500);
            cmbPeople.SendKeys(OpenQA.Selenium.Keys.Down);
            cmbPeople.SendKeys(OpenQA.Selenium.Keys.Down);
            cmbPeople.SendKeys(OpenQA.Selenium.Keys.Enter);

            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            btnCalculate.Click();

            var result = driver.FindElementByAccessibilityId("txtResult").Text;
            Assert.AreEqual("18000", result);
        }

        [TestMethod]
        public void Test_Luxury_1Person_1Day_WithSafebox()
        {
            var txtDays = driver.FindElementByAccessibilityId("txtDays");
            txtDays.Clear();
            txtDays.SendKeys("1");

            var cmbCategory = driver.FindElementByAccessibilityId("cmbCategory");
            cmbCategory.Click();
            Thread.Sleep(500);
            cmbCategory.SendKeys(OpenQA.Selenium.Keys.Enter);

            var cmbPeople = driver.FindElementByAccessibilityId("cmbPeople");
            cmbPeople.Click();
            Thread.Sleep(500);
            cmbPeople.SendKeys(OpenQA.Selenium.Keys.Enter);

            var safeBox = driver.FindElementByAccessibilityId("chkSafebox");
            safeBox.Click();

            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            btnCalculate.Click();
            
            var result = driver.FindElementByAccessibilityId("txtResult").Text;
            Assert.AreEqual("9800", result);
        }

        [TestMethod]
        public void Test_Luxury_1Person_1Day_WithBreakfast()
        {
            var txtDays = driver.FindElementByAccessibilityId("txtDays");
            txtDays.Clear();
            txtDays.SendKeys("1");

            var cmbCategory = driver.FindElementByAccessibilityId("cmbCategory");
            cmbCategory.Click();
            Thread.Sleep(500);
            cmbCategory.SendKeys(OpenQA.Selenium.Keys.Enter);

            var cmbPeople = driver.FindElementByAccessibilityId("cmbPeople");
            cmbPeople.Click();
            Thread.Sleep(500);
            cmbPeople.SendKeys(OpenQA.Selenium.Keys.Enter);

            var breakfast = driver.FindElementByAccessibilityId("chkBreakfast");
            breakfast.Click();

            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            btnCalculate.Click();

            var result = driver.FindElementByAccessibilityId("txtResult").Text;
            Assert.AreEqual("9100", result);
        }

        [TestMethod]
        public void Test_Luxury_1Person_1Day_WithBothExtras()
        {
            var txtDays = driver.FindElementByAccessibilityId("txtDays");
            txtDays.Clear();
            txtDays.SendKeys("1");

            var cmbCategory = driver.FindElementByAccessibilityId("cmbCategory");
            cmbCategory.Click();
            Thread.Sleep(500);
            cmbCategory.SendKeys(OpenQA.Selenium.Keys.Enter);

            var cmbPeople = driver.FindElementByAccessibilityId("cmbPeople");
            cmbPeople.Click();
            Thread.Sleep(500);
            cmbPeople.SendKeys(OpenQA.Selenium.Keys.Enter);

            var safeBox = driver.FindElementByAccessibilityId("chkSafebox");
            safeBox.Click();

            var breakfast = driver.FindElementByAccessibilityId("chkBreakfast");
            breakfast.Click();

            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            btnCalculate.Click();

            var result = driver.FindElementByAccessibilityId("txtResult").Text;
            Assert.AreEqual("10400", result);
        }

        [TestMethod]
        public void Test_Standard_2People_2Days_WithSafebox()
        {
            var txtDays = driver.FindElementByAccessibilityId("txtDays");
            txtDays.Clear();
            txtDays.SendKeys("2");

            var cmbCategory = driver.FindElementByAccessibilityId("cmbCategory");
            cmbCategory.Click();
            Thread.Sleep(500);
            cmbCategory.SendKeys(OpenQA.Selenium.Keys.Down);
            cmbCategory.SendKeys(OpenQA.Selenium.Keys.Enter);

            var cmbPeople = driver.FindElementByAccessibilityId("cmbPeople");
            cmbPeople.Click();
            Thread.Sleep(500);
            cmbPeople.SendKeys(OpenQA.Selenium.Keys.Down);
            cmbPeople.SendKeys(OpenQA.Selenium.Keys.Enter);

            var safeBox = driver.FindElementByAccessibilityId("chkSafebox");
            safeBox.Click();

            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            btnCalculate.Click();

            var result = driver.FindElementByAccessibilityId("txtResult").Text;
            Assert.AreEqual("22200", result);
        }

        [TestMethod]
        public void Test_Standard_2People_2Days_WithBreakfast()
        {
            var txtDays = driver.FindElementByAccessibilityId("txtDays");
            txtDays.Clear();
            txtDays.SendKeys("2");

            var cmbCategory = driver.FindElementByAccessibilityId("cmbCategory");
            cmbCategory.Click();
            Thread.Sleep(500);
            cmbCategory.SendKeys(OpenQA.Selenium.Keys.Down);
            cmbCategory.SendKeys(OpenQA.Selenium.Keys.Enter);

            var cmbPeople = driver.FindElementByAccessibilityId("cmbPeople");
            cmbPeople.Click();
            Thread.Sleep(500);
            cmbPeople.SendKeys(OpenQA.Selenium.Keys.Down);
            cmbPeople.SendKeys(OpenQA.Selenium.Keys.Enter);

            var breakfast = driver.FindElementByAccessibilityId("chkBreakfast");
            breakfast.Click();

            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            btnCalculate.Click();

            var result = driver.FindElementByAccessibilityId("txtResult").Text;
            Assert.AreEqual("22000", result);
        }

        [TestMethod]
        public void Test_Econom_3People_3Days_WithSafebox()
        {
            var txtDays = driver.FindElementByAccessibilityId("txtDays");
            txtDays.Clear();
            txtDays.SendKeys("3");

            var cmbCategory = driver.FindElementByAccessibilityId("cmbCategory");
            cmbCategory.Click();
            Thread.Sleep(500);
            cmbCategory.SendKeys(OpenQA.Selenium.Keys.Down);
            cmbCategory.SendKeys(OpenQA.Selenium.Keys.Down);
            cmbCategory.SendKeys(OpenQA.Selenium.Keys.Enter);

            var cmbPeople = driver.FindElementByAccessibilityId("cmbPeople");
            cmbPeople.Click();
            Thread.Sleep(500);
            cmbPeople.SendKeys(OpenQA.Selenium.Keys.Down);
            cmbPeople.SendKeys(OpenQA.Selenium.Keys.Down);
            cmbPeople.SendKeys(OpenQA.Selenium.Keys.Enter);

            var safeBox = driver.FindElementByAccessibilityId("chkSafebox");
            safeBox.Click();

            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            btnCalculate.Click();

            var result = driver.FindElementByAccessibilityId("txtResult").Text;
            Assert.AreEqual("21900", result);
        }

        [TestMethod]
        public void Test_Econom_3People_3Days_WithBothExtras()
        {
            var txtDays = driver.FindElementByAccessibilityId("txtDays");
            txtDays.Clear();
            txtDays.SendKeys("3");

            var cmbCategory = driver.FindElementByAccessibilityId("cmbCategory");
            cmbCategory.Click();
            Thread.Sleep(500);
            cmbCategory.SendKeys(OpenQA.Selenium.Keys.Down);
            cmbCategory.SendKeys(OpenQA.Selenium.Keys.Down);
            cmbCategory.SendKeys(OpenQA.Selenium.Keys.Enter);

            var cmbPeople = driver.FindElementByAccessibilityId("cmbPeople");
            cmbPeople.Click();
            Thread.Sleep(500);
            cmbPeople.SendKeys(OpenQA.Selenium.Keys.Down);
            cmbPeople.SendKeys(OpenQA.Selenium.Keys.Down);
            cmbPeople.SendKeys(OpenQA.Selenium.Keys.Enter);

            var safeBox = driver.FindElementByAccessibilityId("chkSafebox");
            safeBox.Click();

            var breakfast = driver.FindElementByAccessibilityId("chkBreakfast");
            breakfast.Click();

            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            btnCalculate.Click();

            var result = driver.FindElementByAccessibilityId("txtResult").Text;
            Assert.AreEqual("27300", result);
        }
    }
}