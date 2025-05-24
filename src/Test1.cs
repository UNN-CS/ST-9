using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace HotelCalculatorTests
{
    [TestClass]
    public class Test1
    {
        private const string DriverUrl = "http://127.0.0.1:4723/";
        private const string AppPath = @"C:\Users\shvedovav\source\repos\HotelCalculator\bin\Debug\net8.0-windows\HotelCalculator.exe";

        private WindowsDriver<WindowsElement>? driver;

        /// <summary>
        /// Initialize WinAppDriver session before each test.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", AppPath);
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            options.AddAdditionalCapability("ms:waitForAppLaunch", "5");

            driver = new WindowsDriver<WindowsElement>(new Uri(DriverUrl), options);
        }

        [TestMethod]
        public void EconomySingleNoExtras_ShouldBeCorrect()
        {
            driver!.FindElementByAccessibilityId("tbDays").SendKeys("1");
            driver.FindElementByAccessibilityId("tbCategory").SendKeys("1");
            driver.FindElementByAccessibilityId("tbCapacity").SendKeys("1");
            driver.FindElementByAccessibilityId("tbSafe").SendKeys("нет");
            driver.FindElementByAccessibilityId("tbBreakfast").SendKeys("нет");

            driver.FindElementByAccessibilityId("btnCalculate").Click();
            var total = driver.FindElementByAccessibilityId("tbTotal").Text;

            Assert.AreEqual("950", total);
        }

        [TestMethod]
        public void StandardDoubleWithoutExtras_ShouldBeCorrect()
        {
            driver!.FindElementByAccessibilityId("tbDays").SendKeys("2");
            driver.FindElementByAccessibilityId("tbCategory").SendKeys("2");
            driver.FindElementByAccessibilityId("tbCapacity").SendKeys("2");
            driver.FindElementByAccessibilityId("tbSafe").SendKeys("нет");
            driver.FindElementByAccessibilityId("tbBreakfast").SendKeys("нет");

            driver.FindElementByAccessibilityId("btnCalculate").Click();
            Assert.AreEqual("4500", driver.FindElementByAccessibilityId("tbTotal").Text);
        }

        [TestMethod]
        public void LuxTripleWithSafeOnly_ShouldBeCorrect()
        {
            driver!.FindElementByAccessibilityId("tbDays").SendKeys("1");
            driver.FindElementByAccessibilityId("tbCategory").SendKeys("3");
            driver.FindElementByAccessibilityId("tbCapacity").SendKeys("3");
            driver.FindElementByAccessibilityId("tbSafe").SendKeys("да");
            driver.FindElementByAccessibilityId("tbBreakfast").SendKeys("нет");

            driver.FindElementByAccessibilityId("btnCalculate").Click();
            Assert.AreEqual("3900", driver.FindElementByAccessibilityId("tbTotal").Text);
        }

        [TestMethod]
        public void EconomyWithBreakfastOnly_ShouldBeCorrect()
        {
            driver!.FindElementByAccessibilityId("tbDays").SendKeys("3");
            driver.FindElementByAccessibilityId("tbCategory").SendKeys("1");
            driver.FindElementByAccessibilityId("tbCapacity").SendKeys("1");
            driver.FindElementByAccessibilityId("tbSafe").SendKeys("нет");
            driver.FindElementByAccessibilityId("tbBreakfast").SendKeys("да");

            driver.FindElementByAccessibilityId("btnCalculate").Click();
            Assert.AreEqual("3660", driver.FindElementByAccessibilityId("tbTotal").Text);
        }

        [TestMethod]
        public void LuxDoubleWithExtras_TwoDays_ShouldBeCorrect()
        {
            driver!.FindElementByAccessibilityId("tbDays").SendKeys("2");
            driver.FindElementByAccessibilityId("tbCategory").SendKeys("3");
            driver.FindElementByAccessibilityId("tbCapacity").SendKeys("2");
            driver.FindElementByAccessibilityId("tbSafe").SendKeys("да");
            driver.FindElementByAccessibilityId("tbBreakfast").SendKeys("да");

            driver.FindElementByAccessibilityId("btnCalculate").Click();
            Assert.AreEqual("7440", driver.FindElementByAccessibilityId("tbTotal").Text);
        }

        [TestMethod]
        public void StandardTripleNoExtras_ShouldBeCorrect()
        {
            driver!.FindElementByAccessibilityId("tbDays").SendKeys("1");
            driver.FindElementByAccessibilityId("tbCategory").SendKeys("2");
            driver.FindElementByAccessibilityId("tbCapacity").SendKeys("3");
            driver.FindElementByAccessibilityId("tbSafe").SendKeys("нет");
            driver.FindElementByAccessibilityId("tbBreakfast").SendKeys("нет");

            driver.FindElementByAccessibilityId("btnCalculate").Click();
            Assert.AreEqual("2700", driver.FindElementByAccessibilityId("tbTotal").Text);
        }

        [TestMethod]
        public void EmptyInputs_ShouldShowNothing()
        {
            driver!.FindElementByAccessibilityId("btnCalculate").Click();
            string result = driver.FindElementByAccessibilityId("tbTotal").Text;
            Assert.IsTrue(string.IsNullOrEmpty(result));
        }

        [TestMethod]
        public void InvalidDaysInput_ShouldNotCrash()
        {
            driver!.FindElementByAccessibilityId("tbDays").SendKeys("abc");
            driver.FindElementByAccessibilityId("tbCategory").SendKeys("1");
            driver.FindElementByAccessibilityId("tbCapacity").SendKeys("1");
            driver.FindElementByAccessibilityId("tbSafe").SendKeys("нет");
            driver.FindElementByAccessibilityId("tbBreakfast").SendKeys("нет");

            driver.FindElementByAccessibilityId("btnCalculate").Click();
            string result = driver.FindElementByAccessibilityId("tbTotal").Text;
            Assert.IsTrue(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public void InvalidCategory_ShouldCalculateAsZeroBase()
        {
            driver!.FindElementByAccessibilityId("tbDays").SendKeys("1");
            driver.FindElementByAccessibilityId("tbCategory").SendKeys("9");
            driver.FindElementByAccessibilityId("tbCapacity").SendKeys("2");
            driver.FindElementByAccessibilityId("tbSafe").SendKeys("да");
            driver.FindElementByAccessibilityId("tbBreakfast").SendKeys("да");

            driver.FindElementByAccessibilityId("btnCalculate").Click();
            Assert.AreEqual("920", driver.FindElementByAccessibilityId("tbTotal").Text);
        }

        [TestMethod]
        public void YesInputs_ShouldWorkCorrectly()
        {
            driver!.FindElementByAccessibilityId("tbDays").SendKeys("1");
            driver.FindElementByAccessibilityId("tbCategory").SendKeys("1");
            driver.FindElementByAccessibilityId("tbCapacity").SendKeys("1");
            driver.FindElementByAccessibilityId("tbSafe").SendKeys("yes");
            driver.FindElementByAccessibilityId("tbBreakfast").SendKeys("yes");

            driver.FindElementByAccessibilityId("btnCalculate").Click();
            var result = driver.FindElementByAccessibilityId("tbTotal").Text;

            Assert.AreEqual("1470", result);
        }

        [TestMethod]
        public void NoInputs_ShouldAlsoWorkCorrectly()
        {
            driver!.FindElementByAccessibilityId("tbDays").SendKeys("2");
            driver.FindElementByAccessibilityId("tbCategory").SendKeys("2");
            driver.FindElementByAccessibilityId("tbCapacity").SendKeys("2");
            driver.FindElementByAccessibilityId("tbSafe").SendKeys("no");
            driver.FindElementByAccessibilityId("tbBreakfast").SendKeys("no");

            driver.FindElementByAccessibilityId("btnCalculate").Click();
            var total = driver.FindElementByAccessibilityId("tbTotal").Text;

            Assert.AreEqual("4500", total);
        }

        [TestMethod]
        public void Mixed_YesNo_ShouldBeHandled()
        {
            driver!.FindElementByAccessibilityId("tbDays").SendKeys("3");
            driver.FindElementByAccessibilityId("tbCategory").SendKeys("2");
            driver.FindElementByAccessibilityId("tbCapacity").SendKeys("1");
            driver.FindElementByAccessibilityId("tbSafe").SendKeys("yes");
            driver.FindElementByAccessibilityId("tbBreakfast").SendKeys("no");

            driver.FindElementByAccessibilityId("btnCalculate").Click();
            var result = driver.FindElementByAccessibilityId("tbTotal").Text;

            Assert.AreEqual("6300", result);
        }

        [TestMethod]
        public void Mixed_NoYes_ShouldBeHandled()
        {
            driver!.FindElementByAccessibilityId("tbDays").SendKeys("2");
            driver.FindElementByAccessibilityId("tbCategory").SendKeys("1");
            driver.FindElementByAccessibilityId("tbCapacity").SendKeys("2");
            driver.FindElementByAccessibilityId("tbSafe").SendKeys("no");
            driver.FindElementByAccessibilityId("tbBreakfast").SendKeys("yes");

            driver.FindElementByAccessibilityId("btnCalculate").Click();
            var result = driver.FindElementByAccessibilityId("tbTotal").Text;

            Assert.AreEqual("3240", result);
        }

        [TestMethod]
        public void YesNoUppercase_ShouldBeAccepted()
        {
            driver!.FindElementByAccessibilityId("tbDays").SendKeys("1");
            driver.FindElementByAccessibilityId("tbCategory").SendKeys("3");
            driver.FindElementByAccessibilityId("tbCapacity").SendKeys("3");
            driver.FindElementByAccessibilityId("tbSafe").SendKeys("YES");
            driver.FindElementByAccessibilityId("tbBreakfast").SendKeys("NO");

            driver.FindElementByAccessibilityId("btnCalculate").Click();
            var result = driver.FindElementByAccessibilityId("tbTotal").Text;

            Assert.AreEqual("3900", result);
        }



        /// <summary>
        /// Close the application session after each test.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            driver?.Quit();
        }
    }
}
