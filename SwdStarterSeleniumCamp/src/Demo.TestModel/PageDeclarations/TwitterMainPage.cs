
#region Usings - System
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion
#region Usings - SWD
using Swd.Core;
using Swd.Core.Pages;
using Swd.Core.WebDriver;
#endregion
#region Usings - WebDriver
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
#endregion
namespace Demo.TestModel.PageDeclarations
{
    public class TwitterMainPage : MyPageBase
    {
        #region WebElements

        [FindsBy(How = How.XPath, Using = @"id(""user-dropdown-toggle"")")]
        protected IWebElement btnSettings { get; set; }


        [FindsBy(How = How.CssSelector, Using = @"a[data-nav=""settings""]")]
        protected IWebElement lblSettingsMenuItem { get; set; }

        #endregion

        #region Invoke() and IsDisplayed()
        public override void Invoke()
        {
            if (!IsDisplayed())
            {
                var loginPage = MyPages.TwitterLoginPage;

                loginPage.Invoke();

                loginPage.Login();
            }
        }

        public override bool IsDisplayed()
        {
            return btnSettings.CanBeFound();
        }
        #endregion

        public override void VerifyExpectedElementsAreDisplayed()
        {
            VerifyElementVisible("btnSettings", btnSettings);
        }

        internal void GoToSettings()
        {
            btnSettings.Click();

            lblSettingsMenuItem.WaitUntilVisible()
                                .Click();

            MyPages.TwitterAccountPage.WaitForOpen();
        }
    }
}