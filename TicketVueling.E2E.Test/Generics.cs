using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TicketVueling.E2E.Test
{

    public class Generics
    {
        private IWebDriver webDriver;

        public enum Trip
        {
            ROUNDTRIP = 0,
            ONEWAY,
            MULTIPLE,
        };

        public Generics(string url, IWebDriver webDriver)
        {
            this.webDriver = webDriver;
            this.webDriver.Url = url;
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

        public void SelectTripOption(Trip n_trip, int timeout)
        {
            WebDriverWait wait = new WebDriverWait(this.webDriver, TimeSpan.FromSeconds(timeout));

            string id = "radiosBuscador";
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id(id)));

            var radioButtons = this.webDriver.FindElements(By.ClassName("elForm_radio--label"));
            radioButtons[(int)n_trip].Click();
        }

        public void Close()
        {
            this.webDriver.Close();
            this.webDriver.Quit();
        }
    }
}
