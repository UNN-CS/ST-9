using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;

namespace MS_Test
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
            appCapabilities.AddAdditionalCapability("app", @"C:\Users\1\source\repos\Hotel Calculator\Hotel Calculator\bin\Debug\net8.0-windows\Hotel Calculator.exe");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

            driver = new WindowsDriver<WindowsElement>(new Uri(DriveURL), appCapabilities);
        }
        [TestMethod]
        public void Test1()
        {
            var sdTextBox = driver.FindElementByAccessibilityId("days");
            sdTextBox.SendKeys("10");
            var rtTextBox = driver.FindElementByAccessibilityId("category");
            rtTextBox.SendKeys("2");
            var paTextBox = driver.FindElementByAccessibilityId("capacity");
            paTextBox.SendKeys("3");
            var vTextBox = driver.FindElementByAccessibilityId("safe");
            vTextBox.SendKeys("0");
            var bfastTextBox = driver.FindElementByAccessibilityId("breakfast");
            bfastTextBox.SendKeys("1");
            driver.FindElementByAccessibilityId("button_calc").Click();
            var answer = driver.FindElementByAccessibilityId("label_sum").Text;
            Assert.AreEqual(answer, "18500");
        }
        [TestMethod]
        public void Test2()
        {
            var sdTextBox = driver.FindElementByAccessibilityId("days");
            sdTextBox.SendKeys("1");
            var rtTextBox = driver.FindElementByAccessibilityId("category");
            rtTextBox.SendKeys("2");
            var paTextBox = driver.FindElementByAccessibilityId("capacity");
            paTextBox.SendKeys("2");
            var vTextBox = driver.FindElementByAccessibilityId("safe");
            vTextBox.SendKeys("1");
            var bfastTextBox = driver.FindElementByAccessibilityId("breakfast");
            bfastTextBox.SendKeys("1");
            driver.FindElementByAccessibilityId("button_calc").Click();
            var answer = driver.FindElementByAccessibilityId("label_sum").Text;
            Assert.AreEqual(answer, "2600");
        }
        [TestMethod]
        public void Test3()
        {
            var sdTextBox = driver.FindElementByAccessibilityId("days");
            sdTextBox.SendKeys("8");
            var rtTextBox = driver.FindElementByAccessibilityId("category");
            rtTextBox.SendKeys("3");
            var paTextBox = driver.FindElementByAccessibilityId("capacity");
            paTextBox.SendKeys("1");
            var vTextBox = driver.FindElementByAccessibilityId("safe");
            vTextBox.SendKeys("1");
            var bfastTextBox = driver.FindElementByAccessibilityId("breakfast");
            bfastTextBox.SendKeys("1");
            driver.FindElementByAccessibilityId("button_calc").Click();
            var answer = driver.FindElementByAccessibilityId("label_sum").Text;
            Assert.AreEqual(answer, "17300");
        }
        [TestMethod]
        public void Test4()
        {
            var sdTextBox = driver.FindElementByAccessibilityId("days");
            sdTextBox.SendKeys("5");
            var rtTextBox = driver.FindElementByAccessibilityId("category");
            rtTextBox.SendKeys("1");
            var paTextBox = driver.FindElementByAccessibilityId("capacity");
            paTextBox.SendKeys("3");
            var vTextBox = driver.FindElementByAccessibilityId("safe");
            vTextBox.SendKeys("10");
            var bfastTextBox = driver.FindElementByAccessibilityId("breakfast");
            bfastTextBox.SendKeys("0");
            driver.FindElementByAccessibilityId("button_calc").Click();
            var answer = driver.FindElementByAccessibilityId("label_sum").Text;
            Assert.AreEqual(answer, "7000");
        }
        [TestMethod]
        public void Test5()
        {
            var sdTextBox = driver.FindElementByAccessibilityId("days");
            sdTextBox.SendKeys("5");
            var rtTextBox = driver.FindElementByAccessibilityId("category");
            rtTextBox.SendKeys("3");
            var paTextBox = driver.FindElementByAccessibilityId("capacity");
            paTextBox.SendKeys("2");
            var vTextBox = driver.FindElementByAccessibilityId("safe");
            vTextBox.SendKeys("0");
            var bfastTextBox = driver.FindElementByAccessibilityId("breakfast");
            bfastTextBox.SendKeys("1");
            driver.FindElementByAccessibilityId("button_calc").Click();
            var answer = driver.FindElementByAccessibilityId("label_sum").Text;
            Assert.AreEqual(answer, "9500");
        }
    }
}
