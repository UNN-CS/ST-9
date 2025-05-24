using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace TestProject1
{
    [TestClass]
    public class Test1
    {
        public const string DriveURL = "http://127.0.0.1:4723/";
        public WindowsDriver<WindowsElement> driver;

        [TestInitialize]
        public void Setup()
        {
            var appCapabilities = new AppiumOptions();
            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("\\TestProject1\\"));
            string appPath = System.IO.Path.Combine(projectPath, "WinFormsApp1", "bin", "Debug", "net8.0-windows", "WinFormsApp1.exe"); 
            
            if (!System.IO.File.Exists(appPath))
            {
                appPath = System.IO.Path.Combine(projectPath, "WinFormsApp1", "bin", "Release", "net8.0-windows", "WinFormsApp1.exe");
                if (!System.IO.File.Exists(appPath))
                {
                    string solutionPath = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("\\WinFormsApp1\\TestProject1\\"));
                    appPath = System.IO.Path.Combine(solutionPath, "WinFormsApp1", "WinFormsApp1", "bin", "Debug", "net8.0-windows", "WinFormsApp1.exe");
                     if (!System.IO.File.Exists(appPath))
                     {
                        appPath = System.IO.Path.Combine(solutionPath, "WinFormsApp1", "WinFormsApp1", "bin", "Release", "net8.0-windows", "WinFormsApp1.exe");
                        if (!System.IO.File.Exists(appPath))
                        {
                            string workspaceRoot = "/c:/Users/Deus/Git-Repositories/ST-9"; 
                            appPath = System.IO.Path.Combine(workspaceRoot, "WinFormsApp1", "WinFormsApp1", "bin", "Debug", "net8.0-windows", "WinFormsApp1.exe");
                             if (!System.IO.File.Exists(appPath)){
                                appPath = System.IO.Path.Combine(workspaceRoot, "WinFormsApp1", "WinFormsApp1", "bin", "Release", "net8.0-windows", "WinFormsApp1.exe");
                                 if (!System.IO.File.Exists(appPath)){
                                    Assert.Fail($"Не удалось найти WinFormsApp1.exe. Проверьте путь: {appPath}. Укажите корректный АБСОЛЮТНЫЙ путь в Setup методе.");
                                 }
                             }
                        }
                     }
                }
            }

            appCapabilities.AddAdditionalCapability("app", appPath);
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "10"); 
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

            try
            {
                driver = new WindowsDriver<WindowsElement>(new Uri(DriveURL), appCapabilities, TimeSpan.FromSeconds(60)); 
            }
            catch (Exception ex)
            {
                Assert.Fail($"Не удалось запустить драйвер: {ex.Message}. Убедитесь, что WinAppDriver запущен.");
            }
            Assert.IsNotNull(driver, "Драйвер не был инициализирован.");
            Assert.IsNotNull(driver.SessionId, "Сессия драйвера не была создана.");
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

        private void ClearAndSendKeys(string accessibilityId, string text)
        {
            var element = driver.FindElementByAccessibilityId(accessibilityId);
            element.Clear();
            element.SendKeys(text);
        }

        [TestMethod]
        public void TestMethod1_StandardRoom_2Days_1Person_NoExtras()
        {
            ClearAndSendKeys("textBoxDays", "2");
            ClearAndSendKeys("textBoxCategory", "2"); 
            ClearAndSendKeys("textBoxCapacity", "1");
            ClearAndSendKeys("textBoxSafe", "нет");
            ClearAndSendKeys("textBoxBreakfast", "нет");

            driver.FindElementByAccessibilityId("buttonCalculate").Click();

            var tbSum = driver.FindElementByAccessibilityId("textBoxSum");
            
            Assert.IsTrue(tbSum.Text.Contains("$4,000.00"), $"Ожидалось '$4,000.00', получено '{tbSum.Text}'");
        }

        [TestMethod]
        public void TestMethod2_LuxuryRoom_3Days_2Persons_WithBreakfast()
        {
            ClearAndSendKeys("textBoxDays", "3");
            ClearAndSendKeys("textBoxCategory", "1"); 
            ClearAndSendKeys("textBoxCapacity", "2");
            ClearAndSendKeys("textBoxSafe", "нет");
            ClearAndSendKeys("textBoxBreakfast", "да");
            driver.FindElementByAccessibilityId("buttonCalculate").Click();
            var tbSum = driver.FindElementByAccessibilityId("textBoxSum");
            
            Assert.IsTrue(tbSum.Text.Contains("$21,000.00"), $"Ожидалось '$21,000.00', получено '{tbSum.Text}'");
        }

        [TestMethod]
        public void TestMethod3_EconomyRoom_5Days_3Persons_WithSafeAndBreakfast()
        {
            ClearAndSendKeys("textBoxDays", "5");
            ClearAndSendKeys("textBoxCategory", "3"); 
            ClearAndSendKeys("textBoxCapacity", "3");
            ClearAndSendKeys("textBoxSafe", "да");
            ClearAndSendKeys("textBoxBreakfast", "да");
            driver.FindElementByAccessibilityId("buttonCalculate").Click();
            var tbSum = driver.FindElementByAccessibilityId("textBoxSum");
            
            Assert.IsTrue(tbSum.Text.Contains("$24,000.00"), $"Ожидалось '$24,000.00', получено '{tbSum.Text}'");
        }

        [TestMethod]
        public void TestMethod4_LuxuryRoom_1Day_1Person_WithSafe()
        {
            ClearAndSendKeys("textBoxDays", "1");
            ClearAndSendKeys("textBoxCategory", "1"); 
            ClearAndSendKeys("textBoxCapacity", "1");
            ClearAndSendKeys("textBoxSafe", "да");
            ClearAndSendKeys("textBoxBreakfast", "нет");
            driver.FindElementByAccessibilityId("buttonCalculate").Click();
            var tbSum = driver.FindElementByAccessibilityId("textBoxSum");
            
            Assert.IsTrue(tbSum.Text.Contains("$3,300.00"), $"Ожидалось '$3,300.00', получено '{tbSum.Text}'");
        }

        [TestMethod]
        public void TestMethod5_InvalidCategoryInput_ShouldShowError() 
        {
            ClearAndSendKeys("textBoxDays", "1");
            ClearAndSendKeys("textBoxCategory", "5"); 
            ClearAndSendKeys("textBoxCapacity", "1");
            ClearAndSendKeys("textBoxSafe", "нет");
            ClearAndSendKeys("textBoxBreakfast", "нет");
            driver.FindElementByAccessibilityId("buttonCalculate").Click();
            
            try
            {
                 WindowsElement errorDialog = driver.FindElementByName("Ошибка"); 
                 Assert.IsNotNull(errorDialog, "Диалоговое окно ошибки не найдено.");
                 WindowsElement okButton = null;
                 try { okButton = (WindowsElement?)errorDialog.FindElementByAccessibilityId("2"); } catch { }
                 if (okButton == null) try { okButton = (WindowsElement?)errorDialog.FindElementByName("OK"); } catch {}
                 
                 if (okButton == null) try { okButton = (WindowsElement?)errorDialog.FindElementByClassName("Button"); } catch {}

                 Assert.IsNotNull(okButton, "Кнопка OK в диалоге ошибки не найдена.");
                 okButton.Click();
            }
            catch (OpenQA.Selenium.WebDriverException ex) 
            {
                Assert.Fail($"Диалоговое окно ошибки не появилось или не удалось с ним взаимодействовать: {ex.Message}");
            }
        }
    }
}
