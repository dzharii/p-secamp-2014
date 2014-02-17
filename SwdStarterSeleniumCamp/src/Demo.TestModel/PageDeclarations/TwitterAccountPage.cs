
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
#endregion
namespace Demo.TestModel.PageDeclarations
{
    /// <summary>
    /// MainPage - Settings - Account Page
    /// First page on the Settings area
    /// </summary>
    public class TwitterAccountPage : MyPageBase
    {
        #region WebElements

        [FindsBy(How = How.XPath, Using = @"id(""user_screen_name"")")]
        protected IWebElement txtUsername { get; set; }


        [FindsBy(How = How.XPath, Using = @"id(""user_email"")")]
        protected IWebElement txtEmail { get; set; }


        [FindsBy(How = How.XPath, Using = @"id(""user_lang"")")]
        protected IWebElement lstLanguage { get; set; }


        [FindsBy(How = How.XPath, Using = @"id(""user_time_zone"")")]
        protected IWebElement lstTimeZone { get; set; }


        [FindsBy(How = How.XPath, Using = @"id(""user_country"")")]
        protected IWebElement lstCountry { get; set; }


        [FindsBy(How = How.XPath, Using = @"id(""user_nsfw_view"")")]
        protected IWebElement chkDoNotInform { get; set; }


        [FindsBy(How = How.XPath, Using = @"id(""user_nsfw_user"")")]
        protected IWebElement chkMarkMedia { get; set; }


        [FindsBy(How = How.XPath, Using = @"id(""export_button"")")]
        protected IWebElement btnRequestYourArchive { get; set; }


        [FindsBy(How = How.XPath, Using = @"id(""account_deactivate_link"")")]
        protected IWebElement lnkDeactivateMyAccount { get; set; }

        #endregion

        #region Invoke() and IsDisplayed()
        public override void Invoke()
        {
            if (!IsDisplayed())
            {
                var mainPage = MyPages.TwitterMainPage;
                mainPage.Invoke();
                mainPage.GoToSettings();
            }
        }

        public override bool IsDisplayed()
        {
            return lstLanguage.CanBeFound();
        }
        #endregion

        public override void VerifyExpectedElementsAreDisplayed()
        {
            VerifyElementVisible("txtUsername", txtUsername);
            VerifyElementVisible("txtEmail", txtEmail);
            VerifyElementVisible("lstLanguage", lstLanguage);
            VerifyElementVisible("lstTimeZone", lstTimeZone);
            VerifyElementVisible("lstCountry", lstCountry);
            VerifyElementVisible("chkDoNotInform", chkDoNotInform);
            VerifyElementVisible("chkMarkMedia", chkMarkMedia);
            VerifyElementVisible("btnRequestYourArchive", btnRequestYourArchive);
            VerifyElementVisible("lnkDeactivateMyAccount", lnkDeactivateMyAccount);
        }

        /// <summary>
        ///  Waits until the Account page is opened 
        /// </summary>
        internal void WaitForOpen()
        {
            lnkDeactivateMyAccount.WaitUntilVisible(TimeSpan.FromSeconds(10));
        }
    }
}