using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace Etap_9
{
    [TestClass]
    public class LoginTests
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
            NavigateToLoginPage();

            var testEmail = _driver.FindElement(By.Id("Input_Email"));
            var testPassword = _driver.FindElement(By.Id("Input_Password"));
            var Testlogin = _driver.FindElement(By.Id("login-submit"));

            testEmail.SendKeys("bodya.xax@gmail.com");
            testPassword.SendKeys("QwArty_1");
            Testlogin.Click();

            Assert.AreEqual("https://localhost:44335/Identity/Account/Login", _driver.Url);
        }

        [TestMethod]
        public void UserCannotLoginWithIncorrectCredentials()
        {
            NavigateToLoginPage();

            var testEmail = _driver.FindElement(By.Id("Input_Email"));
            var testPassword = _driver.FindElement(By.Id("Input_Password"));
            var Testlogin = _driver.FindElement(By.Id("login-submit"));

            testEmail.SendKeys("wrong.email@gmail.com");
            testPassword.SendKeys("WrongPassword");
            Testlogin.Click();

            Assert.AreNotEqual("https://localhost:44335/", _driver.Url);
        }

        [TestMethod]
        public void LoginPageDisplaysAllRequiredElements()
        {
            _driver.Navigate().GoToUrl("https://localhost:44335/Identity/Account/Login");

            var testEmail = _driver.FindElement(By.Id("Input_Email"));
            var testPassword = _driver.FindElement(By.Id("Input_Password"));

            Assert.IsTrue(testEmail.Displayed); 
            Assert.IsTrue(testPassword.Displayed);

        }

        [TestMethod]
        public void ForgotPasswordLinkIsDisplayed()
        {
            NavigateToLoginPage();

            var forgotPasswordLink = _driver.FindElement(By.Id("forgot-password-link"));

            Assert.IsTrue(forgotPasswordLink.Displayed);
        }


        private void NavigateToLoginPage()
        {
            _driver.Navigate().GoToUrl("https://localhost:44335/Identity/Account/Login");
        }

        [TestCleanup]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
