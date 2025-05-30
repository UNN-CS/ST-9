using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;

namespace Tests
{
    [TestClass]
    public class Tests
    {
        public const string DriveURL = "http://127.0.0.1:4723/";
        public WindowsDriver<WindowsElement> driver;

        [TestInitialize]
        public void Setup()
        {
            string appPath = @"C:\Users\ekorn\ST-9\HotelCalculator\HotelCalculator\bin\Debug\net9.0-windows\HotelCalculator.exe";
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", appPath);
            appCapabilities.AddAdditionalCapability("platformName", "Windows");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

            driver = new WindowsDriver<WindowsElement>(new Uri(DriveURL), appCapabilities);
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

        [TestMethod]
        public void Test_Luxury_1Person_3Days_NoExtras()
        {
            try
            {
                var tbDays = driver.FindElementByAccessibilityId("tbDays");
                tbDays.Clear();
                tbDays.SendKeys("3");

                var tbCategory = driver.FindElementByAccessibilityId("tbCategory");
                tbCategory.Clear();
                tbCategory.SendKeys("1");

                var tbCapacity = driver.FindElementByAccessibilityId("tbCapacity");
                tbCapacity.Clear();
                tbCapacity.SendKeys("1");

                var tbSafe = driver.FindElementByAccessibilityId("tbSafe");
                tbSafe.Clear();
                tbSafe.SendKeys("нет");

                var tbBreakfast = driver.FindElementByAccessibilityId("tbBreakfast");
                tbBreakfast.Clear();
                tbBreakfast.SendKeys("нет");

                var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
                btnCalculate.Click();

                var tbResult = driver.FindElementByAccessibilityId("tbResult");
                var result = tbResult.Text;
                Assert.AreEqual("24000", result);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Ошибка в тесте: {ex.Message}");
            }
        }

        [TestMethod]
        public void Test_Standard_2People_2Days_WithSafe()
        {
            try
            {
                var tbDays = driver.FindElementByAccessibilityId("tbDays");
                tbDays.Clear();
                tbDays.SendKeys("2");

                var tbCategory = driver.FindElementByAccessibilityId("tbCategory");
                tbCategory.Clear();
                tbCategory.SendKeys("2");

                var tbCapacity = driver.FindElementByAccessibilityId("tbCapacity");
                tbCapacity.Clear();
                tbCapacity.SendKeys("2");

                var tbSafe = driver.FindElementByAccessibilityId("tbSafe");
                tbSafe.Clear();
                tbSafe.SendKeys("да");

                var tbBreakfast = driver.FindElementByAccessibilityId("tbBreakfast");
                tbBreakfast.Clear();
                tbBreakfast.SendKeys("нет");

                var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
                btnCalculate.Click();

                var tbResult = driver.FindElementByAccessibilityId("tbResult");
                var result = tbResult.Text;
                Assert.AreEqual("9800", result);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Ошибка в тесте: {ex.Message}");
            }
        }

        [TestMethod]
        public void Test_Economy_3People_1Day_AllExtras()
        {
            try
            {
                var tbDays = driver.FindElementByAccessibilityId("tbDays");
                tbDays.Clear();
                tbDays.SendKeys("1");

                var tbCategory = driver.FindElementByAccessibilityId("tbCategory");
                tbCategory.Clear();
                tbCategory.SendKeys("3");

                var tbCapacity = driver.FindElementByAccessibilityId("tbCapacity");
                tbCapacity.Clear();
                tbCapacity.SendKeys("3");

                var tbSafe = driver.FindElementByAccessibilityId("tbSafe");
                tbSafe.Clear();
                tbSafe.SendKeys("да");

                var tbBreakfast = driver.FindElementByAccessibilityId("tbBreakfast");
                tbBreakfast.Clear();
                tbBreakfast.SendKeys("да");

                var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
                btnCalculate.Click();

                var tbResult = driver.FindElementByAccessibilityId("tbResult");
                var result = tbResult.Text;
                Assert.AreEqual("3600", result);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Ошибка в тесте: {ex.Message}");
            }
        }

        [TestMethod]
        public void Test_InvalidCategory()
        {
            try
            {
                var tbDays = driver.FindElementByAccessibilityId("tbDays");
                tbDays.Clear();
                tbDays.SendKeys("2");

                var tbCategory = driver.FindElementByAccessibilityId("tbCategory");
                tbCategory.Clear();
                tbCategory.SendKeys("5");

                var tbCapacity = driver.FindElementByAccessibilityId("tbCapacity");
                tbCapacity.Clear();
                tbCapacity.SendKeys("2");

                var tbSafe = driver.FindElementByAccessibilityId("tbSafe");
                tbSafe.Clear();
                tbSafe.SendKeys("да");

                var tbBreakfast = driver.FindElementByAccessibilityId("tbBreakfast");
                tbBreakfast.Clear();
                tbBreakfast.SendKeys("да");

                var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
                btnCalculate.Click();

                var tbResult = driver.FindElementByAccessibilityId("tbResult");
                var result = tbResult.Text;
                Assert.IsTrue(string.IsNullOrWhiteSpace(result) || result == "0", "Результат должен быть пустым или 0 при неверной категории.");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Ошибка в тесте: {ex.Message}");
            }
        }

        [TestMethod]
        public void Test_InvalidCapacity()
        {
            try
            {
                var tbDays = driver.FindElementByAccessibilityId("tbDays");
                tbDays.Clear();
                tbDays.SendKeys("1");

                var tbCategory = driver.FindElementByAccessibilityId("tbCategory");
                tbCategory.Clear();
                tbCategory.SendKeys("1");

                var tbCapacity = driver.FindElementByAccessibilityId("tbCapacity");
                tbCapacity.Clear();
                tbCapacity.SendKeys("5");

                var tbSafe = driver.FindElementByAccessibilityId("tbSafe");
                tbSafe.Clear();
                tbSafe.SendKeys("да");

                var tbBreakfast = driver.FindElementByAccessibilityId("tbBreakfast");
                tbBreakfast.Clear();
                tbBreakfast.SendKeys("нет");

                var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
                btnCalculate.Click();

                var tbResult = driver.FindElementByAccessibilityId("tbResult");
                var result = tbResult.Text;
                Assert.IsTrue(string.IsNullOrWhiteSpace(result) || result == "0", "Результат должен быть пустым или 0 при неверной вместимости.");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Ошибка в тесте: {ex.Message}");
            }
        }

        [TestMethod]
        public void Test_InvalidDays()
        {
            try
            {
                var tbDays = driver.FindElementByAccessibilityId("tbDays");
                tbDays.Clear();
                tbDays.SendKeys("два");

                var tbCategory = driver.FindElementByAccessibilityId("tbCategory");
                tbCategory.Clear();
                tbCategory.SendKeys("1");

                var tbCapacity = driver.FindElementByAccessibilityId("tbCapacity");
                tbCapacity.Clear();
                tbCapacity.SendKeys("1");

                var tbSafe = driver.FindElementByAccessibilityId("tbSafe");
                tbSafe.Clear();
                tbSafe.SendKeys("нет");

                var tbBreakfast = driver.FindElementByAccessibilityId("tbBreakfast");
                tbBreakfast.Clear();
                tbBreakfast.SendKeys("нет");

                var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
                btnCalculate.Click();

                var tbResult = driver.FindElementByAccessibilityId("tbResult");
                var result = tbResult.Text;
                Assert.IsTrue(string.IsNullOrWhiteSpace(result) || result == "0", "Результат должен быть пустым или 0 при некорректном числе дней.");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Ошибка в тесте: {ex.Message}");
            }
        }

        [TestMethod]
        public void Test_Economy_1Person_2Days_BreakfastOnly()
        {
            try
            {
                var tbDays = driver.FindElementByAccessibilityId("tbDays");
                tbDays.Clear();
                tbDays.SendKeys("2");

                var tbCategory = driver.FindElementByAccessibilityId("tbCategory");
                tbCategory.Clear();
                tbCategory.SendKeys("3");

                var tbCapacity = driver.FindElementByAccessibilityId("tbCapacity");
                tbCapacity.Clear();
                tbCapacity.SendKeys("1");

                var tbSafe = driver.FindElementByAccessibilityId("tbSafe");
                tbSafe.Clear();
                tbSafe.SendKeys("нет");

                var tbBreakfast = driver.FindElementByAccessibilityId("tbBreakfast");
                tbBreakfast.Clear();
                tbBreakfast.SendKeys("да");

                var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
                btnCalculate.Click();

                var tbResult = driver.FindElementByAccessibilityId("tbResult");
                var result = tbResult.Text;
                Assert.AreEqual("4400", result);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Ошибка в тесте: {ex.Message}");
            }
        }

        [TestMethod]
        public void Test_Standard_1Person_1Day_SafeOnly()
        {
            try
            {
                var tbDays = driver.FindElementByAccessibilityId("tbDays");
                tbDays.Clear();
                tbDays.SendKeys("1");

                var tbCategory = driver.FindElementByAccessibilityId("tbCategory");
                tbCategory.Clear();
                tbCategory.SendKeys("2");

                var tbCapacity = driver.FindElementByAccessibilityId("tbCapacity");
                tbCapacity.Clear();
                tbCapacity.SendKeys("1");

                var tbSafe = driver.FindElementByAccessibilityId("tbSafe");
                tbSafe.Clear();
                tbSafe.SendKeys("да");

                var tbBreakfast = driver.FindElementByAccessibilityId("tbBreakfast");
                tbBreakfast.Clear();
                tbBreakfast.SendKeys("нет");

                var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
                btnCalculate.Click();

                var tbResult = driver.FindElementByAccessibilityId("tbResult");
                var result = tbResult.Text;
                Assert.AreEqual("4400", result);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Ошибка в тесте: {ex.Message}");
            }
        }

        [TestMethod]
        public void Test_Luxury_3People_10Days_AllExtras()
        {
            try
            {
                var tbDays = driver.FindElementByAccessibilityId("tbDays");
                tbDays.Clear();
                tbDays.SendKeys("10");

                var tbCategory = driver.FindElementByAccessibilityId("tbCategory");
                tbCategory.Clear();
                tbCategory.SendKeys("1");

                var tbCapacity = driver.FindElementByAccessibilityId("tbCapacity");
                tbCapacity.Clear();
                tbCapacity.SendKeys("3");

                var tbSafe = driver.FindElementByAccessibilityId("tbSafe");
                tbSafe.Clear();
                tbSafe.SendKeys("да");

                var tbBreakfast = driver.FindElementByAccessibilityId("tbBreakfast");
                tbBreakfast.Clear();
                tbBreakfast.SendKeys("да");

                var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
                btnCalculate.Click();

                var tbResult = driver.FindElementByAccessibilityId("tbResult");
                var result = tbResult.Text;
                Assert.AreEqual("96000", result);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Ошибка в тесте: {ex.Message}");
            }
        }

        [TestMethod]
        public void Test_Economy_1Person_1Day_NoExtras()
        {
            try
            {
                var tbDays = driver.FindElementByAccessibilityId("tbDays");
                tbDays.Clear();
                tbDays.SendKeys("1");

                var tbCategory = driver.FindElementByAccessibilityId("tbCategory");
                tbCategory.Clear();
                tbCategory.SendKeys("3");

                var tbCapacity = driver.FindElementByAccessibilityId("tbCapacity");
                tbCapacity.Clear();
                tbCapacity.SendKeys("1");

                var tbSafe = driver.FindElementByAccessibilityId("tbSafe");
                tbSafe.Clear();
                tbSafe.SendKeys("нет");

                var tbBreakfast = driver.FindElementByAccessibilityId("tbBreakfast");
                tbBreakfast.Clear();
                tbBreakfast.SendKeys("нет");

                var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
                btnCalculate.Click();

                var tbResult = driver.FindElementByAccessibilityId("tbResult");
                var result = tbResult.Text;
                Assert.AreEqual("2000", result);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Ошибка в тесте: {ex.Message}");
            }
        }
    }
}