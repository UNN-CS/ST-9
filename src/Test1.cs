using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace HotelCalculatorTests
{
    [TestClass]
    public class Tests
    {
        private const string DriverUrl = "http://127.0.0.1:4723/";
        private const string AppPath = @"C:\Users\source\repos\HotelCalculator\HotelCalculator\bin\Debug\net8.0-windows\HotelCalculator.exe";
        private WindowsDriver<WindowsElement> _driver;

        [TestInitialize]
        public void Setup()
        {
            var appOptions = new AppiumOptions
            {
                PlatformName = "Windows"
            };
            appOptions.AddAdditionalCapability("app", AppPath);
            appOptions.AddAdditionalCapability("deviceName", "WindowsPC");

            _driver = new WindowsDriver<WindowsElement>(new Uri(DriverUrl), appOptions);
            System.Threading.Thread.Sleep(2000);
        }

        private void EnterDataAndCalculate(string days, string category, string capacity, string safe, string breakfast)
        {
            ClearAllInputs();
            
            _driver.FindElementByAccessibilityId("tbDays").SendKeys(days);
            _driver.FindElementByAccessibilityId("tbCategory").SendKeys(category);
            _driver.FindElementByAccessibilityId("tbCapacity").SendKeys(capacity);
            _driver.FindElementByAccessibilityId("tbSafe").SendKeys(safe);
            _driver.FindElementByAccessibilityId("tbBreakfast").SendKeys(breakfast);

            _driver.FindElementByAccessibilityId("btnCalculate").Click();
            System.Threading.Thread.Sleep(500);
        }

        private void ClearAllInputs()
        {
            string[] inputIds = { "tbDays", "tbCategory", "tbCapacity", "tbSafe", "tbBreakfast" };
            foreach (var id in inputIds)
            {
                _driver.FindElementByAccessibilityId(id).Clear();
            }
        }

        private string GetTotalResult()
        {
            return _driver.FindElementByAccessibilityId("tbTotal").Text;
        }

        [TestMethod]
        public void Test_EconomySingleWithoutExtras_OneDay()
        {
            EnterDataAndCalculate("1", "1", "1", "нет", "нет");
            Assert.AreEqual("1800", GetTotalResult());
        }

        [TestMethod]
        public void Test_StandardDoubleWithSafe_TwoDays()
        {
            EnterDataAndCalculate("2", "2", "2", "да", "нет");
            Assert.AreEqual("9800", GetTotalResult());
        }

        [TestMethod]
        public void Test_LuxTripleWithBreakfast_FourDays()
        {
            EnterDataAndCalculate("4", "3", "3", "нет", "да");
            Assert.AreEqual("36000", GetTotalResult());
        }

        [TestMethod]
        public void Test_EconomyTripleWithAllExtras_SevenDays()
        {
            EnterDataAndCalculate("7", "1", "3", "да", "да");
            Assert.AreEqual("35000", GetTotalResult());
        }

        [TestMethod]
        public void Test_StandardSingleWithAllExtras_OneDay()
        {
            EnterDataAndCalculate("1", "2", "1", "да", "да");
            Assert.AreEqual("5000", GetTotalResult());
        }

        [TestMethod]
        public void Test_LuxSingleWithoutExtras_TenDays()
        {
            EnterDataAndCalculate("10", "3", "1", "нет", "нет");
            Assert.AreEqual("65000", GetTotalResult());
        }

        [TestMethod]
        public void Test_EconomySingleWithoutExtras_ZeroDays()
        {
            EnterDataAndCalculate("0", "1", "1", "нет", "нет");
            Assert.AreEqual("0", GetTotalResult());
        }

        [TestMethod]
        public void Test_LuxTripleWithAllExtras_LargeNumberOfDays()
        {
            EnterDataAndCalculate("30", "3", "3", "да", "да");
            Assert.AreEqual("300000", GetTotalResult());
        }

        [TestMethod]
        public void Test_CaseInsensitiveInput_SafeBreakfast()
        {
            EnterDataAndCalculate("1", "1", "1", "НЕТ", "Да");
            Assert.AreEqual("2800", GetTotalResult());
        }

        [TestMethod]
        public void Test_EmptyInput_HandlesErrorDialogAndChecksOutput()
        {
            ClearAllInputs();
            _driver.FindElementByAccessibilityId("btnCalculate").Click();
            System.Threading.Thread.Sleep(1000);

            try
            {
                var okButton = FindOkButton();
                okButton?.Click();
            }
            catch
            {
                Assert.Fail("Failed to find and click OK button in error dialog");
            }

            Assert.AreEqual("", GetTotalResult(), 
                "Total field should be empty after invalid input and error dialog");
        }

        private WindowsElement FindOkButton()
        {
            try
            {
                return _driver.FindElementByName("ОК");
            }
            catch
            {
                try
                {
                    return _driver.FindElementByAccessibilityId("2");
                }
                catch
                {
                    return null;
                }
            }
        }

        [TestCleanup]
        public void TearDown()
        {
            _driver?.Quit();
        }
    }
}