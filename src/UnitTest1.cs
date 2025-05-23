namespace TestProject1
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
    public class UnitTest1
    {
        public WindowsDriver<WindowsElement> driver;

        [TestInitialize]
        public void Setup()
        {
            //Progress.Start(@"C:\Program Files (x86)\Windows Application Driver\WinAppDriver.exe");
            var app = new AppiumOptions();
            app.AddAdditionalCapability("app", @"C:\Users\fill2\repos\QA\ST-9\ST-9\bin\Debug\net8.0-windows\ST-9.exe");
            //app.AddAdditionalCapability("ms:waitForAppLaunch", "30");
            app.AddAdditionalCapability("deviceName", "WindowsPC");
            driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), app);
        }

        [TestMethod]
        public void Test1()
        {
            var count_day = driver.FindElementByAccessibilityId("count_day");
            count_day.SendKeys("1");

            var kategory = driver.FindElementByAccessibilityId("kategory");
            kategory.SendKeys("эконом");

            var vmestimost = driver.FindElementByAccessibilityId("vmestimost");
            vmestimost.SendKeys("1");

            var seif = driver.FindElementByAccessibilityId("seif");
            seif.SendKeys("нет");

            var breakfast = driver.FindElementByAccessibilityId("breakfast");
            breakfast.SendKeys("нет");

            var button1 = driver.FindElementByAccessibilityId("button1");
            button1.Click();

            var summa = driver.FindElementByAccessibilityId("summa");
            Assert.AreEqual(summa.Text, "10000");
        }

        [TestMethod]
        public void Test2()
        {
            var count_day = driver.FindElementByAccessibilityId("count_day");
            count_day.SendKeys("2");

            var kategory = driver.FindElementByAccessibilityId("kategory");
            kategory.SendKeys("люкс");

            var vmestimost = driver.FindElementByAccessibilityId("vmestimost");
            vmestimost.SendKeys("1");

            var seif = driver.FindElementByAccessibilityId("seif");
            seif.SendKeys("нет");

            var breakfast = driver.FindElementByAccessibilityId("breakfast");
            breakfast.SendKeys("нет");

            var button1 = driver.FindElementByAccessibilityId("button1");
            button1.Click();

            var summa = driver.FindElementByAccessibilityId("summa");
            Assert.AreEqual(summa.Text, "78000");
        }

        [TestMethod]
        public void Test3()
        {
            var count_day = driver.FindElementByAccessibilityId("count_day");
            count_day.SendKeys("3");

            var kategory = driver.FindElementByAccessibilityId("kategory");
            kategory.SendKeys("стандарт");

            var vmestimost = driver.FindElementByAccessibilityId("vmestimost");
            vmestimost.SendKeys("1");

            var seif = driver.FindElementByAccessibilityId("seif");
            seif.SendKeys("нет");

            var breakfast = driver.FindElementByAccessibilityId("breakfast");
            breakfast.SendKeys("нет");

            var button1 = driver.FindElementByAccessibilityId("button1");
            button1.Click();

            var summa = driver.FindElementByAccessibilityId("summa");
            Assert.AreEqual(summa.Text, "57000");
        }

        [TestMethod]
        public void Test4()
        {
            var count_day = driver.FindElementByAccessibilityId("count_day");
            count_day.SendKeys("1");

            var kategory = driver.FindElementByAccessibilityId("kategory");
            kategory.SendKeys("эконом");

            var vmestimost = driver.FindElementByAccessibilityId("vmestimost");
            vmestimost.SendKeys("2");

            var seif = driver.FindElementByAccessibilityId("seif");
            seif.SendKeys("да");

            var breakfast = driver.FindElementByAccessibilityId("breakfast");
            breakfast.SendKeys("нет");

            var button1 = driver.FindElementByAccessibilityId("button1");
            button1.Click();

            var summa = driver.FindElementByAccessibilityId("summa");
            Assert.AreEqual(summa.Text, "12500");
        }

        [TestMethod]
        public void Test5()
        {
            var count_day = driver.FindElementByAccessibilityId("count_day");
            count_day.SendKeys("2");

            var kategory = driver.FindElementByAccessibilityId("kategory");
            kategory.SendKeys("люкс");

            var vmestimost = driver.FindElementByAccessibilityId("vmestimost");
            vmestimost.SendKeys("1");

            var seif = driver.FindElementByAccessibilityId("seif");
            seif.SendKeys("да");

            var breakfast = driver.FindElementByAccessibilityId("breakfast");
            breakfast.SendKeys("да");

            var button1 = driver.FindElementByAccessibilityId("button1");
            button1.Click();

            var summa = driver.FindElementByAccessibilityId("summa");
            Assert.AreEqual(summa.Text, "84000");
        }

        [TestMethod]
        public void Test6()
        {
            var count_day = driver.FindElementByAccessibilityId("count_day");
            count_day.SendKeys("3");

            var kategory = driver.FindElementByAccessibilityId("kategory");
            kategory.SendKeys("стандарт");

            var vmestimost = driver.FindElementByAccessibilityId("vmestimost");
            vmestimost.SendKeys("2");

            var seif = driver.FindElementByAccessibilityId("seif");
            seif.SendKeys("да");

            var breakfast = driver.FindElementByAccessibilityId("breakfast");
            breakfast.SendKeys("да");

            var button1 = driver.FindElementByAccessibilityId("button1");
            button1.Click();

            var summa = driver.FindElementByAccessibilityId("summa");
            Assert.AreEqual(summa.Text, "78000");
        }

        [TestMethod]
        public void Test7()
        {
            var count_day = driver.FindElementByAccessibilityId("count_day");
            count_day.SendKeys("3");

            var kategory = driver.FindElementByAccessibilityId("kategory");
            kategory.SendKeys("эконом");

            var vmestimost = driver.FindElementByAccessibilityId("vmestimost");
            vmestimost.SendKeys("1");

            var seif = driver.FindElementByAccessibilityId("seif");
            seif.SendKeys("да");

            var breakfast = driver.FindElementByAccessibilityId("breakfast");
            breakfast.SendKeys("нет");

            var button1 = driver.FindElementByAccessibilityId("button1");
            button1.Click();

            var summa = driver.FindElementByAccessibilityId("summa");
            Assert.AreEqual(summa.Text, "30000");
        }

        [TestMethod]
        public void Test8()
        {
            var count_day = driver.FindElementByAccessibilityId("count_day");
            count_day.SendKeys("1");

            var kategory = driver.FindElementByAccessibilityId("kategory");
            kategory.SendKeys("эконом");

            var vmestimost = driver.FindElementByAccessibilityId("vmestimost");
            vmestimost.SendKeys("3");

            var seif = driver.FindElementByAccessibilityId("seif");
            seif.SendKeys("нет");

            var breakfast = driver.FindElementByAccessibilityId("breakfast");
            breakfast.SendKeys("нет");

            var button1 = driver.FindElementByAccessibilityId("button1");
            button1.Click();

            var summa = driver.FindElementByAccessibilityId("summa");
            Assert.AreEqual(summa.Text, "14000");
        }

        [TestMethod]
        public void Test9()
        {
            var count_day = driver.FindElementByAccessibilityId("count_day");
            count_day.SendKeys("2");

            var kategory = driver.FindElementByAccessibilityId("kategory");
            kategory.SendKeys("люкс");

            var vmestimost = driver.FindElementByAccessibilityId("vmestimost");
            vmestimost.SendKeys("3");

            var seif = driver.FindElementByAccessibilityId("seif");
            seif.SendKeys("нет");

            var breakfast = driver.FindElementByAccessibilityId("breakfast");
            breakfast.SendKeys("нет");

            var button1 = driver.FindElementByAccessibilityId("button1");
            button1.Click();

            var summa = driver.FindElementByAccessibilityId("summa");
            Assert.AreEqual(summa.Text, "110000");
        }

        [TestMethod]
        public void Test10()
        {
            var count_day = driver.FindElementByAccessibilityId("count_day");
            count_day.SendKeys("2");

            var kategory = driver.FindElementByAccessibilityId("kategory");
            kategory.SendKeys("люкс");

            var vmestimost = driver.FindElementByAccessibilityId("vmestimost");
            vmestimost.SendKeys("2");

            var seif = driver.FindElementByAccessibilityId("seif");
            seif.SendKeys("нет");

            var breakfast = driver.FindElementByAccessibilityId("breakfast");
            breakfast.SendKeys("да");

            var button1 = driver.FindElementByAccessibilityId("button1");
            button1.Click();

            var summa = driver.FindElementByAccessibilityId("summa");
            Assert.AreEqual(summa.Text, "98000");
        }
    }
}