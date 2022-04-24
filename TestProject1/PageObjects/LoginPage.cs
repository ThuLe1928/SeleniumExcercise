using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQADotComTest.PageObjects
{
    class LoginPage : GeneralPage
    {
        private By usernameTextBox = By.Id("userName");
        private By passwordTextBox = By.Id("password");

        protected IWebElement getUsernameTexBox()
        {
            return Constant.Constant.WEBDRIVER.FindElement(usernameTextBox);
        }

        protected IWebElement getPasswordTextBox()
        {
            return Constant.Constant.WEBDRIVER.FindElement(passwordTextBox);
        }

        public void login(String username, String password)
        {
            this.getUsernameTexBox().SendKeys(username);
            this.getPasswordTextBox().SendKeys(password);
            this.scrollElementIntoView(this.getLoginButton());
            this.getLoginButton().Click();
        }

    }
}
