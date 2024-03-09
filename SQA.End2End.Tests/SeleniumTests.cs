namespace SQA.End2End.Tests
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;
    public class SeleniumTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver(); // O usa FirefoxDriver para pruebas en Firefox
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("https://demo.seleniumeasy.com/");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void ClickingTodaysDateInPicker_ShouldDisplayCurrentDateText()
        {
            driver.FindElement(By.CssSelector(".tree-branch:nth-child(2) > .tree-indicator")).Click();
            driver.FindElement(By.LinkText("Bootstrap Date Picker")).Click();
            driver.FindElement(By.CssSelector(".glyphicon-th")).Click();
            driver.FindElement(By.CssSelector(".datepicker-days tfoot .today")).Click();
            
            var actual = driver.FindElement(By.CssSelector("#sandbox-container1 > div > input")).GetAttribute("value");

            Assert.That(actual, Is.EqualTo(DateTime.Now.ToString("dd/MM/yyyy")));
        }

        [Test]
        public void SubmitInputText_ShouldDisplayTextBelow()
        {
            const string ExpectedMessage = "hola mundo SDQ";
            driver.FindElement(By.Id("btn_basic_example")).Click();
            driver.FindElement(By.LinkText("Simple Form Demo")).Click();
            driver.FindElement(By.Id("user-message")).SendKeys(ExpectedMessage);
            driver.FindElement(By.CssSelector(".btn:nth-child(2)")).Click();

            var actual = driver.FindElement(By.CssSelector("#display")).Text;

            Assert.That(actual, Is.EqualTo(ExpectedMessage));
        }

        [Test]
        public void ClickGetUser_ShouldLoadANewUserImage()
        {
            driver.FindElement(By.CssSelector(".tree-branch:nth-child(7) > .tree-indicator")).Click();
            driver.FindElement(By.LinkText("Dynamic Data Loading")).Click();
            driver.FindElement(By.Id("save")).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(driver => driver.FindElements(By.CssSelector("#loading > img")).Count > 0);

            var actual = driver.FindElements(By.CssSelector("#loading > img"));

            Assert.That(actual.Count, Is.GreaterThan(0));
        }

        [Test]
        public void ClickPopUp_ShouldDisplayConfirmationText()
        {
            driver.FindElement(By.CssSelector(".tree-branch:nth-child(5) > .tree-indicator")).Click();
            driver.FindElement(By.LinkText("Javascript Alerts")).Click();
            driver.FindElement(By.CssSelector(".panel:nth-child(5) .btn")).Click();
            driver.SwitchTo().Alert().Accept();

            var actual = driver.FindElement(By.Id("confirm-demo")).Text;

            Assert.That(actual, Is.EqualTo("You pressed OK!"));
        }
    }
}