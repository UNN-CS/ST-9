using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Threading;

namespace HotelCalculators
{
    [TestClass]
    public class HotelCalculator
    {
        private const string DriverUrl = "http://127.0.0.1:4723/";
        private const string AppPath = @"D:\WindowsFormsApp2\WindowsForms\bin\Debug\WindowsForms.exe";
        private WindowsDriver<WindowsElement> session;

        [TestInitialize]
        public void LaunchApp()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", AppPath);
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            session = new WindowsDriver<WindowsElement>(new Uri(DriverUrl), options);

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    session.FindElementByAccessibilityId("tbDays");
                    return;
                }
                catch { Thread.Sleep(200); }
            }

            Assert.Fail("Форма не загрузилась.");
        }

        [TestCleanup]
        public void CloseApp()
        {
            session?.Quit();
        }

        private void FillInput(string id, string value)
        {
            var element = session.FindElementByAccessibilityId(id);
            element.Clear();
            element.SendKeys(value);
        }

        private void TriggerCalculation()
        {
            try
            {
                var resultField = session.FindElementByAccessibilityId("tbResult");
                if (resultField.Enabled)
                    resultField.Clear();
            }
            catch
            {
            }

            session.FindElementByAccessibilityId("btnCalculate").Click();
            Thread.Sleep(500); // дать времени результату обновиться
        }



        private void AssertResult(string expected)
        {
            var result = session.FindElementByAccessibilityId("tbResult").Text;

            if (result != expected)
                Console.WriteLine($"[DEBUG] Expected: {expected}, Actual: {result}");

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void FullPackageWithSafeAndBreakfast()
        {
            FillInput("tbDays", "2");
            FillInput("tbCategory", "1");
            FillInput("tbPlaces", "2");
            FillInput("tbSafe", "да");
            FillInput("tbBreakfast", "да");
            TriggerCalculation();
            AssertResult("18900");
        }

        [TestMethod]
        public void EconomyRoomSingleGuestNoExtras()
        {
            FillInput("tbDays", "1");
            FillInput("tbCategory", "2");
            FillInput("tbPlaces", "1");
            FillInput("tbSafe", "нет");
            FillInput("tbBreakfast", "нет");
            TriggerCalculation();
            AssertResult("2800");
        }

        [TestMethod]
        public void SafeOptionOnlyWithLowRate()
        {
            FillInput("tbDays", "3");
            FillInput("tbCategory", "3");
            FillInput("tbPlaces", "2");
            FillInput("tbSafe", "да");
            FillInput("tbBreakfast", "нет");
            TriggerCalculation();
            AssertResult("11500");
        }

        [TestMethod]
        public void BreakfastOnlyOptionApplied()
        {
            FillInput("tbDays", "2");
            FillInput("tbCategory", "2");
            FillInput("tbPlaces", "2");
            FillInput("tbSafe", "нет");
            FillInput("tbBreakfast", "да");
            TriggerCalculation();
            AssertResult("12600");
        }

        [TestMethod]
        public void InvalidDaysShouldFail()
        {
            FillInput("tbDays", "abc");
            FillInput("tbCategory", "1");
            FillInput("tbPlaces", "2");
            FillInput("tbSafe", "да");
            FillInput("tbBreakfast", "да");
            TriggerCalculation();
            AssertResult("");
        }

        [TestMethod]
        public void MinimumValidInputValues()
        {
            FillInput("tbDays", "1");
            FillInput("tbCategory", "3");
            FillInput("tbPlaces", "1");
            FillInput("tbSafe", "нет");
            FillInput("tbBreakfast", "нет");
            TriggerCalculation();
            AssertResult("1800");
        }

        [TestMethod]
        public void MaximumLoadWithAllExtras()
        {
            FillInput("tbDays", "10");
            FillInput("tbCategory", "1");
            FillInput("tbPlaces", "3");
            FillInput("tbSafe", "да");
            FillInput("tbBreakfast", "да");
            TriggerCalculation();
            AssertResult("137200");
        }

        [TestMethod]
        public void SafeOptionWithCapitalLetters()
        {
            FillInput("tbDays", "1");
            FillInput("tbCategory", "2");
            FillInput("tbPlaces", "1");
            FillInput("tbSafe", "Да");
            FillInput("tbBreakfast", "нет");
            TriggerCalculation();
            AssertResult("3500");
        }

        [TestMethod]
        public void BreakfastOptionWithCapitalLetters()
        {
            FillInput("tbDays", "2");
            FillInput("tbCategory", "2");
            FillInput("tbPlaces", "1");
            FillInput("tbSafe", "нет");
            FillInput("tbBreakfast", "Да");
            TriggerCalculation();
            AssertResult("6300");
        }

        [TestMethod]
        public void ZeroDaysShouldGiveNoRoomCharge()
        {
            FillInput("tbDays", "0");
            FillInput("tbCategory", "1");
            FillInput("tbPlaces", "2");
            FillInput("tbSafe", "да");
            FillInput("tbBreakfast", "да");
            TriggerCalculation();
            AssertResult("");
        }

        [TestMethod]
        public void InvalidCategoryShouldBeHandled()
        {
            FillInput("tbDays", "2");
            FillInput("tbCategory", "5");
            FillInput("tbPlaces", "1");
            FillInput("tbSafe", "нет");
            FillInput("tbBreakfast", "нет");
            TriggerCalculation();
            AssertResult("");
        }

        [TestMethod]
        public void InvalidPlacesInputShouldFail()
        {
            FillInput("tbDays", "2");
            FillInput("tbCategory", "2");
            FillInput("tbPlaces", "abc");
            FillInput("tbSafe", "да");
            FillInput("tbBreakfast", "нет");
            TriggerCalculation();
            AssertResult("");
        }
    }
}
