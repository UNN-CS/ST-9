using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Threading;

namespace HotelTests
{
    [TestClass]
    public class Test1
    {
        private const string WinAppDriverUrl = "http://127.0.0.1:4723/";
        private const string AppPath = @"C:\Users\Admin\source\repos\WindowsForm\WindowsForm\bin\Debug\WindowsForm.exe";
        private WindowsDriver<WindowsElement> driver;

        [TestInitialize]
        public void Setup()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", AppPath);
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            options.AddAdditionalCapability("ms:waitForAppLaunch", "5");

            driver = new WindowsDriver<WindowsElement>(new Uri(WinAppDriverUrl), options);

            for (int i = 0; i < 15; i++)
            {
                try
                {
                    driver.FindElementByAccessibilityId("tbDays");
                    return;
                }
                catch
                {
                    Thread.Sleep(300);
                }
            }

            Assert.Fail("Не удалось найти поле tbDays. Возможно, форма не загрузилась.");
        }

        [TestCleanup]
        public void TearDown()
        {
            driver?.Quit();
        }

        private void EnterText(string id, string value)
        {
            try
            {
                var field = driver.FindElementByAccessibilityId(id);
                field.Clear();
                field.SendKeys(value);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Ошибка при вводе в поле {id}: {ex.Message}");
            }
        }

        private void Calculate()
        {
            Thread.Sleep(300);
            driver.FindElementByAccessibilityId("btnCalculate").Click();
        }

        [TestMethod]
        public void Scenario_WithAllOptions()
        {
            EnterText("tbDays", "2");
            EnterText("tbCategory", "1");
            EnterText("tbPlaces", "2");
            EnterText("tbSafe", "да");
            EnterText("tbBreakfast", "да");
            Calculate();
            Assert.AreEqual("21700", driver.FindElementByAccessibilityId("tbResult").Text);
        }

        [TestMethod]
        public void Scenario_BasicRoomOnly()
        {
            EnterText("tbDays", "1");
            EnterText("tbCategory", "2");
            EnterText("tbPlaces", "1");
            EnterText("tbSafe", "нет");
            EnterText("tbBreakfast", "нет");
            Calculate();
            Assert.AreEqual("3000", driver.FindElementByAccessibilityId("tbResult").Text);
        }

        [TestMethod]
        public void Scenario_SafeOptionOnly()
        {
            EnterText("tbDays", "3");
            EnterText("tbCategory", "3");
            EnterText("tbPlaces", "2");
            EnterText("tbSafe", "да");
            EnterText("tbBreakfast", "нет");
            Calculate();
            Assert.AreEqual("9500", driver.FindElementByAccessibilityId("tbResult").Text);
        }

        [TestMethod]
        public void Scenario_OnlyBreakfastSelected()
        {
            EnterText("tbDays", "2");
            EnterText("tbCategory", "2");
            EnterText("tbPlaces", "2");
            EnterText("tbSafe", "нет");
            EnterText("tbBreakfast", "да");
            Calculate();
            Assert.AreEqual("13200", driver.FindElementByAccessibilityId("tbResult").Text);
        }

        [TestMethod]
        public void Scenario_InputIsInvalid()
        {
            EnterText("tbDays", "abc");
            EnterText("tbCategory", "1");
            EnterText("tbPlaces", "2");
            EnterText("tbSafe", "да");
            EnterText("tbBreakfast", "да");
            Calculate();
            Assert.AreEqual("", driver.FindElementByAccessibilityId("tbResult").Text);
        }

        [TestMethod]
        public void Scenario_MinimumAllowedValues()
        {
            EnterText("tbDays", "1");
            EnterText("tbCategory", "3");
            EnterText("tbPlaces", "1");
            EnterText("tbSafe", "нет");
            EnterText("tbBreakfast", "нет");
            Calculate();
            Assert.AreEqual("1500", driver.FindElementByAccessibilityId("tbResult").Text);
        }

        [TestMethod]
        public void Scenario_MaxInputCombination()
        {
            EnterText("tbDays", "10");
            EnterText("tbCategory", "1");
            EnterText("tbPlaces", "3");
            EnterText("tbSafe", "да");
            EnterText("tbBreakfast", "да");
            Calculate();
            Assert.AreEqual("159500", driver.FindElementByAccessibilityId("tbResult").Text);
        }

        [TestMethod]
        public void Scenario_SafeUppercaseInput()
        {
            EnterText("tbDays", "1");
            EnterText("tbCategory", "2");
            EnterText("tbPlaces", "1");
            EnterText("tbSafe", "Да");
            EnterText("tbBreakfast", "нет");
            Calculate();
            Assert.AreEqual("3500", driver.FindElementByAccessibilityId("tbResult").Text);
        }

        [TestMethod]
        public void Scenario_BreakfastUppercase()
        {
            EnterText("tbDays", "2");
            EnterText("tbCategory", "2");
            EnterText("tbPlaces", "1");
            EnterText("tbSafe", "нет");
            EnterText("tbBreakfast", "Да");
            Calculate();
            Assert.AreEqual("6600", driver.FindElementByAccessibilityId("tbResult").Text);
        }

        [TestMethod]
        public void Scenario_NoDaysInput()
        {
            EnterText("tbDays", "0");
            EnterText("tbCategory", "1");
            EnterText("tbPlaces", "2");
            EnterText("tbSafe", "да");
            EnterText("tbBreakfast", "да");
            Calculate();
            Assert.AreEqual("500", driver.FindElementByAccessibilityId("tbResult").Text);
        }
    }
}
