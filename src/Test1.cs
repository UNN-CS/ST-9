namespace Test1
{
    using System;
    using System.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium.Appium.Windows;
    using OpenQA.Selenium.Remote;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Appium;
    using OpenQA.Selenium.Support.UI;

    [TestClass]
    public class Test1
    {
        public WindowsDriver<WindowsElement> driver;

        [TestInitialize]
        public void Setup()
        {
            var app = new AppiumOptions();
            app.AddAdditionalCapability("app", @"C:\admin\ST-9\ST-9\bin\Debug\net8.0-windows\ST-9.exe");
            app.AddAdditionalCapability("ms:waitForAppLaunch", "3");
            app.AddAdditionalCapability("deviceName", "WindowsPC");
            driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), app);
        }

        [TestMethod]
        public void TestMethod1()
        {
            var number_people = driver.FindElementByAccessibilityId("number_people");
            number_people.SendKeys("1");

            var category = driver.FindElementByAccessibilityId("category");
            category.SendKeys("1");

            var capacity = driver.FindElementByAccessibilityId("capacity");
            capacity.SendKeys("1");

            var vault = driver.FindElementByAccessibilityId("vault");
            vault.SendKeys("   ");

            var breakfast = driver.FindElementByAccessibilityId("breakfast");
            breakfast.SendKeys("   ");

            var button1 = driver.FindElementByAccessibilityId("button1");
            button1.Click();

            var summa = driver.FindElementByAccessibilityId("summa");
            Assert.AreEqual(summa.Text, "6000");
        }

        [TestMethod]
        public void TestMethod2()
        {
            var number_people = driver.FindElementByAccessibilityId("number_people");
            number_people.SendKeys("2");

            var category = driver.FindElementByAccessibilityId("category");
            category.SendKeys("1");

            var capacity = driver.FindElementByAccessibilityId("capacity");
            capacity.SendKeys("3");

            var vault = driver.FindElementByAccessibilityId("vault");
            vault.SendKeys("   ");

            var breakfast = driver.FindElementByAccessibilityId("breakfast");
            breakfast.SendKeys("   ");

            var button1 = driver.FindElementByAccessibilityId("button1");
            button1.Click();

            var summa = driver.FindElementByAccessibilityId("summa");
            Assert.AreEqual(summa.Text, "36000");
        }

        [TestMethod]
        public void TestMethod3()
        {
            var number_people = driver.FindElementByAccessibilityId("number_people");
            number_people.SendKeys("2");

            var category = driver.FindElementByAccessibilityId("category");
            category.SendKeys("1");

            var capacity = driver.FindElementByAccessibilityId("capacity");
            capacity.SendKeys("3");

            var vault = driver.FindElementByAccessibilityId("vault");
            vault.SendKeys("  ");

            var breakfast = driver.FindElementByAccessibilityId("breakfast");
            breakfast.SendKeys("  ");

            var button1 = driver.FindElementByAccessibilityId("button1");
            button1.Click();

            var summa = driver.FindElementByAccessibilityId("summa");
            Assert.AreEqual(summa.Text, "43000");
        }

        [TestMethod]
        public void TestMethod4()
        {
            var number_people = driver.FindElementByAccessibilityId("number_people");
            number_people.SendKeys("2");

            var category = driver.FindElementByAccessibilityId("category");
            category.SendKeys("3");

            var capacity = driver.FindElementByAccessibilityId("capacity");
            capacity.SendKeys("1");

            var vault = driver.FindElementByAccessibilityId("vault");
            vault.SendKeys("   ");

            var breakfast = driver.FindElementByAccessibilityId("breakfast");
            breakfast.SendKeys("  ");

            var button1 = driver.FindElementByAccessibilityId("button1");
            button1.Click();

            var summa = driver.FindElementByAccessibilityId("summa");
            Assert.AreEqual(summa.Text, "62000");
        }

        [TestMethod]
        public void TestMethod5()
        {
            var number_people = driver.FindElementByAccessibilityId("number_people");
            number_people.SendKeys("1");

            var category = driver.FindElementByAccessibilityId("category");
            category.SendKeys("2");

            var capacity = driver.FindElementByAccessibilityId("capacity");
            capacity.SendKeys("3");

            var vault = driver.FindElementByAccessibilityId("vault");
            vault.SendKeys("  ");

            var breakfast = driver.FindElementByAccessibilityId("breakfast");
            breakfast.SendKeys("   ");

            var button1 = driver.FindElementByAccessibilityId("button1");
            button1.Click();

            var summa = driver.FindElementByAccessibilityId("summa");
            Assert.AreEqual(summa.Text, "46000");
        }

        [TestMethod]
        public void TestMethod6()
        {
            var number_people = driver.FindElementByAccessibilityId("number_people");
            number_people.SendKeys("2");

            var category = driver.FindElementByAccessibilityId("category");
            category.SendKeys("1");

            var capacity = driver.FindElementByAccessibilityId("capacity");
            capacity.SendKeys("2");

            var vault = driver.FindElementByAccessibilityId("vault");
            vault.SendKeys("  ");

            var breakfast = driver.FindElementByAccessibilityId("breakfast");
            breakfast.SendKeys("  ");

            var button1 = driver.FindElementByAccessibilityId("button1");
            button1.Click();

            var summa = driver.FindElementByAccessibilityId("summa");
            Assert.AreEqual(summa.Text, "29000");
        }
    }
}