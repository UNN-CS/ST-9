using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Threading;

namespace H_Calcs
{
    [TestClass]
    public class H_Calc
    {
        private const string DriverUrl = "http://127.0.0.1:4723/";
        private const string AppPath = @"D:\Users\sarafan\Poject\Poject\bin\Debug\Poject.exe";
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
                    session.FindElementByAccessibilityId("inputStayDays");
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
                var resultField = session.FindElementByAccessibilityId("outputTotal");
                if (resultField.Enabled)
                    resultField.Clear();
            }
            catch
            {
              
            }

            session.FindElementByAccessibilityId("btnCompute").Click();
            Thread.Sleep(500);
        }

        private void AssertResult(string expected)
        {
            var result = session.FindElementByAccessibilityId("outputTotal").Text;

            if (result != expected)
                Console.WriteLine($"[DEBUG] Expected: {expected}, Actual: {result}");

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void OneDayStayWithSafeAndBreakfast()
        {
            FillInput("inputStayDays", "1");
            FillInput("inputRoomClass", "1");
            FillInput("inputCapacity", "1");
            FillInput("inputSafe", "ДА");
            FillInput("inputBreakfast", "ДА");
            TriggerCalculation();
            AssertResult("101050");
        }

        [TestMethod]
        public void ThreeGuestsNoExtras()
        {
            FillInput("inputStayDays", "2");
            FillInput("inputRoomClass", "3");
            FillInput("inputCapacity", "3");
            FillInput("inputSafe", "нет");
            FillInput("inputBreakfast", "нет");
            TriggerCalculation();
            AssertResult("150000");
        }

        [TestMethod]
        public void NegativeInputShouldFail()
        {
            FillInput("inputStayDays", "-2");
            FillInput("inputRoomClass", "2");
            FillInput("inputCapacity", "2");
            FillInput("inputSafe", "да");
            FillInput("inputBreakfast", "да");
            TriggerCalculation();
            AssertResult("");
        }

        [TestMethod]
        public void EmptyFieldsShouldFail()
        {
            FillInput("inputStayDays", "");
            FillInput("inputRoomClass", "");
            FillInput("inputCapacity", "");
            FillInput("inputSafe", "");
            FillInput("inputBreakfast", "");
            TriggerCalculation();
            AssertResult("");
        }

        [TestMethod]
        public void BreakfastOnlyMixedCase()
        {
            FillInput("inputStayDays", "3");
            FillInput("inputRoomClass", "2");
            FillInput("inputCapacity", "2");
            FillInput("inputSafe", "нет");
            FillInput("inputBreakfast", "дА");
            TriggerCalculation();
            AssertResult("302100");
        }

        [TestMethod]
        public void SafeOnlyMixedCase()
        {
            FillInput("inputStayDays", "2");
            FillInput("inputRoomClass", "3");
            FillInput("inputCapacity", "1");
            FillInput("inputSafe", "дА");
            FillInput("inputBreakfast", "нет");
            TriggerCalculation();
            AssertResult("50700");
        }

        [TestMethod]
        public void NonNumericGuestsShouldFail()
        {
            FillInput("inputStayDays", "2");
            FillInput("inputRoomClass", "2");
            FillInput("inputCapacity", "one");
            FillInput("inputSafe", "да");
            FillInput("inputBreakfast", "нет");
            TriggerCalculation();
            AssertResult("");
        }

        [TestMethod]
        public void MaxDaysWithAllOptions()
        {
            FillInput("inputStayDays", "999");
            FillInput("inputRoomClass", "1");
            FillInput("inputCapacity", "1");
            FillInput("inputSafe", "да");
            FillInput("inputBreakfast", "да");
            TriggerCalculation();
            AssertResult((999 * 100000 + 700 + 350 * 999).ToString());
        }

        [TestMethod]
        public void MinimumInputWithSafe()
        {
            FillInput("inputStayDays", "1");
            FillInput("inputRoomClass", "3");
            FillInput("inputCapacity", "1");
            FillInput("inputSafe", "да");
            FillInput("inputBreakfast", "нет");
            TriggerCalculation();
            AssertResult("25700");
        }

        [TestMethod]
        public void CaseInsensitiveCheckForExtras()
        {
            FillInput("inputStayDays", "2");
            FillInput("inputRoomClass", "1");
            FillInput("inputCapacity", "2");
            FillInput("inputSafe", "Да");
            FillInput("inputBreakfast", "дА");
            TriggerCalculation();
            AssertResult("402100");
        }

        [TestMethod]
        public void EnglishWordsShouldNotBeAccepted()
        {
            FillInput("inputStayDays", "2");
            FillInput("inputRoomClass", "2");
            FillInput("inputCapacity", "2");
            FillInput("inputSafe", "yes");
            FillInput("inputBreakfast", "no");
            TriggerCalculation();
            AssertResult("200000");
        }

        [TestMethod]
        public void InvalidRoomCategoryShouldFail()
        {
            FillInput("inputStayDays", "3");
            FillInput("inputRoomClass", "0");
            FillInput("inputCapacity", "2");
            FillInput("inputSafe", "да");
            FillInput("inputBreakfast", "нет");
            TriggerCalculation();
            AssertResult("");
        }

        [TestMethod]
        public void MissingRoomClassShouldFail()
        {
            FillInput("inputStayDays", "2");
            FillInput("inputRoomClass", "");
            FillInput("inputCapacity", "2");
            FillInput("inputSafe", "да");
            FillInput("inputBreakfast", "да");
            TriggerCalculation();
            AssertResult("");
        }

    }
}
