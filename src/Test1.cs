using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;

namespace Test1
{
    [TestClass]
    public class UnitTest1
    {
        public WindowsDriver<WindowsElement> driver;

        [TestInitialize]
        public void Setup()
        {
            var app = new AppiumOptions();
            app.AddAdditionalCapability("app", @"C:\Users\Liza\ST-9\ST-9\bin\Debug\net8.0-windows\ST-9.exe");
            app.AddAdditionalCapability("deviceName", "WindowsPC");
            driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), app);
        }

        [TestMethod]
        public void TestMethod1()
        {
            test("1", "1", "1", "   ", "   ", "4000");
        }

        [TestMethod]
        public void TestMethod2()
        {
            test("3", "1", "1", "   ", "  ", "15000");
        }
        [TestMethod]
        public void TestMethod3()
        {
            test("3", "2", "2", "  ", "   ", "63000");
        }
        [TestMethod]
        public void TestMethod4()
        {
            test("1", "3", "3", "  ", "  ", "94000");
        }
        [TestMethod]
        public void TestMethod5()
        {
            test("2", "2", "2", "   ", "   ", "40000");
        }
        [TestMethod]
        public void TestMethod6()
        {
            test("1", "3", "2", "  ", "  ", "63000");
        }

        private void test(string countDay, string category, string people, string safe, string breakfast, string summa)
        {
            driver.FindElementByAccessibilityId("countDay").SendKeys(countDay);
            driver.FindElementByAccessibilityId("category").SendKeys(category);
            driver.FindElementByAccessibilityId("people").SendKeys(people);
            driver.FindElementByAccessibilityId("safe").SendKeys(safe);
            driver.FindElementByAccessibilityId("breakfast").SendKeys(breakfast);
            driver.FindElementByAccessibilityId("button").Click();
            
            var sum = driver.FindElementByAccessibilityId("summa");
            Assert.AreEqual(sum.Text, summa);
        }

    }
}