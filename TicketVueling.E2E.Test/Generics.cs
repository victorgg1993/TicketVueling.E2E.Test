using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TicketVueling.E2E.Test
{
    public class Generics
    {
        IWebDriver webDriver;

        public Generics(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public void AcceptCookies(int timeout)
        {
            WebDriverWait wait = new WebDriverWait(this.webDriver, TimeSpan.FromSeconds(timeout));

            wait.Until(driver => driver.FindElement(By.Id("ensBannerDescription")));

            var button = this.webDriver.FindElement(By.Id("ensCloseBanner"));

            if (button.Displayed)
            {
                button.Click();
            }
        }

    }
}
