using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;

namespace Tests
{
    [TestClass]
    public class Test1
    {
        private const string DriverUrl = "http://127.0.0.1:4723/";
        private const string AppPath = @".\UI.exe";
        private WindowsDriver<WindowsElement> driver;

        [TestInitialize]
        public void Setup()
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", AppPath);
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");

            driver = new WindowsDriver<WindowsElement>(new Uri(DriverUrl), appCapabilities);
        }

        private string Submit(string days, string category, string capacity, string safe, string breakfast)
        {
            driver.FindElementByAccessibilityId("days").SendKeys(days);
            driver.FindElementByAccessibilityId("category").SendKeys(category);
            driver.FindElementByAccessibilityId("capacity").SendKeys(capacity);
            driver.FindElementByAccessibilityId("safe").SendKeys(safe);
            driver.FindElementByAccessibilityId("breakfast").SendKeys(breakfast);

            driver.FindElementByAccessibilityId("calcBtn").Click();

            return driver.FindElementByAccessibilityId("tbTotal").Text;
        }


        [TestMethod]
        [DataRow("1", "1", "1", "да", "да", 11111)]
        [DataRow("2", "1", "1", "да", "нет", 21110)]
        [DataRow("1", "0", "3", "нет", "да", 10301)]
        [DataRow("0", "0", "3", "да", "да", 311)]
        [DataRow("0", "3", "3", "да", "да", 3311)]
        [DataRow("7", "3", "3", "да", "да", 73311)]
        [DataRow("2", "2", "0", "нет", "да", 22001)]
        [DataRow("0", "0", "0", "нет", "нет", 0)]
        public void CorrectInput(string days, string category, string capacity, string safe, string breakfast, int expected)
        {
            Assert.AreEqual(expected.ToString(), Submit(days, category, capacity, safe, breakfast));
        }


        [TestMethod]
        [DataRow("а", "1", "1", "да", "да")]
        [DataRow("2", "б", "1", "да", "нет")]
        [DataRow("1", "0", "в", "нет", "да")]
        [DataRow("0", "0", "5", "г", "да")]
        [DataRow("0", "5", "5", "да", "д")]
        public void InvalidInput(string days, string category, string capacity, string safe, string breakfast)
        {
            Assert.AreEqual("", Submit(days, category, capacity, safe, breakfast));
        }

        [TestCleanup]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
