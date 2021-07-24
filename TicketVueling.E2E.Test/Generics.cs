using System;
using System.Threading;
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

        public enum Place
        {
            ORIGIN = 0,
            DESTINATION,
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

        public void ChoosePlace(Place origin_destin, string placeName, int timeout)
        {
            // default origin
            string id_origin_dest = "origin";
            string id_box = "AvailabilitySearchInputSearchView_TextBoxMarketOrigin1";

            if (origin_destin == Place.DESTINATION)
            {
                id_origin_dest = "destination";
                id_box = "AvailabilitySearchInputSearchView_TextBoxMarketDestination1";
            }

            this.ClickPlaceElement(id_origin_dest, timeout);
            this.WriteBox(id_box, placeName, timeout);
        }

        public void Close()
        {
            this.webDriver.Close();
            this.webDriver.Quit();
        }

        private void ClickPlaceElement(string id_origin_dest, int timeout)
        {
            this.webDriver.FindElement(By.Id(id_origin_dest)).Click();

            new WebDriverWait(this.webDriver, TimeSpan.FromSeconds(timeout))
            .Until(SeleniumExtras.WaitHelpers.ExpectedConditions
            .ElementIsVisible(By.Id("stationsList")));
        }

        private void WriteBox(string id, string place, int timeout)
        {
            this.webDriver.FindElement(By.Id(id)).SendKeys(place);

            Thread.Sleep(450); // pending to fix

            this.webDriver.FindElement(By.Id(id)).SendKeys(Keys.Tab);
        }
    }
}
