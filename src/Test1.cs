using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;


namespace TestProject1
{
    [TestClass]
  
    public sealed class Test1
    {
        public const string DriveURL = "http://127.0.0.1:4723/";
        public WindowsDriver<WindowsElement> driver;

        private string days_name = "days";
        private string room_name = "typeOfRoom";
        private string beds_name = "beds";
        private string safe_name = "safe";
        private string breakfast_name = "breakfast";
        private string price_name = "price";
        private string button_name = "button1";

        [TestInitialize]
        public void Setup()
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", @"D:\study\6semester\testirov\lab9\ST-9\WinForms\bin\Debug\net8.0-windows\WinForms.exe");
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
                driver = null;
            }
        }
        
        [TestMethod]
        public void TestMethod1()
        {
            var tbX = driver.FindElementByAccessibilityId(room_name);
            tbX.SendKeys(Keys.Down);
            tbX.SendKeys(Keys.Down);
            var btnSum = driver.FindElementByAccessibilityId(button_name);
            btnSum.Click();
            var tbZ = driver.FindElementByAccessibilityId(price_name);
            Assert.AreEqual(tbZ.Text, "700");
        }




        [TestMethod]
        public void TestMethod2()
        {
            var tbDays = driver.FindElementByAccessibilityId(days_name);
            tbDays.SendKeys("1");

            var tbX = driver.FindElementByAccessibilityId(room_name);
            tbX.SendKeys(Keys.Down); 

            var btnSum = driver.FindElementByAccessibilityId(button_name);
            btnSum.Click(); 

            var tbZ = driver.FindElementByAccessibilityId(price_name);
            Assert.AreEqual(tbZ.Text, ((int)(700*0.7*11)).ToString()); 
        }

        [TestMethod]
        public void TestMethod3()
        {
            var tbDays = driver.FindElementByAccessibilityId(days_name);
            tbDays.SendKeys(Keys.Up);

            var tbX = driver.FindElementByAccessibilityId(room_name);
            tbX.SendKeys(Keys.Down);
            tbX.SendKeys(Keys.Down);
            tbX.SendKeys(Keys.Down);

            var tbB = driver.FindElementByAccessibilityId(beds_name);
            tbB.SendKeys(Keys.Down);
            tbB.SendKeys(Keys.Down);

            var cbBreakfast = driver.FindElementByAccessibilityId(breakfast_name);
            cbBreakfast.Click();

            var btnSum = driver.FindElementByAccessibilityId(button_name);
            btnSum.Click();

            var tbZ = driver.FindElementByAccessibilityId(price_name);
            Assert.AreEqual(tbZ.Text, "5100");
        }

        [TestMethod]
        public void TestMethod4()
        {
            var tbDays = driver.FindElementByAccessibilityId(days_name);
            tbDays.SendKeys(Keys.Up);
            tbDays.SendKeys(Keys.Up);


            var tbX = driver.FindElementByAccessibilityId(room_name);
            tbX.SendKeys(Keys.Down);
            tbX.SendKeys(Keys.Down);
            tbX.SendKeys(Keys.Down);
            tbX.SendKeys(Keys.Down); 


            var tbB = driver.FindElementByAccessibilityId(beds_name);
            tbB.SendKeys(Keys.Down);
            tbB.SendKeys(Keys.Down);

            var cbBreakfast = driver.FindElementByAccessibilityId(breakfast_name);
            cbBreakfast.Click(); 

            var cbSafe = driver.FindElementByAccessibilityId(safe_name);
            cbSafe.Click(); 

            var btnSum = driver.FindElementByAccessibilityId(button_name);
            btnSum.Click();

            var tbZ = driver.FindElementByAccessibilityId(price_name);
            
            Assert.AreEqual(tbZ.Text, "16900");
        }

        [TestMethod]
        public void TestMethod5()
        {
            var tbDays = driver.FindElementByAccessibilityId(days_name);
        
           

            var tbX = driver.FindElementByAccessibilityId(room_name);
            tbX.SendKeys(Keys.Down);
            tbX.SendKeys(Keys.Down); 

            var tbBeds = driver.FindElementByAccessibilityId(beds_name);
            tbBeds.SendKeys(Keys.Down);
            tbBeds.SendKeys(Keys.Down); 

            var btnSum = driver.FindElementByAccessibilityId(button_name);
            btnSum.Click(); 

            var tbZ = driver.FindElementByAccessibilityId(price_name);
            Assert.AreEqual(tbZ.Text, "1000"); 
        }
        

        [TestMethod]
        public void TestMethod6()
        {

            var tbDays = driver.FindElementByAccessibilityId(days_name);
     

            var tbX = driver.FindElementByAccessibilityId(room_name);
            tbX.SendKeys(Keys.Down);

            var cbSafe = driver.FindElementByAccessibilityId(safe_name);
            cbSafe.Click(); 

            var tbBeds = driver.FindElementByAccessibilityId(beds_name);
            tbBeds.SendKeys(Keys.Down);
            tbBeds.SendKeys(Keys.Down);

            var btnSum = driver.FindElementByAccessibilityId(button_name);
            btnSum.Click(); 

            var tbZ = driver.FindElementByAccessibilityId(price_name);
            Assert.AreEqual(tbZ.Text, "950"); 
        }

        [TestMethod]
        public void TestMethod7()
        {
            var tbDays = driver.FindElementByAccessibilityId(days_name);
            tbDays.SendKeys(Keys.Up);

            var tbX = driver.FindElementByAccessibilityId(room_name);
            tbX.SendKeys(Keys.Down);
            tbX.SendKeys(Keys.Down); 

            var tbBeds = driver.FindElementByAccessibilityId(beds_name);
            tbBeds.SendKeys(Keys.Down);
            tbBeds.SendKeys(Keys.Down);

            var btnSum = driver.FindElementByAccessibilityId(button_name);
            btnSum.Click(); 

            var tbZ = driver.FindElementByAccessibilityId(price_name);
            Assert.AreEqual(tbZ.Text, "2000"); 
        }

        [TestMethod]
        public void TestMethod8()
        {
            var tbDays = driver.FindElementByAccessibilityId(days_name);
        

            var tbX = driver.FindElementByAccessibilityId(room_name);
            tbX.SendKeys(Keys.Down);
            tbX.SendKeys(Keys.Down);
            tbX.SendKeys(Keys.Down);

            var cbBreakfast = driver.FindElementByAccessibilityId(breakfast_name);
            cbBreakfast.Click(); 

            var cbSafe = driver.FindElementByAccessibilityId(safe_name);
            cbSafe.Click(); 

            var btnSum = driver.FindElementByAccessibilityId(button_name);
            btnSum.Click(); 

            var tbBeds = driver.FindElementByAccessibilityId(beds_name);
            tbBeds.SendKeys(Keys.Down);
            tbBeds.SendKeys(Keys.Down);

            var tbZ = driver.FindElementByAccessibilityId(price_name);
            Assert.AreEqual(tbZ.Text, "2800");
        }

    }
}
