using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using System.Runtime.CompilerServices;

namespace HotelCalculatorTests
{
    [TestClass]
    public class Test1
    {
        public const string DriveURL = "http://127.0.0.1:4723/";
        public WindowsDriver<WindowsElement> driver;
        WindowsElement[] inputs;
        WindowsElement output;
        WindowsElement calculateButton;

        void ClearAllFields()
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i].Clear();
            }
        }

        string Execution(string[] inputs)   //{ days, category, capacity, safe, breakfast }
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                this.inputs[i].SendKeys(inputs[i]);
            }

            calculateButton.Click();

            return output.Text;
        }

        [TestInitialize]
        public void Setup()
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", @"D:\Testing\ST-9\HotelCalculator\HotelCalculator\bin\Debug\HotelCalculator.exe");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

            driver = new WindowsDriver<WindowsElement>(new Uri(DriveURL), appCapabilities);

            inputs = new WindowsElement[] { driver.FindElementByAccessibilityId("txtDays"), 
                driver.FindElementByAccessibilityId("txtCategory"), driver.FindElementByAccessibilityId("txtCapacity"), 
                driver.FindElementByAccessibilityId("txtSafe"), driver.FindElementByAccessibilityId("txtBreakfast") };
            output = driver.FindElementByAccessibilityId("txtTotal");
            calculateButton = driver.FindElementByAccessibilityId("btnCalculate");
        }

        [TestMethod]
        public void Test01_OneGuestWithSafeAndBreakfasts()
        {
            string res = Execution(new string[] { "1", "1", "1", "да", "да" });
            Assert.AreEqual("1300", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "1", "1", "да", "да" });
            Assert.AreEqual("3900", res);

            ClearAllFields();

            res = Execution(new string[] { "1", "2", "1", "да", "да" });
            Assert.AreEqual("1100", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "2", "1", "да", "да" });
            Assert.AreEqual("3300", res);

            ClearAllFields();

            res = Execution(new string[] { "1", "3", "1", "да", "да" });
            Assert.AreEqual("800", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "3", "1", "да", "да" });
            Assert.AreEqual("2400", res);
        }

        [TestMethod]
        public void Test02_OneGuestWithSafeWithoutBreakfasts()
        {
            string res = Execution(new string[] { "1", "1", "1", "да", "нет" });
            Assert.AreEqual("1100", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "1", "1", "да", "нет" });
            Assert.AreEqual("3300", res);

            ClearAllFields();

            res = Execution(new string[] { "1", "2", "1", "да", "нет" });
            Assert.AreEqual("900", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "2", "1", "да", "нет" });
            Assert.AreEqual("2700", res);

            ClearAllFields();

            res = Execution(new string[] { "1", "3", "1", "да", "нет" });
            Assert.AreEqual("600", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "3", "1", "да", "нет" });
            Assert.AreEqual("1800", res);
        }

        [TestMethod]
        public void Test03_OneGuestWithBreakfastsWithoutSafe()
        {
            string res = Execution(new string[] { "1", "1", "1", "нет", "да" });
            Assert.AreEqual("1200", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "1", "1", "нет", "да" });
            Assert.AreEqual("3600", res);

            ClearAllFields();

            res = Execution(new string[] { "1", "2", "1", "нет", "да" });
            Assert.AreEqual("1000", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "2", "1", "нет", "да" });
            Assert.AreEqual("3000", res);

            ClearAllFields();

            res = Execution(new string[] { "1", "3", "1", "нет", "да" });
            Assert.AreEqual("700", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "3", "1", "нет", "да" });
            Assert.AreEqual("2100", res);
        }

        [TestMethod]
        public void Test04_OneGuestWithoutSafeAndBreakfasts()
        {
            string res = Execution(new string[] { "1", "1", "1", "нет", "нет" });
            Assert.AreEqual("1000", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "1", "1", "нет", "нет" });
            Assert.AreEqual("3000", res);

            ClearAllFields();

            res = Execution(new string[] { "1", "2", "1", "нет", "нет" });
            Assert.AreEqual("800", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "2", "1", "нет", "нет" });
            Assert.AreEqual("2400", res);

            ClearAllFields();

            res = Execution(new string[] { "1", "3", "1", "нет", "нет" });
            Assert.AreEqual("500", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "3", "1", "нет", "нет" });
            Assert.AreEqual("1500", res);
        }

        [TestMethod]
        public void Test05_TwoGuestsWithSafeAndBreakfasts()
        {
            string res = Execution(new string[] { "1", "1", "2", "да", "да" });
            Assert.AreEqual("2000", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "1", "2", "да", "да" });
            Assert.AreEqual("6000", res);

            ClearAllFields();

            res = Execution(new string[] { "1", "2", "2", "да", "да" });
            Assert.AreEqual("1700", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "2", "2", "да", "да" });
            Assert.AreEqual("5100", res);

            ClearAllFields();

            res = Execution(new string[] { "1", "3", "2", "да", "да" });
            Assert.AreEqual("1300", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "3", "2", "да", "да" });
            Assert.AreEqual("3900", res);
        }

        [TestMethod]
        public void Test06_TwoGuestsWithSafeWithoutBreakfasts()
        {
            string res = Execution(new string[] { "1", "1", "2", "да", "нет" });
            Assert.AreEqual("1600", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "1", "2", "да", "нет" });
            Assert.AreEqual("4800", res);

            ClearAllFields();

            res = Execution(new string[] { "1", "2", "2", "да", "нет" });
            Assert.AreEqual("1300", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "2", "2", "да", "нет" });
            Assert.AreEqual("3900", res);

            ClearAllFields();

            res = Execution(new string[] { "1", "3", "2", "да", "нет" });
            Assert.AreEqual("900", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "3", "2", "да", "нет" });
            Assert.AreEqual("2700", res );
        }

        [TestMethod]
        public void Test07_TwoGuestsWithBreakfastsWithoutSafe()
        {
            string res = Execution(new string[] { "1", "1", "2", "нет", "да" });
            Assert.AreEqual("1900", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "1", "2", "нет", "да" });
            Assert.AreEqual("5700", res);

            ClearAllFields();

            res = Execution(new string[] { "1", "2", "2", "нет", "да" });
            Assert.AreEqual("1600", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "2", "2", "нет", "да" });
            Assert.AreEqual("4800", res);

            ClearAllFields();

            res = Execution(new string[] { "1", "3", "2", "нет", "да" });
            Assert.AreEqual("1200", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "3", "2", "нет", "да" });
            Assert.AreEqual("3600", res);
        }

        [TestMethod]
        public void Test08_TwoGuestsWithoutSafeAndBreakfasts()
        {
            string res = Execution(new string[] { "1", "1", "2", "нет", "нет" });
            Assert.AreEqual("1500", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "1", "2", "нет", "нет" });
            Assert.AreEqual("4500", res);

            ClearAllFields();

            res = Execution(new string[] { "1", "2", "2", "нет", "нет" });
            Assert.AreEqual("1200", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "2", "2", "нет", "нет" });
            Assert.AreEqual("3600", res);

            ClearAllFields();

            res = Execution(new string[] { "1", "3", "2", "нет", "нет" });
            Assert.AreEqual("800", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "3", "2", "нет", "нет" });
            Assert.AreEqual("2400", res);
        }

        [TestMethod]
        public void Test09_ThreeGuestsWithSafeAndBreakfasts()
        {
            string res = Execution(new string[] { "1", "1", "3", "да", "да" });
            Assert.AreEqual("2700", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "1", "3", "да", "да" });
            Assert.AreEqual("8100", res);

            ClearAllFields();

            res = Execution(new string[] { "1", "2", "3", "да", "да" });
            Assert.AreEqual("2300", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "2", "3", "да", "да" });
            Assert.AreEqual("6900", res);

            ClearAllFields();

            res = Execution(new string[] { "1", "3", "3", "да", "да" });
            Assert.AreEqual("1800", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "3", "3", "да", "да" });
            Assert.AreEqual("5400", res);
        }

        [TestMethod]
        public void Test10_ThreeGuestsWithSafeWithoutBreakfasts()
        {
            string res = Execution(new string[] { "1", "1", "3", "да", "нет" });
            Assert.AreEqual("2100", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "1", "3", "да", "нет" });
            Assert.AreEqual("6300", res);

            ClearAllFields();

            res = Execution(new string[] { "1", "2", "3", "да", "нет" });
            Assert.AreEqual("1700", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "2", "3", "да", "нет" });
            Assert.AreEqual("5100", res);

            ClearAllFields();

            res = Execution(new string[] { "1", "3", "3", "да", "нет" });
            Assert.AreEqual("1200", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "3", "3", "да", "нет" });
            Assert.AreEqual("3600", res);
        }

        [TestMethod]
        public void Test11_ThreeGuestsWithBreakfastsWithoutSafe()
        {
            string res = Execution(new string[] { "1", "1", "3", "нет", "да" });
            Assert.AreEqual("2600", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "1", "3", "нет", "да" });
            Assert.AreEqual("7800", res);

            ClearAllFields();

            res = Execution(new string[] { "1", "2", "3", "нет", "да" });
            Assert.AreEqual("2200", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "2", "3", "нет", "да" });
            Assert.AreEqual("6600", res);

            ClearAllFields();

            res = Execution(new string[] { "1", "3", "3", "нет", "да" });
            Assert.AreEqual("1700", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "3", "3", "нет", "да" });
            Assert.AreEqual("5100", res);
        }

        [TestMethod]
        public void Test12_ThreeGuestsWithoutSafeAndBreakfasts()
        {
            string res = Execution(new string[] { "1", "1", "3", "нет", "нет" });
            Assert.AreEqual("2000", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "1", "3", "нет", "нет" });
            Assert.AreEqual("6000", res);

            ClearAllFields();

            res = Execution(new string[] { "1", "2", "3", "нет", "нет" });
            Assert.AreEqual("1600", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "2", "3", "нет", "нет" });
            Assert.AreEqual("4800", res);

            ClearAllFields();

            res = Execution(new string[] { "1", "3", "3", "нет", "нет" });
            Assert.AreEqual("1100", res);

            ClearAllFields();

            res = Execution(new string[] { "3", "3", "3", "нет", "нет" });
            Assert.AreEqual("3300", res);
        }

        [TestCleanup]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}