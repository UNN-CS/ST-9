using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace TestProject1
{
    [TestClass]
    [DoNotParallelize]
    public sealed class Test1
    {
        public const string DriveURL = "http://127.0.0.1:4723/";
        public const string AppPath = @"C:\Main\Dev\ST\ST-9\src\bin\Debug\net8.0-windows\HotelCalculator.exe";
        public WindowsDriver<WindowsElement> driver;

        [TestInitialize]
        public void Setup()
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", AppPath);
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

            driver = new WindowsDriver<WindowsElement>(new Uri(DriveURL), appCapabilities);

            driver.FindElementByAccessibilityId("textBox_days").Clear();
            driver.FindElementByAccessibilityId("textBox_category").Clear();
            driver.FindElementByAccessibilityId("textBox_capacity").Clear();
            driver.FindElementByAccessibilityId("textBox_safe").Clear();
            driver.FindElementByAccessibilityId("textBox_breakfast").Clear();
            driver.FindElementByAccessibilityId("textBox_sum").Clear();
        }

        [TestMethod]
        public void TestMethod_standard()
        {
            driver.FindElementByAccessibilityId("textBox_days").SendKeys("1");
            driver.FindElementByAccessibilityId("textBox_category").SendKeys("2");
            driver.FindElementByAccessibilityId("textBox_capacity").SendKeys("3");
            driver.FindElementByAccessibilityId("textBox_safe").SendKeys("нет");
            driver.FindElementByAccessibilityId("textBox_breakfast").SendKeys("нет");
            driver.FindElementByAccessibilityId("button1").Click();

            // 200*3*1 + 0 + 0 = 600
            var sum = driver.FindElementByAccessibilityId("textBox_sum").Text;
            Assert.AreEqual("600", sum);
        }

        [TestMethod]
        public void TestMethod_safe()
        {
            driver.FindElementByAccessibilityId("textBox_days").SendKeys("1");
            driver.FindElementByAccessibilityId("textBox_category").SendKeys("2");
            driver.FindElementByAccessibilityId("textBox_capacity").SendKeys("3");
            driver.FindElementByAccessibilityId("textBox_safe").SendKeys("да");
            driver.FindElementByAccessibilityId("textBox_breakfast").SendKeys("нет");
            driver.FindElementByAccessibilityId("button1").Click();

            // 200*3*1 + 10 + 0 = 610
            var sum = driver.FindElementByAccessibilityId("textBox_sum").Text;
            Assert.AreEqual("610", sum);
        }

        [TestMethod]
        public void TestMethod_breakfast()
        {
            driver.FindElementByAccessibilityId("textBox_days").SendKeys("1");
            driver.FindElementByAccessibilityId("textBox_category").SendKeys("2");
            driver.FindElementByAccessibilityId("textBox_capacity").SendKeys("3");
            driver.FindElementByAccessibilityId("textBox_safe").SendKeys("нет");
            driver.FindElementByAccessibilityId("textBox_breakfast").SendKeys("да");
            driver.FindElementByAccessibilityId("button1").Click();

            // 200*3*1 + 0 + 20*3*1 = 660
            var sum = driver.FindElementByAccessibilityId("textBox_sum").Text;
            Assert.AreEqual("660", sum);
        }
        
        [TestMethod]
        public void TestMethod_days_incorrect()
        {
            driver.FindElementByAccessibilityId("textBox_days").SendKeys("-1");
            driver.FindElementByAccessibilityId("textBox_category").SendKeys("2");
            driver.FindElementByAccessibilityId("textBox_capacity").SendKeys("3");
            driver.FindElementByAccessibilityId("textBox_safe").SendKeys("нет");
            driver.FindElementByAccessibilityId("textBox_breakfast").SendKeys("нет");
            driver.FindElementByAccessibilityId("button1").Click();

            WindowsElement errorWindow = driver.FindElementByName("Ошибка");
            Assert.IsNotNull(errorWindow);
            var ok = driver.FindElementByName("ОК");
            ok.Click();
        }

        [TestMethod]
        public void TestMethod_category_incorrect()
        {
            driver.FindElementByAccessibilityId("textBox_days").SendKeys("1");
            driver.FindElementByAccessibilityId("textBox_category").SendKeys("10000");
            driver.FindElementByAccessibilityId("textBox_capacity").SendKeys("3");
            driver.FindElementByAccessibilityId("textBox_safe").SendKeys("нет");
            driver.FindElementByAccessibilityId("textBox_breakfast").SendKeys("нет");
            driver.FindElementByAccessibilityId("button1").Click();

            WindowsElement errorWindow = driver.FindElementByName("Ошибка");
            Assert.IsNotNull(errorWindow);
            var ok = driver.FindElementByName("ОК");
            ok.Click();
        }

        [TestMethod]
        public void TestMethod_capacity_incorrect()
        {
            driver.FindElementByAccessibilityId("textBox_days").SendKeys("1");
            driver.FindElementByAccessibilityId("textBox_category").SendKeys("2");
            driver.FindElementByAccessibilityId("textBox_capacity").SendKeys("0");
            driver.FindElementByAccessibilityId("textBox_safe").SendKeys("нет");
            driver.FindElementByAccessibilityId("textBox_breakfast").SendKeys("нет");
            driver.FindElementByAccessibilityId("button1").Click();

            WindowsElement errorWindow = driver.FindElementByName("Ошибка");
            Assert.IsNotNull(errorWindow);
            var ok = driver.FindElementByName("ОК");
            ok.Click();
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (driver != null)
            {
                try { driver.CloseApp(); } catch { }
                try { driver.Quit(); } catch { }
                driver = null;
            }
        }
    }
}
