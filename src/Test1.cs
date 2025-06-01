using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace HotelBookingTests
{
    [TestClass]
    public class HotelCostCalculatorTests
    {
        private const string AppiumServer = "http://127.0.0.1:4723/";
        private const string ApplicationPath = @"D:\WindowsFormsApp2\WindowsForms\bin\Debug\WindowsForms.exe";
        private WindowsDriver<WindowsElement> _driver;

        [TestInitialize]
        public void InitializeTestSession()
        {
            var appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", ApplicationPath);
            appiumOptions.AddAdditionalCapability("deviceName", "WindowsPC");
            _driver = new WindowsDriver<WindowsElement>(new Uri(AppiumServer), appiumOptions);

            WaitForElementToLoad("tbDays");
        }

        [TestCleanup]
        public void TerminateTestSession()
        {
            try
            {
                _driver?.CloseApp();
                _driver?.Dispose();
            }
            catch { /* Игнорируем ошибки при закрытии */ }
        }

        private void WaitForElementToLoad(string elementId, int timeoutMs = 2000)
        {
            var wait = new DefaultWait<WindowsDriver<WindowsElement>>(_driver)
            {
                Timeout = TimeSpan.FromMilliseconds(timeoutMs),
                PollingInterval = TimeSpan.FromMilliseconds(200)
            };
            wait.IgnoreExceptionTypes(typeof(WebDriverException));

            wait.Until(driver => driver.FindElementByAccessibilityId(elementId));
        }

        private void SetInputValue(string fieldId, string value)
        {
            var element = _driver.FindElementByAccessibilityId(fieldId);
            element.Clear();
            element.SendKeys(value);
        }

        private void ClickCalculateButton()
        {
            _driver.FindElementByAccessibilityId("btnCalculate").Click();
            System.Threading.Thread.Sleep(300);
        }

        private void VerifyCalculationResult(string expectedValue)
        {
            var resultElement = _driver.FindElementByAccessibilityId("tbResult");
            Assert.AreEqual(expectedValue, resultElement.Text.Trim());
        }

        [TestMethod]
        public void FullPackageWithSafeAndBreakfast()
        {
            SetInputValue("tbDays", "3");  
            SetInputValue("tbCategory", "1");
            SetInputValue("tbPlaces", "1"); 
            SetInputValue("tbSafe", "да");
            SetInputValue("tbBreakfast", "да");
            ClickCalculateButton();
            VerifyCalculationResult("14850"); 
        }

        [TestMethod]
        public void EconomyRoomSingleGuestNoExtras()
        {
            SetInputValue("tbDays", "2"); 
            SetInputValue("tbCategory", "3"); 
            SetInputValue("tbPlaces", "1");
            SetInputValue("tbSafe", "нет");
            SetInputValue("tbBreakfast", "нет");
            ClickCalculateButton();
            VerifyCalculationResult("3600"); 
        }

        [TestMethod]
        public void SafeOptionOnlyWithLowRate()
        {
            SetInputValue("tbDays", "2"); 
            SetInputValue("tbCategory", "3");
            SetInputValue("tbPlaces", "1"); 
            SetInputValue("tbSafe", "да");
            SetInputValue("tbBreakfast", "нет");
            ClickCalculateButton();
            VerifyCalculationResult("4300"); 
        }

        [TestMethod]
        public void BreakfastOnlyOptionApplied()
        {
            SetInputValue("tbDays", "1"); 
            SetInputValue("tbCategory", "2");
            SetInputValue("tbPlaces", "3"); 
            SetInputValue("tbSafe", "нет");
            SetInputValue("tbBreakfast", "да");
            ClickCalculateButton();
            VerifyCalculationResult("10500"); 
        }

        [TestMethod]
        public void InvalidDaysShouldFail()
        {
            SetInputValue("tbDays", "-5"); 
            SetInputValue("tbCategory", "1");
            SetInputValue("tbPlaces", "2");
            SetInputValue("tbSafe", "да");
            SetInputValue("tbBreakfast", "да");
            ClickCalculateButton();
            VerifyCalculationResult("");
        }

        [TestMethod]
        public void MinimumValidInputValues()
        {
            SetInputValue("tbDays", "1");
            SetInputValue("tbCategory", "3");
            SetInputValue("tbPlaces", "2"); 
            SetInputValue("tbSafe", "нет");
            SetInputValue("tbBreakfast", "нет");
            ClickCalculateButton();
            VerifyCalculationResult("3600"); 
        }

        [TestMethod]
        public void MaximumLoadWithAllExtras()
        {
            SetInputValue("tbDays", "7"); 
            SetInputValue("tbCategory", "1");
            SetInputValue("tbPlaces", "2");
            SetInputValue("tbSafe", "да");
            SetInputValue("tbBreakfast", "да");
            ClickCalculateButton();
            VerifyCalculationResult("65100");
        }

        [TestMethod]
        public void SafeOptionWithCapitalLetters()
        {
            SetInputValue("tbDays", "4"); 
            SetInputValue("tbCategory", "2");
            SetInputValue("tbPlaces", "1");
            SetInputValue("tbSafe", "Да");
            SetInputValue("tbBreakfast", "нет");
            ClickCalculateButton();
            VerifyCalculationResult("11900"); 
        }

        [TestMethod]
        public void BreakfastOptionWithCapitalLetters()
        {
            SetInputValue("tbDays", "3"); 
            SetInputValue("tbCategory", "2");
            SetInputValue("tbPlaces", "2"); 
            SetInputValue("tbSafe", "нет");
            SetInputValue("tbBreakfast", "Да");
            ClickCalculateButton();
            VerifyCalculationResult("21000"); 
        }

        [TestMethod]
        public void ZeroDaysShouldGiveNoRoomCharge()
        {
            SetInputValue("tbDays", "0");
            SetInputValue("tbCategory", "2"); 
            SetInputValue("tbPlaces", "1"); 
            SetInputValue("tbSafe", "да");
            SetInputValue("tbBreakfast", "да");
            ClickCalculateButton();
            VerifyCalculationResult("");
        }

        [TestMethod]
        public void InvalidCategoryShouldBeHandled()
        {
            SetInputValue("tbDays", "2");
            SetInputValue("tbCategory", "0"); 
            SetInputValue("tbPlaces", "1");
            SetInputValue("tbSafe", "нет");
            SetInputValue("tbBreakfast", "нет");
            ClickCalculateButton();
            VerifyCalculationResult("");
        }

        [TestMethod]
        public void InvalidPlacesInputShouldFail()
        {
            SetInputValue("tbDays", "2");
            SetInputValue("tbCategory", "2");
            SetInputValue("tbPlaces", "-1"); 
            SetInputValue("tbSafe", "да");
            SetInputValue("tbBreakfast", "нет");
            ClickCalculateButton();
            VerifyCalculationResult("");
        }
    }
}