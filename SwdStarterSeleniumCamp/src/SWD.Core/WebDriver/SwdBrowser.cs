using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.PhantomJS;

using Swd.Core.Configuration;

using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Interactions;


namespace Swd.Core.WebDriver
{

    public static class SwdBrowser
    {
        public static event Action OnDriverStarted;
        public static event Action OnDriverClosed;

        public static Func<IWebDriver> _initializeDriver;
        public static Func<IWebDriver> InitializeDriver
        {
            get { return _initializeDriver; }
            set { _initializeDriver = value; }
        }

        private static IWebDriver _driver = null;
        private static IWebDriver _realDriver = null;

        static SwdBrowser()
        {
            InitializeDriver = () =>
                {
                    _driver = WebDriverRunner.Run(Config.swdBrowserType, Config.wdIsRemote, Config.wdRemoteUrl);
                    return _driver;
                };
        }

        public static IWebDriver RealDriver { get { return _realDriver; } }
        public static IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    Initialize();
                    _realDriver = _driver;

                    var eventFiringDriver = new EventFiringWebDriver(_driver);

                    eventFiringDriver.ElementClicking += eventFiringDriver_HighlightElement;
                    eventFiringDriver.FindElementCompleted += eventFiringDriver_FindElementCompleted;
                    eventFiringDriver.ElementValueChanging += eventFiringDriver_ElementValueChanging;

                    _driver = eventFiringDriver;

                }
                return _driver;
            }
        }


        static void eventFiringDriver_ElementValueChanging(object sender, WebElementEventArgs e)
        {
            HighlightElement(e.Element, "balck");
        }

        static void eventFiringDriver_FindElementCompleted(object sender, FindElementEventArgs e)
        {
            HighlightElement(e.FindMethod, "green");
            System.Threading.Thread.Sleep(500);
        }


        static void eventFiringDriver_HighlightElement(object sender, WebElementEventArgs e)
        {
            HighlightElement(e.Element);
        }

        public static void HighlightElement(By by, string borderColor = "red")
        {
            IJavaScriptExecutor jsExec = SwdBrowser.Driver as IJavaScriptExecutor;
            IList<IWebElement> webElements = SwdBrowser.RealDriver.FindElements(by);
            foreach (var webElement in webElements)
            {
                HighlightElement(webElement, borderColor);
            }

        }

        public static void HighlightElement(IWebElement webElement, string borderColor = "red")
        {
            IJavaScriptExecutor jsExec = SwdBrowser.Driver as IJavaScriptExecutor;
            if (webElement != null)
            {
                Actions acts = new Actions(SwdBrowser.RealDriver);
                acts.MoveToElement(webElement).Build().Perform();
                
                jsExec.ExecuteScript(
                @"
                element = arguments[0];
                original_style = element.getAttribute('style');
                element.setAttribute('style', 'background: yellow !important; border: 3px solid " + borderColor + @" !important;');
                setTimeout(function(){
                    element.setAttribute('style', '');
                }, 1000);

           ", webElement);
            }
            else throw new ArgumentNullException("webElement");
        }



        public static void Initialize()
        {

            if (_driver != null)
            {
                _driver.Quit();
                if (OnDriverClosed != null) OnDriverClosed();
            }

            _driver = InitializeDriver();

            // Fire OnDriverStarted event
            if (OnDriverStarted != null)
            {
                OnDriverStarted();
            }
        }

        public static void CloseDriver()
        {

            if (_driver != null)
            {
                _driver.Dispose();

                // Fire OnDriverClosed
                if (OnDriverClosed != null)
                {
                    OnDriverClosed();

                }
            }
        }


        static readonly Finalizer finalizer = new Finalizer();
        sealed class Finalizer
        {
            ~Finalizer()
            {
                CloseDriver();
            }
        }

        public static object ExecuteScript(string jsCode)
        {
            return (Driver as IJavaScriptExecutor).ExecuteScript(jsCode);
        }

        public static void HandleJavaScriptErrors()
        {
            string jsCode =
            #region JavaScript Error Handler code
            @"
                    if (typeof window.jsErrors === 'undefined') 
                    {
                        window.jsErrors = '';
                        window.onerror = function (errorMessage, url, lineNumber) 
                                         {
                                              var message = 'Error: [' + errorMessage + '], url: [' + url + '], line: [' + lineNumber + ']';
                                              message = message + ""\n"";
                                              window.jsErrors += message;
                                              return false;
                                         };
                    }

                    var errors = window.jsErrors;
                    window.jsErrors = '';
                    return errors;";
            #endregion
            
            string errors = "";
            errors = (string)ExecuteScript(jsCode);

            if (!string.IsNullOrEmpty(errors))
            {
                throw new JavaScriptErrorOnThePageException(errors);
            }
        }

        
    }
}
