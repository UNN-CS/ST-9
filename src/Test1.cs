using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace HotelCalculatorTest
{
    [TestClass]
    public class UnitTest1
    {
        public const string DriveURL = "http://127.0.0.1:4723/";
        public WindowsDriver<WindowsElement> driver;
        [TestInitialize]
        public void Setup()
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", @"C:\Users\egorm\source\repos\HotelCalculator\HotelForm\bin\Debug\net8.0-windows\HotelForm.exe");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

            driver = new WindowsDriver<WindowsElement>(new Uri(DriveURL), appCapabilities, TimeSpan.FromSeconds(30));
        }
        [TestMethod]
        public void TestError1()
        {
            var button = driver.FindElementByAccessibilityId("button1");
            button.Click();
            var result = driver.FindElementByAccessibilityId("result");
            Assert.AreEqual(result.Text, "error");
        }
        [TestMethod]
        public void TestError2()
        {
            var countDays = driver.FindElementByAccessibilityId("countDays");
            countDays.SendKeys("1");
            var button = driver.FindElementByAccessibilityId("button1");
            button.Click();
            var result = driver.FindElementByAccessibilityId("result");
            Assert.AreEqual(result.Text, "error");
        }
        [TestMethod]
        public void TestError3()
        {
            var countDays = driver.FindElementByAccessibilityId("countDays");
            countDays.SendKeys("1");
            var carRoom = driver.FindElementByAccessibilityId("catRoom");
            carRoom.SendKeys("4");
            var volRoom = driver.FindElementByAccessibilityId("volRoom");
            volRoom.SendKeys("1");
            var button = driver.FindElementByAccessibilityId("button1");
            button.Click();
            var result = driver.FindElementByAccessibilityId("result");
            Assert.AreEqual(result.Text, "error");
        }
        [TestMethod]
        public void TestMain1()
        {
            var countDays = driver.FindElementByAccessibilityId("countDays");
            countDays.SendKeys("1");
            var carRoom = driver.FindElementByAccessibilityId("catRoom");
            carRoom.SendKeys("1");
            var volRoom = driver.FindElementByAccessibilityId("volRoom");
            volRoom.SendKeys("1");
            var button = driver.FindElementByAccessibilityId("button1");
            button.Click();
            var result = driver.FindElementByAccessibilityId("result");
            Assert.AreEqual(result.Text, "500");
        }
        [TestMethod]
        public void TestMain2()
        {
            var countDays = driver.FindElementByAccessibilityId("countDays");
            countDays.SendKeys("1");
            var carRoom = driver.FindElementByAccessibilityId("catRoom");
            carRoom.SendKeys("3");
            var volRoom = driver.FindElementByAccessibilityId("volRoom");
            volRoom.SendKeys("1");
            var button = driver.FindElementByAccessibilityId("button1");
            button.Click();
            var result = driver.FindElementByAccessibilityId("result");
            Assert.AreEqual(result.Text, "900");
        }
        [TestMethod]
        public void TestMain3()
        {
            var countDays = driver.FindElementByAccessibilityId("countDays");
            countDays.SendKeys("1");
            var carRoom = driver.FindElementByAccessibilityId("catRoom");
            carRoom.SendKeys("3");
            var volRoom = driver.FindElementByAccessibilityId("volRoom");
            volRoom.SendKeys("2");
            var button = driver.FindElementByAccessibilityId("button1");
            button.Click();
            var result = driver.FindElementByAccessibilityId("result");
            Assert.AreEqual(result.Text, "1300");
        }
        [TestMethod]
        public void TestMain4()
        {
            var countDays = driver.FindElementByAccessibilityId("countDays");
            countDays.SendKeys("1");
            var carRoom = driver.FindElementByAccessibilityId("catRoom");
            carRoom.SendKeys("1");
            var volRoom = driver.FindElementByAccessibilityId("volRoom");
            volRoom.SendKeys("1");
            var isSafe = driver.FindElementByAccessibilityId("isSafe");
            isSafe.SendKeys("да");
            var button = driver.FindElementByAccessibilityId("button1");
            button.Click();
            var result = driver.FindElementByAccessibilityId("result");
            Assert.AreEqual(result.Text, "700");
        }
        [TestMethod]
        public void TestMain5()
        {
            var countDays = driver.FindElementByAccessibilityId("countDays");
            countDays.SendKeys("1");
            var carRoom = driver.FindElementByAccessibilityId("catRoom");
            carRoom.SendKeys("1");
            var volRoom = driver.FindElementByAccessibilityId("volRoom");
            volRoom.SendKeys("1");
            var isSafe = driver.FindElementByAccessibilityId("isSafe");
            isSafe.SendKeys("no");
            var button = driver.FindElementByAccessibilityId("button1");
            button.Click();
            var result = driver.FindElementByAccessibilityId("result");
            Assert.AreEqual(result.Text, "500");
        }
        [TestMethod]
        public void TestMain6()
        {
            var countDays = driver.FindElementByAccessibilityId("countDays");
            countDays.SendKeys("1");
            var carRoom = driver.FindElementByAccessibilityId("catRoom");
            carRoom.SendKeys("1");
            var volRoom = driver.FindElementByAccessibilityId("volRoom");
            volRoom.SendKeys("1");
            var isSafe = driver.FindElementByAccessibilityId("isSafe");
            isSafe.SendKeys("no");
            var isBreakfast = driver.FindElementByAccessibilityId("isBreakfast");
            isBreakfast.SendKeys("yes");
            var button = driver.FindElementByAccessibilityId("button1");
            button.Click();
            var result = driver.FindElementByAccessibilityId("result");
            Assert.AreEqual(result.Text, "850");
        }
        [TestMethod]
        public void TestMain7()
        {
            var countDays = driver.FindElementByAccessibilityId("countDays");
            countDays.SendKeys("1");
            var carRoom = driver.FindElementByAccessibilityId("catRoom");
            carRoom.SendKeys("1");
            var volRoom = driver.FindElementByAccessibilityId("volRoom");
            volRoom.SendKeys("1");
            var isSafe = driver.FindElementByAccessibilityId("isSafe");
            isSafe.SendKeys("yes");
            var isBreakfast = driver.FindElementByAccessibilityId("isBreakfast");
            isBreakfast.SendKeys("yes");
            var button = driver.FindElementByAccessibilityId("button1");
            button.Click();
            var result = driver.FindElementByAccessibilityId("result");
            Assert.AreEqual(result.Text, "1050");
        }
    }
}