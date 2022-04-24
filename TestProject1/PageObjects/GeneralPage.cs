using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQADotComTest.PageObjects
{
    class GeneralPage
    {
        private By usernameLabel = By.Id("userName-value");
        private By loginButton = By.Id("login");

        public IWebElement getLoginButton()
        {
            return Constant.Constant.WEBDRIVER.FindElement(loginButton);
        }

        public IWebElement getUsernameLable()
        {
            return Constant.Constant.WEBDRIVER.FindElement(usernameLabel);
        }

        public void scrollElementIntoView(IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Constant.Constant.WEBDRIVER;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }
        public void goToLoginPage()
        {
            this.getLoginButton().Click();
        }

        public String getUsernameLabelValue()
        {
            return this.getUsernameLable().Text;
        }

    }
}
