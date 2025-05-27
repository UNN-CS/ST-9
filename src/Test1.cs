using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject1
{
    [TestClass]
    public class Tests
    {
        public const string DriveURL = "http://127.0.0.1:4723/";
        public WindowsDriver<WindowsElement> driver;

        [TestInitialize]
        public void Setup()
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", @"F:\\repos\\ST\\ST9\\WinFormsApp1\\bin\\Debug\\net8.0-windows\\WinFormsApp1.exe");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

            driver = new WindowsDriver<WindowsElement>(new Uri(DriveURL), appCapabilities);
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver?.Quit();
        }

        [TestMethod]
        public void TestEconomRoomCalculation()
        {
            // Тест расчета для номера "Эконом" на 3 дня, 2 человека без доп. услуг
            var tbDays = driver.FindElementByAccessibilityId("tbDays");
            tbDays.SendKeys("3");
            
            var cbCategory = driver.FindElementByAccessibilityId("cbCategory");
            cbCategory.Click();
            var economOption = driver.FindElementByName("Эконом");
            economOption.Click();
            
            var tbPeople = driver.FindElementByAccessibilityId("tbPeople");
            tbPeople.SendKeys("2");
            
            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            btnCalculate.Click();
            
            var tbResult = driver.FindElementByAccessibilityId("tbResult");
            // Ожидаемый результат: 1500 * 3 * 2 = 9000.00 руб.
            Assert.AreEqual("9000,00", tbResult.Text.Replace(" руб.", ""));
        }

        [TestMethod]
        public void TestStandardRoomWithBreakfast()
        {
            // Тест расчета для номера "Стандарт" на 2 дня, 1 человек с завтраком
            var tbDays = driver.FindElementByAccessibilityId("tbDays");
            tbDays.SendKeys("2");
            
            var cbCategory = driver.FindElementByAccessibilityId("cbCategory");
            cbCategory.Click();
            var standardOption = driver.FindElementByName("Стандарт");
            standardOption.Click();
            
            var tbPeople = driver.FindElementByAccessibilityId("tbPeople");
            tbPeople.SendKeys("1");
            
            var chkBreakfast = driver.FindElementByAccessibilityId("chkBreakfast");
            chkBreakfast.Click();
            
            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            btnCalculate.Click();
            
            var tbResult = driver.FindElementByAccessibilityId("tbResult");
            // Ожидаемый результат: (3000 * 2 * 1) + (500 * 2 * 1) = 6000 + 1000 = 7000.00 руб.
            Assert.AreEqual("7000,00", tbResult.Text.Replace(" руб.", ""));
        }

        [TestMethod]
        public void TestLuxuryRoomWithAllServices()
        {
            // Тест расчета для номера "Люкс" на 1 день, 2 человека со всеми услугами
            var tbDays = driver.FindElementByAccessibilityId("tbDays");
            tbDays.SendKeys("1");
            
            var cbCategory = driver.FindElementByAccessibilityId("cbCategory");
            cbCategory.Click();
            var luxOption = driver.FindElementByName("Люкс");
            luxOption.Click();
            
            var tbPeople = driver.FindElementByAccessibilityId("tbPeople");
            tbPeople.SendKeys("2");
            
            var chkBreakfast = driver.FindElementByAccessibilityId("chkBreakfast");
            chkBreakfast.Click();
            
            var chkWifi = driver.FindElementByAccessibilityId("chkWifi");
            chkWifi.Click();
            
            var chkParking = driver.FindElementByAccessibilityId("chkParking");
            chkParking.Click();
            
            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            btnCalculate.Click();
            
            var tbResult = driver.FindElementByAccessibilityId("tbResult");
            // Ожидаемый результат: (5000 * 1 * 2) + (500 * 1 * 2) + (200 * 1) + (300 * 1) = 10000 + 1000 + 200 + 300 = 11500.00 руб.
            Assert.AreEqual("11500,00", tbResult.Text.Replace(" руб.", ""));
        }

        [TestMethod]
        public void TestEconomRoomLongStay()
        {
            // Тест расчета для длительного проживания: номер "Эконом" на 7 дней, 1 человек с Wi-Fi
            var tbDays = driver.FindElementByAccessibilityId("tbDays");
            tbDays.SendKeys("7");
            
            var cbCategory = driver.FindElementByAccessibilityId("cbCategory");
            cbCategory.Click();
            var economOption = driver.FindElementByName("Эконом");
            economOption.Click();
            
            var tbPeople = driver.FindElementByAccessibilityId("tbPeople");
            tbPeople.SendKeys("1");
            
            var chkWifi = driver.FindElementByAccessibilityId("chkWifi");
            chkWifi.Click();
            
            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            btnCalculate.Click();
            
            var tbResult = driver.FindElementByAccessibilityId("tbResult");
            // Ожидаемый результат: (1500 * 7 * 1) + (200 * 7) = 10500 + 1400 = 11900.00 руб.
            Assert.AreEqual("11900,00", tbResult.Text.Replace(" руб.", ""));
        }

        [TestMethod]
        public void TestStandardRoomWithParking()
        {
            // Тест расчета для номера "Стандарт" на 4 дня, 3 человека с парковкой
            var tbDays = driver.FindElementByAccessibilityId("tbDays");
            tbDays.SendKeys("4");
            
            var cbCategory = driver.FindElementByAccessibilityId("cbCategory");
            cbCategory.Click();
            var standardOption = driver.FindElementByName("Стандарт");
            standardOption.Click();
            
            var tbPeople = driver.FindElementByAccessibilityId("tbPeople");
            tbPeople.SendKeys("3");
            
            var chkParking = driver.FindElementByAccessibilityId("chkParking");
            chkParking.Click();
            
            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            btnCalculate.Click();
            
            var tbResult = driver.FindElementByAccessibilityId("tbResult");
            // Ожидаемый результат: (3000 * 4 * 3) + (300 * 4) = 36000 + 1200 = 37200.00 руб.
            Assert.AreEqual("37200,00", tbResult.Text.Replace(" руб.", ""));
        }

        [TestMethod]
        public void TestMinimalStay()
        {
            // Тест минимального проживания: номер "Эконом" на 1 день, 1 человек без доп. услуг
            var tbDays = driver.FindElementByAccessibilityId("tbDays");
            tbDays.SendKeys("1");
            
            var cbCategory = driver.FindElementByAccessibilityId("cbCategory");
            cbCategory.Click();
            var economOption = driver.FindElementByName("Эконом");
            economOption.Click();
            
            var tbPeople = driver.FindElementByAccessibilityId("tbPeople");
            tbPeople.SendKeys("1");
            
            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            btnCalculate.Click();
            
            var tbResult = driver.FindElementByAccessibilityId("tbResult");
            // Ожидаемый результат: 1500 * 1 * 1 = 1500.00 руб.
            Assert.AreEqual("1500,00", tbResult.Text.Replace(" руб.", ""));
        }
    }
} 