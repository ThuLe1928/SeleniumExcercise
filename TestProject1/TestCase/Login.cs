using DemoQADotComTest.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace DemoQADotComTest.TestCase
{
    class Login
    {
        [SetUp]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            Constant.Constant.WEBDRIVER = new ChromeDriver();
            Constant.Constant.WEBDRIVER.Manage().Window.Maximize();
            Constant.Constant.WEBDRIVER.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            Constant.Constant.WEBDRIVER.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void LoginTest()
        {
            HomePage homePage = new HomePage();
            homePage.open();
            homePage.goToLoginPage();

            LoginPage loginPage = new LoginPage();
            loginPage.login(Constant.Constant.USERNAME, Constant.Constant.PASSWORD);

            String headerText = homePage.getUsernameLabelValue();
            Assert.AreEqual(headerText, Constant.Constant.USERNAME, "Username is not displayed as expected");

        }


        [TearDown]

        public void TearDown()
        {
            Constant.Constant.WEBDRIVER.Quit();
        }
    }
}
