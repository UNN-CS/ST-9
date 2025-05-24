using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Threading; 

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
            appCapabilities.AddAdditionalCapability("app", @"C:\Users\alron\source\repos\HotelCalculator\HotelCalculator\bin\Debug\net8.0-windows\HotelCalculator.exe");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

            driver = new WindowsDriver<WindowsElement>(new Uri(DriveURL), appCapabilities);

            Thread.Sleep(2000);
        }

        private void EnterDataAndCalculate(string days, string category, string capacity, string safe, string breakfast)
        {
            driver.FindElementByAccessibilityId("tbDays").Clear();
            driver.FindElementByAccessibilityId("tbCategory").Clear();
            driver.FindElementByAccessibilityId("tbCapacity").Clear();
            driver.FindElementByAccessibilityId("tbSafe").Clear();
            driver.FindElementByAccessibilityId("tbBreakfast").Clear();

            driver.FindElementByAccessibilityId("tbDays").SendKeys(days);
            driver.FindElementByAccessibilityId("tbCategory").SendKeys(category);
            driver.FindElementByAccessibilityId("tbCapacity").SendKeys(capacity);
            driver.FindElementByAccessibilityId("tbSafe").SendKeys(safe);
            driver.FindElementByAccessibilityId("tbBreakfast").SendKeys(breakfast);

            driver.FindElementByAccessibilityId("btnCalculate").Click();

            Thread.Sleep(500);
        }


        [TestMethod]
        public void Test_EconomySingleWithoutExtras_OneDay()
        {
            EnterDataAndCalculate("1", "1", "1", "нет", "нет");
            var tbTotal = driver.FindElementByAccessibilityId("tbTotal");
            Assert.AreEqual(tbTotal.Text, "2000");
        }

        [TestMethod]
        public void Test_StandardDoubleWithSafe_TwoDays()
        {
            EnterDataAndCalculate("2", "2", "2", "да", "нет");
            var tbTotal = driver.FindElementByAccessibilityId("tbTotal");
            Assert.AreEqual(tbTotal.Text, "11000");
        }

        [TestMethod]
        public void Test_LuxTripleWithBreakfast_FourDays()
        {
            EnterDataAndCalculate("4", "3", "3", "нет", "да");
            var tbTotal = driver.FindElementByAccessibilityId("tbTotal");
            Assert.AreEqual(tbTotal.Text, "40000");
        }

        [TestMethod]
        public void Test_EconomyTripleWithAllExtras_SevenDays()
        {
            EnterDataAndCalculate("7", "1", "3", "да", "да");
            var tbTotal = driver.FindElementByAccessibilityId("tbTotal");
            Assert.AreEqual(tbTotal.Text, "38500");
        }

        [TestMethod]
        public void Test_StandardSingleWithAllExtras_OneDay()
        {
            EnterDataAndCalculate("1", "2", "1", "да", "да");
            var tbTotal = driver.FindElementByAccessibilityId("tbTotal");
            Assert.AreEqual(tbTotal.Text, "5500");
        }

        [TestMethod]
        public void Test_LuxSingleWithoutExtras_TenDays()
        {
            EnterDataAndCalculate("10", "3", "1", "нет", "нет");
            var tbTotal = driver.FindElementByAccessibilityId("tbTotal");
            Assert.AreEqual(tbTotal.Text, "70000");
        }

        [TestMethod]
        public void Test_EconomySingleWithoutExtras_ZeroDays()
        {
            EnterDataAndCalculate("0", "1", "1", "нет", "нет");
            var tbTotal = driver.FindElementByAccessibilityId("tbTotal");
            Assert.AreEqual(tbTotal.Text, "0");
        }

        [TestMethod]
        public void Test_LuxTripleWithAllExtras_LargeNumberOfDays()
        {
            EnterDataAndCalculate("30", "3", "3", "да", "да");
            var tbTotal = driver.FindElementByAccessibilityId("tbTotal");
            Assert.AreEqual(tbTotal.Text, "315000");
        }

        [TestMethod]
        public void Test_EmptyInput_HandlesErrorDialogAndChecksOutput()
        {
            driver.FindElementByAccessibilityId("tbDays").Clear();
            driver.FindElementByAccessibilityId("tbCategory").Clear();
            driver.FindElementByAccessibilityId("tbCapacity").Clear();
            driver.FindElementByAccessibilityId("tbSafe").Clear();
            driver.FindElementByAccessibilityId("tbBreakfast").Clear();
            driver.FindElementByAccessibilityId("btnCalculate").Click();
            Thread.Sleep(1000); 
            WindowsElement okButton = null;
            try
            {
                okButton = driver.FindElementByName("ОК");
            }
            catch (NoSuchElementException)
            {
                try
                {
                    okButton = driver.FindElementByAccessibilityId("2"); 
                }
                catch (NoSuchElementException)
                {
                    Assert.Fail("Не удалось найти кнопку 'ОК' в диалоговом окне ошибки. Убедитесь, что диалог появляется и кнопка доступна.");
                }
            }
            if (okButton != null)
            {
                okButton.Click();
            }
            else
            {
                Assert.Fail("Кнопка 'ОК' диалогового окна ошибки не была найдена.");
            }
            Thread.Sleep(500);
            var tbTotal = driver.FindElementByAccessibilityId("tbTotal");
            Assert.AreEqual("", tbTotal.Text, "Поле 'Сумма' должно быть пустым или сброшенным после некорректного ввода и закрытия окна ошибки.");
        }

        [TestMethod]
        public void Test_CaseInsensitiveInput_SafeBreakfast()
        {
            EnterDataAndCalculate("1", "1", "1", "НЕТ", "Да");
            var tbTotal = driver.FindElementByAccessibilityId("tbTotal");
            Assert.AreEqual(tbTotal.Text, "3000");
        }


        [TestCleanup]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}