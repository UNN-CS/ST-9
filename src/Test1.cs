using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;

namespace TestProject1
{
    [TestClass]
    public sealed class Test1
    {
        public const string DriverURL = "http://127.0.0.1:4723/";
        public WindowsDriver<WindowsElement> driver;

        [TestInitialize]
        public void Setup()
        {
            var app = new AppiumOptions();
            app.AddAdditionalCapability("app", @"C:\Users\1367873\source\repos\WinFormsApp1\WinFormsApp1\bin\Debug\net8.0-windows\WinFormsApp1.exe");
            app.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            app.AddAdditionalCapability("deviceName", "WindowsPC");

            driver = new WindowsDriver<WindowsElement>(new Uri(DriverURL), app);
        }
        [TestCleanup]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }

        [TestMethod]
        public void TestEconomySingleRoomNoExtras()
        {
            driver.FindElementByAccessibilityId("txtDays").SendKeys("3");
            driver.FindElementByAccessibilityId("txtCategory").SendKeys("1");
            driver.FindElementByAccessibilityId("txtCapacity").SendKeys("1");
            driver.FindElementByAccessibilityId("txtSafe").SendKeys("нет");
            driver.FindElementByAccessibilityId("txtBreakfast").SendKeys("нет");

            driver.FindElementByAccessibilityId("button1").Click();

            var result = driver.FindElementByAccessibilityId("txtSum").Text;
            Assert.AreEqual("3 000 руб.", result);
        }

        [TestMethod]
        public void TestStandardDoubleRoomWithBreakfast()
        {
            driver.FindElementByAccessibilityId("txtDays").SendKeys("2");
            driver.FindElementByAccessibilityId("txtCategory").SendKeys("2");
            driver.FindElementByAccessibilityId("txtCapacity").SendKeys("2");
            driver.FindElementByAccessibilityId("txtSafe").SendKeys("нет");
            driver.FindElementByAccessibilityId("txtBreakfast").SendKeys("да");

            driver.FindElementByAccessibilityId("button1").Click();

            var result = driver.FindElementByAccessibilityId("txtSum").Text;
            Assert.AreEqual("6 400 руб.", result);
        }

        [TestMethod]
        public void TestLuxTripleRoomWithAllExtras()
        {
            driver.FindElementByAccessibilityId("txtDays").SendKeys("1");
            driver.FindElementByAccessibilityId("txtCategory").SendKeys("3");
            driver.FindElementByAccessibilityId("txtCapacity").SendKeys("3");
            driver.FindElementByAccessibilityId("txtSafe").SendKeys("да");
            driver.FindElementByAccessibilityId("txtBreakfast").SendKeys("да");

            driver.FindElementByAccessibilityId("button1").Click();

            var result = driver.FindElementByAccessibilityId("txtSum").Text;
            Assert.AreEqual("5 800 руб.", result);
        }

        [TestMethod]
        public void TestInvalidCategoryShowsError()
        {
            driver.FindElementByAccessibilityId("txtDays").SendKeys("1");
            driver.FindElementByAccessibilityId("txtCategory").SendKeys("4");
            driver.FindElementByAccessibilityId("txtCapacity").SendKeys("1");
            driver.FindElementByAccessibilityId("txtSafe").SendKeys("нет");
            driver.FindElementByAccessibilityId("txtBreakfast").SendKeys("нет");

            driver.FindElementByAccessibilityId("button1").Click();

            var result = driver.FindElementByAccessibilityId("txtSum").Text;
            Assert.IsTrue(string.IsNullOrEmpty(result) || !result.Contains("5 800 руб."));
        }

        [TestMethod]
        public void TestZeroDaysShowsError()
        {
            driver.FindElementByAccessibilityId("txtDays").SendKeys("0");
            driver.FindElementByAccessibilityId("txtCategory").SendKeys("1");
            driver.FindElementByAccessibilityId("txtCapacity").SendKeys("1");
            driver.FindElementByAccessibilityId("txtSafe").SendKeys("нет");
            driver.FindElementByAccessibilityId("txtBreakfast").SendKeys("нет");

            driver.FindElementByAccessibilityId("button1").Click();

            var result = driver.FindElementByAccessibilityId("txtSum").Text;
            Assert.IsTrue(string.IsNullOrEmpty(result) || !result.Contains("3 000 руб."));
        }

        [TestMethod]
        public void TestNegativeDaysShowsError()
        {
            driver.FindElementByAccessibilityId("txtDays").SendKeys("-5");
            driver.FindElementByAccessibilityId("txtCategory").SendKeys("1");
            driver.FindElementByAccessibilityId("txtCapacity").SendKeys("1");
            driver.FindElementByAccessibilityId("txtSafe").SendKeys("нет");
            driver.FindElementByAccessibilityId("txtBreakfast").SendKeys("нет");

            driver.FindElementByAccessibilityId("button1").Click();

            var result = driver.FindElementByAccessibilityId("txtSum").Text;
            Assert.IsTrue(string.IsNullOrEmpty(result) || !result.Contains("3 000 руб."));
        }

        [TestMethod]
        public void TestNonNumericInputShowsError()
        {
            driver.FindElementByAccessibilityId("txtDays").SendKeys("abc");
            driver.FindElementByAccessibilityId("txtCategory").SendKeys("2");
            driver.FindElementByAccessibilityId("txtCapacity").SendKeys("1");
            driver.FindElementByAccessibilityId("txtSafe").SendKeys("нет");
            driver.FindElementByAccessibilityId("txtBreakfast").SendKeys("да");

            driver.FindElementByAccessibilityId("button1").Click();

            var result = driver.FindElementByAccessibilityId("txtSum").Text;
            Assert.IsTrue(string.IsNullOrEmpty(result) || !result.Contains("3 000 руб."));
        }
    }
}
