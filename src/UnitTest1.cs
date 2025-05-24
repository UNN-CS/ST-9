using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;

namespace HotelCalculatorTests
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
            appCapabilities.AddAdditionalCapability("app", @"C:\Projects\Hotel Calculator\Hotel Calculator\bin\Debug\net6.0-windows\Hotel Calculator.exe");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

            driver = new WindowsDriver<WindowsElement>(new Uri(DriveURL), appCapabilities);
        }
        [TestMethod]
        public void TestOneGuest()
        {
            var sdTextBox = driver.FindElementByAccessibilityId("StayingDaysTextBox");
            sdTextBox.SendKeys("14");
            var rtTextBox = driver.FindElementByAccessibilityId("RoomTypeTextBox");
            rtTextBox.SendKeys("3");
            var paTextBox = driver.FindElementByAccessibilityId("PersonsAmountTextBox");
            paTextBox.SendKeys("1");
            var vTextBox = driver.FindElementByAccessibilityId("VaultTextBox");
            vTextBox.SendKeys("n");
            var bfastTextBox = driver.FindElementByAccessibilityId("BreakfastTextBox");
            bfastTextBox.SendKeys("n");
            driver.FindElementByAccessibilityId("CalculateButton").Click();
            var answer = driver.FindElementByAccessibilityId("SumTextBox").Text;
            Assert.AreEqual(answer, "102200");
        }
        [TestMethod]
      public void TestTwoGuests(){
            var sdTextBox = driver.FindElementByAccessibilityId("StayingDaysTextBox");
            sdTextBox.SendKeys("5");
            var rtTextBox = driver.FindElementByAccessibilityId("RoomTypeTextBox");
            rtTextBox.SendKeys("1");
            var paTextBox = driver.FindElementByAccessibilityId("PersonsAmountTextBox");
            paTextBox.SendKeys("2");
            var vTextBox = driver.FindElementByAccessibilityId("VaultTextBox");
            vTextBox.SendKeys("n");
            var bfastTextBox = driver.FindElementByAccessibilityId("BreakfastTextBox");
            bfastTextBox.SendKeys("n");
            driver.FindElementByAccessibilityId("CalculateButton").Click();
            var answer = driver.FindElementByAccessibilityId("SumTextBox").Text;
            Assert.AreEqual(answer, "13500");
        }
        [TestMethod]
        public void TestThreeGuests()
        {
            var sdTextBox = driver.FindElementByAccessibilityId("StayingDaysTextBox");
            sdTextBox.SendKeys("7");
            var rtTextBox = driver.FindElementByAccessibilityId("RoomTypeTextBox");
            rtTextBox.SendKeys("2");
            var paTextBox = driver.FindElementByAccessibilityId("PersonsAmountTextBox");
            paTextBox.SendKeys("3");
            var vTextBox = driver.FindElementByAccessibilityId("VaultTextBox");
            vTextBox.SendKeys("n");
            var bfastTextBox = driver.FindElementByAccessibilityId("BreakfastTextBox");
            bfastTextBox.SendKeys("y");
            driver.FindElementByAccessibilityId("CalculateButton").Click();
            var answer = driver.FindElementByAccessibilityId("SumTextBox").Text;
            Assert.AreEqual(answer, "31200");
        }
        [TestMethod]
        public void TestALLInclusive()
        {
            var sdTextBox = driver.FindElementByAccessibilityId("StayingDaysTextBox");
            sdTextBox.SendKeys("10");
            var rtTextBox = driver.FindElementByAccessibilityId("RoomTypeTextBox");
            rtTextBox.SendKeys("2");
            var paTextBox = driver.FindElementByAccessibilityId("PersonsAmountTextBox");
            paTextBox.SendKeys("3");
            var vTextBox = driver.FindElementByAccessibilityId("VaultTextBox");
            vTextBox.SendKeys("y");
            var bfastTextBox = driver.FindElementByAccessibilityId("BreakfastTextBox");
            bfastTextBox.SendKeys("y");
            driver.FindElementByAccessibilityId("CalculateButton").Click();
            var answer = driver.FindElementByAccessibilityId("SumTextBox").Text;
            Assert.AreEqual(answer, "43700");
        }
        [TestMethod]
        public void TestMinCost()
        {
            var sdTextBox = driver.FindElementByAccessibilityId("StayingDaysTextBox");
            sdTextBox.SendKeys("1");
            var rtTextBox = driver.FindElementByAccessibilityId("RoomTypeTextBox");
            rtTextBox.SendKeys("1");
            var paTextBox = driver.FindElementByAccessibilityId("PersonsAmountTextBox");
            paTextBox.SendKeys("1");
            var vTextBox = driver.FindElementByAccessibilityId("VaultTextBox");
            vTextBox.SendKeys("n");
            var bfastTextBox = driver.FindElementByAccessibilityId("BreakfastTextBox");
            bfastTextBox.SendKeys("n");
            driver.FindElementByAccessibilityId("CalculateButton").Click();
            var answer = driver.FindElementByAccessibilityId("SumTextBox").Text;
            Assert.AreEqual(answer, "2500");
        }
    }
}