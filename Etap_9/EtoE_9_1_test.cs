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
        public void GenreCreate()
        {
            _driver.Navigate().GoToUrl("https://localhost:44335/Genres/Create");

            var nameInput = _driver.FindElement(By.Id("Name"));
            var submitInput = _driver.FindElement(By.Id("SubmitButton"));

            nameInput.SendKeys("test");
            submitInput.Click();

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            var butoon = _driver.FindElement(By.Id("linkTest"));
            Assert.IsNotNull(butoon);

        }

        [TestCleanup]
        public void CleanDriver()
        {
            _driver.Quit();
        }
    }
}

//QwArty_1