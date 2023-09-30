using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace Etap_9
{
    [TestClass]
    public class EtoE_9_1_test
    {
        private IWebDriver _driver;

        [TestInitialize]
        public void SetUp()
        {
            _driver = new EdgeDriver();
        }

        [TestMethod]
        public void UserCanLogin()
        {
            _driver.Navigate().GoToUrl("https://localhost:44335/Genres/Create");

            var nameInput = _driver.FindElement(By.Id("Name"));
            var submitInput = _driver.FindElement(By.Id("SubmitButton"));

            nameInput.SendKeys("test");
            submitInput.Click();

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            var navbar = _driver.FindElement(By.Id("linkTest"));
            Assert.IsNotNull(navbar);

        }

        [TestCleanup]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}