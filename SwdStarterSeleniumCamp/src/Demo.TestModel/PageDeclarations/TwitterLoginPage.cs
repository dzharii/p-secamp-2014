
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
using Swd.Core.Configuration;
#endregion
#region Usings - WebDriver
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;
#endregion
namespace Demo.TestModel.PageDeclarations
{
    /// <summary>
    /// Twitter Login Page
    /// </summary>
    public class TwitterLoginPage : MyPageBase
    {
        #region WebElements

        [FindsBy(How = How.XPath, Using = @"id(""signin-email"")")]
        protected IWebElement txtLogin { get; set; }


        [FindsBy(How = How.XPath, Using = @"id(""signin-password"")")]
        protected IWebElement txtPassword { get; set; }


        [FindsBy(How = How.XPath, Using = @"id(""front-container"")/div[2]/div[2]/form[1]/table[1]/tbody[1]/tr[1]/td[2]/button[1]")]
        protected IWebElement btnLogin { get; set; }


        [FindsBy(How = How.XPath, Using = @"id(""front-container"")/div[2]/div[2]/form[1]/div[2]/label[1]/input[1]")]
        protected IWebElement chkRememberMe { get; set; }

        #endregion

        #region Invoke() and IsDisplayed()

        /// <summary>
        ///  Opens Twitter Login Page by typing 
        ///  URL into browser address box
        /// </summary>
        public override void Invoke()
        {
            if (!IsDisplayed())
            {
                Driver.Url = Config.applicationMainUrl;
            }
        }

        public override bool IsDisplayed()
        {
            return txtLogin.CanBeFound();
        }
        #endregion

        public override void VerifyExpectedElementsAreDisplayed()
        {
            VerifyElementVisible("txtLogin", txtLogin);
            VerifyElementVisible("txtPassword", txtPassword);
            VerifyElementVisible("btnLogin", btnLogin);
            VerifyElementVisible("chkRememberMe", chkRememberMe);
        }


        /// <summary>
        /// Logins to twitter with default Login and Password
        /// Please, see Configuration file for Details
        /// </summary>
        internal void Login()
        {
            txtLogin.Clear();
            txtLogin.SendKeys(Config.twitterLogin);

            txtPassword.Clear();
            txtPassword.SendKeys(Config.twitterPassword);

            btnLogin.Click();
        }
    }
}