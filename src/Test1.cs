using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace ST9_Test {
[TestClass]
public sealed class Test1
{
    private WindowsDriver<WindowsElement> driver;

    [TestInitialize]
    public void Initialize()
    {
        string appPath = @"ST9-WF\bin\Debug\net8.0-windows\ST9-WF.exe";
        string driverURL = @"http://127.0.0.1:4723";

        AppiumOptions options = new AppiumOptions();
        options.AddAdditionalCapability("app", appPath);
        options.AddAdditionalCapability("deviceName", "WindowsPC");
        options.AddAdditionalCapability("ms:waitForAppLaunch", "3");

        this.driver = new WindowsDriver<WindowsElement>(new Uri(driverURL), options);
    }

    [TestCleanup]
    public void Cleanup() { driver?.Quit(); }

    private string calculate()
    {
        driver.FindElementByAccessibilityId("resultButton").Click();
        return driver.FindElementByAccessibilityId("resultTextBox").Text;
    }

    private void fill(string daysOfStay,
                      string roomCategory,
                      string roomCapacity,
                      string safeOption,
                      string breakfastOption)
    {
        driver.FindElementByAccessibilityId("daysOfStayTextBox").SendKeys(daysOfStay);
        driver.FindElementByAccessibilityId("roomCategoryTextBox").SendKeys(roomCategory);
        driver.FindElementByAccessibilityId("roomCapacityTextBox").SendKeys(roomCapacity);
        driver.FindElementByAccessibilityId("safeOptionTextBox").SendKeys(safeOption);
        driver.FindElementByAccessibilityId("breakfastOptionTextBox").SendKeys(breakfastOption);
    }

    [TestMethod]
    [DoNotParallelize]
    public void TestBasicCalculation()
    {
        fill("2", "1", "2", "нет", "нет");
        string result = calculate();
        Assert.AreEqual("1000", result);
    }

    [TestMethod]
    [DoNotParallelize]
    public void TestWithAllOptions()
    {
        fill("3", "2", "3", "да", "да");
        string result = calculate();
        Assert.AreEqual("6000", result);
    }

    [TestMethod]
    [DoNotParallelize]
    public void TestMinimumValues()
    {
        fill("1", "1", "1", "нет", "нет");
        string result = calculate();
        Assert.AreEqual("250", result);
    }

    [TestMethod]
    [DoNotParallelize]
    public void TestMaximumValues()
    {
        fill("10", "3", "3", "да", "да");
        string result = calculate();
        Assert.AreEqual("24000", result);
    }

    [TestMethod]
    [DoNotParallelize]
    public void TestOnlySafeOption()
    {
        fill("2", "2", "2", "да", "нет");
        string result = calculate();
        Assert.AreEqual("2500", result);
    }

    [TestMethod]
    [DoNotParallelize]
    public void TestOnlyBreakfastOption()
    {
        fill("2", "2", "2", "нет", "да");
        string result = calculate();
        Assert.AreEqual("3000", result);
    }

    [TestMethod]
    [DoNotParallelize]
    public void TestInvalidDaysOfStay()
    {
        fill("0", "2", "2", "да", "да");
        string result = calculate();
        Assert.AreEqual("Ошибка ввода", result);
    }

    [TestMethod]
    [DoNotParallelize]
    public void TestInvalidRoomCategory()
    {
        fill("2", "4", "2", "да", "да");
        string result = calculate();
        Assert.AreEqual("Ошибка ввода", result);
    }

    [TestMethod]
    [DoNotParallelize]
    public void TestInvalidRoomCapacity()
    {
        fill("2", "2", "4", "да", "да");
        string result = calculate();
        Assert.AreEqual("Ошибка ввода", result);
    }

    [TestMethod]
    [DoNotParallelize]
    public void TestInvalidSafeOption()
    {
        fill("2", "2", "2", "maybe", "да");
        string result = calculate();
        Assert.AreEqual("Ошибка ввода", result);
    }

    [TestMethod]
    [DoNotParallelize]
    public void TestInvalidBreakfastOption()
    {
        fill("2", "2", "2", "да", "может быть");
        string result = calculate();
        Assert.AreEqual("Ошибка ввода", result);
    }

    [TestMethod]
    [DoNotParallelize]
    public void TestNonNumericInput()
    {
        fill("два", "2", "2", "да", "да");
        string result = calculate();
        Assert.AreEqual("Ошибка ввода", result);
    }

    [TestMethod]
    [DoNotParallelize]
    public void TestEmptyFields()
    {
        fill("", "", "", "", "");
        string result = calculate();
        Assert.AreEqual("Ошибка ввода", result);
    }

    [TestMethod]
    [DoNotParallelize]
    public void TestNegativeDays()
    {
        fill("-1", "2", "2", "да", "да");
        string result = calculate();
        Assert.AreEqual("Ошибка ввода", result);
    }

    [TestMethod]
    [DoNotParallelize]
    public void TestCaseSensitivityForYes()
    {
        fill("2", "2", "2", "ДА", "Да");
        string result = calculate();
        Assert.AreEqual("3500", result);
    }
}
}
