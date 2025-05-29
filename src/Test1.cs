using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace TestProject1
{
    [TestClass]
    public sealed class Tests
    {
        public const string DriveURL = "http://127.0.0.1:4723/";
        public WindowsDriver<WindowsElement> driver;

        [TestInitialize]
        public void Setup()
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", @"C:\Users\User\source\repos\WinFormsApp2\WinFormsApp2\bin\Debug\net8.0-windows\WinFormsApp2.exe");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

            driver = new WindowsDriver<WindowsElement>(new Uri(DriveURL), appCapabilities);
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
        public void TestBasicCalculation_Category1Size1_NoExtras()
        {
            var tbDays = driver.FindElementByAccessibilityId("tbDays");
            var tbCategory = driver.FindElementByAccessibilityId("tbCategory");
            var tbSize = driver.FindElementByAccessibilityId("tbSize");
            var tbSafe = driver.FindElementByAccessibilityId("tbSafe");
            var tbBreakfast = driver.FindElementByAccessibilityId("tbBreakfast");
            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            var tbSum = driver.FindElementByAccessibilityId("tbSum");

            tbDays.Clear();
            tbDays.SendKeys("2");
            tbCategory.Clear();
            tbCategory.SendKeys("1");
            tbSize.Clear();
            tbSize.SendKeys("1");
            tbSafe.Clear();
            tbSafe.SendKeys("нет");
            tbBreakfast.Clear();
            tbBreakfast.SendKeys("нет");
            btnCalculate.Click();

            Assert.AreEqual("2000.00", tbSum.Text);
        }

        [TestMethod]
        public void TestWithSafe_Category2Size2_3Days()
        {
            var tbDays = driver.FindElementByAccessibilityId("tbDays");
            var tbCategory = driver.FindElementByAccessibilityId("tbCategory");
            var tbSize = driver.FindElementByAccessibilityId("tbSize");
            var tbSafe = driver.FindElementByAccessibilityId("tbSafe");
            var tbBreakfast = driver.FindElementByAccessibilityId("tbBreakfast");
            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            var tbSum = driver.FindElementByAccessibilityId("tbSum");

            tbDays.Clear();
            tbDays.SendKeys("3");
            tbCategory.Clear();
            tbCategory.SendKeys("2");
            tbSize.Clear();
            tbSize.SendKeys("2");
            tbSafe.Clear();
            tbSafe.SendKeys("да");
            tbBreakfast.Clear();
            tbBreakfast.SendKeys("нет");
            btnCalculate.Click();

            Assert.AreEqual("11250.00", tbSum.Text);
        }

        [TestMethod]
        public void TestWithBreakfast_Category3Size1_5Days()
        {
            var tbDays = driver.FindElementByAccessibilityId("tbDays");
            var tbCategory = driver.FindElementByAccessibilityId("tbCategory");
            var tbSize = driver.FindElementByAccessibilityId("tbSize");
            var tbSafe = driver.FindElementByAccessibilityId("tbSafe");
            var tbBreakfast = driver.FindElementByAccessibilityId("tbBreakfast");
            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            var tbSum = driver.FindElementByAccessibilityId("tbSum");

            tbDays.Clear();
            tbDays.SendKeys("5");
            tbCategory.Clear();
            tbCategory.SendKeys("3");
            tbSize.Clear();
            tbSize.SendKeys("1");
            tbSafe.Clear();
            tbSafe.SendKeys("нет");
            tbBreakfast.Clear();
            tbBreakfast.SendKeys("да");
            btnCalculate.Click();

            Assert.AreEqual("11000.00", tbSum.Text);
        }

        [TestMethod]
        public void TestAllExtras_Category1Size3_1Day()
        {
            var tbDays = driver.FindElementByAccessibilityId("tbDays");
            var tbCategory = driver.FindElementByAccessibilityId("tbCategory");
            var tbSize = driver.FindElementByAccessibilityId("tbSize");
            var tbSafe = driver.FindElementByAccessibilityId("tbSafe");
            var tbBreakfast = driver.FindElementByAccessibilityId("tbBreakfast");
            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            var tbSum = driver.FindElementByAccessibilityId("tbSum");

            tbDays.Clear();
            tbDays.SendKeys("1");
            tbCategory.Clear();
            tbCategory.SendKeys("1");
            tbSize.Clear();
            tbSize.SendKeys("3");
            tbSafe.Clear();
            tbSafe.SendKeys("да");
            tbBreakfast.Clear();
            tbBreakfast.SendKeys("да");
            btnCalculate.Click();

            Assert.AreEqual("2750.00", tbSum.Text);
        }

        [TestMethod]
        public void TestInvalidDaysInput_ShouldNotCalculate()
        {
            var tbDays = driver.FindElementByAccessibilityId("tbDays");
            var tbCategory = driver.FindElementByAccessibilityId("tbCategory");
            var tbSize = driver.FindElementByAccessibilityId("tbSize");
            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            var tbSum = driver.FindElementByAccessibilityId("tbSum");

            tbDays.Clear();
            tbDays.SendKeys("0");
            tbCategory.Clear();
            tbCategory.SendKeys("1");
            tbSize.Clear();
            tbSize.SendKeys("1");
            btnCalculate.Click();

            Assert.AreEqual("", tbSum.Text);
        }

        [TestMethod]
        public void TestInvalidCategoryInput_ShouldNotCalculate()
        {
            var tbDays = driver.FindElementByAccessibilityId("tbDays");
            var tbCategory = driver.FindElementByAccessibilityId("tbCategory");
            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            var tbSum = driver.FindElementByAccessibilityId("tbSum");

            tbDays.Clear();
            tbDays.SendKeys("2");
            tbCategory.Clear();
            tbCategory.SendKeys("4");
            btnCalculate.Click();

            Assert.AreEqual("", tbSum.Text);
        }

        [TestMethod]
        public void TestComplexCalculation_Category2Size3_WithAllExtras()
        {
            var tbDays = driver.FindElementByAccessibilityId("tbDays");
            var tbCategory = driver.FindElementByAccessibilityId("tbCategory");
            var tbSize = driver.FindElementByAccessibilityId("tbSize");
            var tbSafe = driver.FindElementByAccessibilityId("tbSafe");
            var tbBreakfast = driver.FindElementByAccessibilityId("tbBreakfast");
            var btnCalculate = driver.FindElementByAccessibilityId("btnCalculate");
            var tbSum = driver.FindElementByAccessibilityId("tbSum");

            tbDays.Clear();
            tbDays.SendKeys("4");
            tbCategory.Clear();
            tbCategory.SendKeys("2");
            tbSize.Clear();
            tbSize.SendKeys("3");
            tbSafe.Clear();
            tbSafe.SendKeys("да");
            tbBreakfast.Clear();
            tbBreakfast.SendKeys("да");
            btnCalculate.Click();

            Assert.AreEqual("22000.00", tbSum.Text);
        }
    }
}