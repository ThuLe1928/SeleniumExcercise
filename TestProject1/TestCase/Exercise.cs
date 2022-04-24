using DemoQADotComTest.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;
using System.Threading;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace DemoQADotComTest.TestCase
{
    class Exercise
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
        public void Scenario1()
        {
            HomePage homePage = new HomePage();
            homePage.open();
            homePage.goToLoginPage();

            LoginPage loginPage = new LoginPage();
            loginPage.login(Constant.Constant.USERNAME, Constant.Constant.PASSWORD);

            var bookSelection = Constant.Constant.WEBDRIVER.FindElement(By.XPath("//span[contains(@id, 'Git Pocket Guide')]"));
            bookSelection.Click();
            Thread.Sleep(3000);

            var bookAddingButton = Constant.Constant.WEBDRIVER.FindElement(By.XPath("//button[@id='addNewRecordButton' and text()='Add To Your Collection']"));
            loginPage.scrollElementIntoView(bookAddingButton);
            bookAddingButton.Click();
            Thread.Sleep(3000);

            IAlert alert = Constant.Constant.WEBDRIVER.SwitchTo().Alert();
            String alertText = Constant.Constant.WEBDRIVER.SwitchTo().Alert().Text;
            Assert.AreEqual(alertText, "Book added to your collection.");
            alert.Accept();

            var myProfile = Constant.Constant.WEBDRIVER.FindElement(By.XPath("//div[@class='element-group' and descendant::div[@class='header-text' and text()='Book Store Application']]//span[text()='Profile']"));
            loginPage.scrollElementIntoView(myProfile);
            myProfile.Click();
            Thread.Sleep(3000);

            var addedBookInProfile = Constant.Constant.WEBDRIVER.FindElement(By.XPath("//span[contains(@id, 'Git Pocket Guide')]"));
            Assert.IsNotNull(addedBookInProfile);
        }

        [Test]
        public void Scenario2()
        {
            HomePage homePage = new HomePage();
            homePage.open();
            homePage.goToLoginPage();

            LoginPage loginPage = new LoginPage();
            loginPage.login(Constant.Constant.USERNAME, Constant.Constant.PASSWORD);

            var inputBookName = Constant.Constant.WEBDRIVER.FindElement(By.XPath("//input[@id='searchBox']"));
            inputBookName.SendKeys("Design");
            Thread.Sleep(2000);
            var searchResults = Constant.Constant.WEBDRIVER.FindElements(By.XPath("//a[contains(text(),'Design')]"));


            var bookName1 = searchResults[0].Text;
            var bookName2 = searchResults[1].Text;
            string[] books = { "Designing Evolvable Web APIs with ASP.NET", "Learning JavaScript Design Patterns" };
            var book1ExistedIndex = 0;
            var isBookName1Existed = false;
            for (int i = 0; i < books.Length; i++) 
            {
                if (bookName1 == books[i])
                {
                    isBookName1Existed = true;
                    book1ExistedIndex = i;
                }
            }
            var isBookName2Existed = false;
            for (int j = 0; j < books.Length && j != book1ExistedIndex; j++)
            {
                if (bookName2 == books[j])
                {
                    isBookName2Existed = true;
                }
            }

            Assert.IsTrue(searchResults.Any(bookElm => bookElm.Text == "Learning JavaScript Design Patterns"));
            Assert.IsTrue(searchResults.Any(bookElm => bookElm.Text == "Designing Evolvable Web APIs with ASP.NET"));
        }

        [Test]
        public void Scenario3()
        {
            HomePage homePage = new HomePage();
            homePage.open();
            homePage.goToLoginPage();

            LoginPage loginPage = new LoginPage();
            loginPage.login(Constant.Constant.USERNAME, Constant.Constant.PASSWORD);
            Thread.Sleep(3000);

            var myProfile = Constant.Constant.WEBDRIVER.FindElement(By.XPath("//div[@class='element-group' and descendant::div[@class='header-text' and text()='Book Store Application']]//span[text()='Profile']"));
            loginPage.scrollElementIntoView(myProfile);
            myProfile.Click();
            Thread.Sleep(3000);

            var inputBookName = Constant.Constant.WEBDRIVER.FindElement(By.XPath("//input[@id='searchBox']"));
            inputBookName.SendKeys("Learning JavaScript Design Patterns");

            var deleteSearchResultBookButton = Constant.Constant.WEBDRIVER.FindElement(By.XPath("//div[@role='rowgroup' and descendant::a[text()='Learning JavaScript Design Patterns']]//span[contains(@id,'delete')]"));
            deleteSearchResultBookButton.Click();
            Thread.Sleep(3000);

            var confirmDeleteButton = Constant.Constant.WEBDRIVER.FindElement(By.XPath("//div[@class='modal-content' and descendant::div[text()='Delete Book']]//button[contains(@id, 'ok')]"));
            confirmDeleteButton.Click();
            Thread.Sleep(3000);

            IAlert alert = Constant.Constant.WEBDRIVER.SwitchTo().Alert();
            String alertText = Constant.Constant.WEBDRIVER.SwitchTo().Alert().Text;
            Assert.AreEqual(alertText, "Book deleted.");
            alert.Accept();

            var deletedBooks = Constant.Constant.WEBDRIVER.FindElements(By.XPath("//div[contains(@class, 'ReactTable')]//div[@class='action-buttons']//a[text()='Learning JavaScript Design Patterns']"));

            Assert.IsTrue(!deletedBooks.Any());

            Thread.Sleep(3000);
        }


        [TearDown]

        public void TearDown()
        {
            Constant.Constant.WEBDRIVER.Quit();
        }

    }
    }

