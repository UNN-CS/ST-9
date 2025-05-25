using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace HotelCalculatorTests
{
	[TestClass]
	public class HotelCalculatorTests
	{
		private const string DriverUrl = "http://127.0.0.1:4723/";
		private WindowsDriver<WindowsElement>? driver;

		[TestInitialize]
		public void Setup()
		{
			var appCapabilities = new AppiumOptions();
			appCapabilities.AddAdditionalCapability("app", @"C:\PROGRAMMING\HotelCalculator\HotelCalculator\bin\Debug\net7.0-windows\HotelCalc.exe");
			appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "5");
			appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");

			driver = new WindowsDriver<WindowsElement>(new Uri(DriverUrl), appCapabilities);
		}

		[TestCleanup]
		public void TearDown()
		{
			driver?.Quit();
		}

		[TestMethod]
		public void TestEconomySingleRoomNoExtras()
		{
			driver.FindElementByAccessibilityId("Days").SendKeys("2");
			driver.FindElementByAccessibilityId("Category").SendKeys("1");
			driver.FindElementByAccessibilityId("Capacity").SendKeys("1");
			driver.FindElementByAccessibilityId("Safe").SendKeys("нет");
			driver.FindElementByAccessibilityId("Breakfast").SendKeys("нет");
			driver.FindElementByAccessibilityId("Calculate").Click();
			string result = driver.FindElementByAccessibilityId("Sum").Text;
			Assert.AreEqual("2000", result);
		}

		[TestMethod]
		public void TestStandardDoubleRoomWithBreakfast()
		{
			driver.FindElementByAccessibilityId("Days").SendKeys("3");
			driver.FindElementByAccessibilityId("Category").SendKeys("2");
			driver.FindElementByAccessibilityId("Capacity").SendKeys("2");
			driver.FindElementByAccessibilityId("Safe").SendKeys("нет");
			driver.FindElementByAccessibilityId("Breakfast").SendKeys("да");
			driver.FindElementByAccessibilityId("Calculate").Click();
			string result = driver.FindElementByAccessibilityId("Sum").Text;
			Assert.AreEqual("10800", result);
		}

		[TestMethod]
		public void TestLuxuryTripleRoomWithAllExtras()
		{
			driver.FindElementByAccessibilityId("Days").SendKeys("5");
			driver.FindElementByAccessibilityId("Category").SendKeys("3");
			driver.FindElementByAccessibilityId("Capacity").SendKeys("3");
			driver.FindElementByAccessibilityId("Safe").SendKeys("да");
			driver.FindElementByAccessibilityId("Breakfast").SendKeys("да");
			driver.FindElementByAccessibilityId("Calculate").Click();
			string result = driver.FindElementByAccessibilityId("Sum").Text;
			Assert.AreEqual("50500", result);
		}

		[TestMethod]
		public void TestInvalidCategory()
		{
			driver.FindElementByAccessibilityId("Days").SendKeys("1");
			driver.FindElementByAccessibilityId("Category").SendKeys("4");
			driver.FindElementByAccessibilityId("Capacity").SendKeys("1");
			driver.FindElementByAccessibilityId("Safe").SendKeys("нет");
			driver.FindElementByAccessibilityId("Breakfast").SendKeys("нет");
			driver.FindElementByAccessibilityId("Calculate").Click();
			string result = driver.FindElementByAccessibilityId("Sum").Text;
			Assert.IsTrue(string.IsNullOrEmpty(result));
		}

		[TestMethod]
		public void TestSingleDayEconomyRoom()
		{
			driver.FindElementByAccessibilityId("Days").SendKeys("1");
			driver.FindElementByAccessibilityId("Category").SendKeys("1");
			driver.FindElementByAccessibilityId("Capacity").SendKeys("1");
			driver.FindElementByAccessibilityId("Safe").SendKeys("нет");
			driver.FindElementByAccessibilityId("Breakfast").SendKeys("нет");
			driver.FindElementByAccessibilityId("Calculate").Click();
			string result = driver.FindElementByAccessibilityId("Sum").Text;
			Assert.AreEqual("1000", result);
		}

		[TestMethod]
		public void TestWeekStayStandardRoom()
		{
			driver.FindElementByAccessibilityId("Days").SendKeys("7");
			driver.FindElementByAccessibilityId("Category").SendKeys("2");
			driver.FindElementByAccessibilityId("Capacity").SendKeys("2");
			driver.FindElementByAccessibilityId("Safe").SendKeys("да");
			driver.FindElementByAccessibilityId("Breakfast").SendKeys("нет");
			driver.FindElementByAccessibilityId("Calculate").Click();
			string result = driver.FindElementByAccessibilityId("Sum").Text;
			Assert.AreEqual("22400", result);
		}
	}
}