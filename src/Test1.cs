using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;

namespace HotelTestProject {
    [TestClass]
    public class Tests {
        const string DriveURL = "http://127.0.0.1:4723/";
        WindowsDriver<WindowsElement> driver;
        string appPath = @"HotelCalculator\bin\Debug\net8.0-windows\HotelCalculator.exe";

        [TestInitialize]
        public void Setup() {
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", appPath);
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "1");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

            driver = new WindowsDriver<WindowsElement>(new Uri(DriveURL), appCapabilities);
        }

        [TestCleanup]
        public void Cleanup() { driver?.Quit(); }

        private void Fill(string days = "1", string category = "эконом", string capacity = "1", string safe = "нет", string breakfast = "нет") {
            driver.FindElementByAccessibilityId("textBoxDays").Clear();
            driver.FindElementByAccessibilityId("textBoxDays").SendKeys(days);

            driver.FindElementByAccessibilityId("textBoxCat").Clear();
            driver.FindElementByAccessibilityId("textBoxCat").SendKeys(category);

            driver.FindElementByAccessibilityId("textBoxCapacity").Clear();
            driver.FindElementByAccessibilityId("textBoxCapacity").SendKeys(capacity);

            driver.FindElementByAccessibilityId("textBoxSafe").Clear();
            driver.FindElementByAccessibilityId("textBoxSafe").SendKeys(safe);

            driver.FindElementByAccessibilityId("textBoxBreakfast").Clear();
            driver.FindElementByAccessibilityId("textBoxBreakfast").SendKeys(breakfast);

            driver.FindElementByAccessibilityId("button1").Click();
        }

        [TestMethod]
        public void Test_SuccessLowcost() {
            Fill();
            string result = driver.FindElementByAccessibilityId("textBoxSum").Text;
            Assert.AreEqual("100,00", result);
        }

        [TestMethod]
        public void Test_SuccessExpensive() {
            Fill("10", "люкс", "3", "да", "да");
            string result = driver.FindElementByAccessibilityId("textBoxSum").Text;
            Assert.AreEqual("9250,00", result);
        }

        [TestMethod]
        public void Test_Empty() {
            Fill("", "", "", "", "");
            string result = driver.FindElementByAccessibilityId("textBoxSum").Text;
            Assert.AreEqual("ОШИБКА", result.Substring(0,6));
        }

        [TestMethod]
        public void Test_ZeroDays() {
            Fill(days: "0");
            string result = driver.FindElementByAccessibilityId("textBoxSum").Text;
            Assert.AreEqual("ОШИБКА ДНЕЙ", result);
        }

        [TestMethod]
        public void Test_NegativeDays() {
            Fill(days: "-1");
            string result = driver.FindElementByAccessibilityId("textBoxSum").Text;
            Assert.AreEqual("ОШИБКА ДНЕЙ", result);
        }

        [TestMethod]
        public void Test_WrongCategory() {
            Fill(category: "guhhh");
            string result = driver.FindElementByAccessibilityId("textBoxSum").Text;
            Assert.AreEqual("ОШИБКА КАТЕГОРИИ", result);
        }

        [TestMethod]
        public void Test_ZeroCapacity() {
            Fill(capacity: "0");
            string result = driver.FindElementByAccessibilityId("textBoxSum").Text;
            Assert.AreEqual("ОШИБКА ВМЕСТИМОСТИ", result);
        }

        [TestMethod]
        public void Test_NegativeCapacity() {
            Fill(capacity: "-1");
            string result = driver.FindElementByAccessibilityId("textBoxSum").Text;
            Assert.AreEqual("ОШИБКА ВМЕСТИМОСТИ", result);
        }

        [TestMethod]
        public void Test_TooMuchCapacity() {
            Fill(capacity: "10");
            string result = driver.FindElementByAccessibilityId("textBoxSum").Text;
            Assert.AreEqual("ОШИБКА ВМЕСТИМОСТИ", result);
        }

        [TestMethod]
        public void Test_WrongSafe() {
            Fill(safe: "guhhh");
            string result = driver.FindElementByAccessibilityId("textBoxSum").Text;
            Assert.AreEqual("ОШИБКА СЕЙФА", result);
        }

        [TestMethod]
        public void Test_WrongBreakfast() {
            Fill(breakfast: "guhhh");
            string result = driver.FindElementByAccessibilityId("textBoxSum").Text;
            Assert.AreEqual("ОШИБКА ЗАВТРАКА", result);
        }

    }
}