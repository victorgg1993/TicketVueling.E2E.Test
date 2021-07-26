using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
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

        public enum FindElement
        {
            ID = 0,
            XPATH,
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
            string id = "radiosBuscador";

            WebDriverWait wait = new WebDriverWait(this.webDriver, TimeSpan.FromSeconds(timeout));
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
            this.WriteBox(id_box, placeName, 450);
        }

        public void ChooseDate(Place origin_destin, string date, int timeout)
        {
            String[] dateParts = date.Split("/");
            string id = "datePickerTitleCloseButton";
            string xpath = ($"//tbody/tr/td[@data-month='{ int.Parse(dateParts[1]) - 1}'][a = '{dateParts[0]}']");

            Thread.Sleep(700);

            new WebDriverWait(this.webDriver, TimeSpan.FromSeconds(timeout))
            .Until(SeleniumExtras.WaitHelpers
            .ExpectedConditions.ElementIsVisible(By.Id(id)));

            webDriver.FindElement(By.XPath(xpath)).Click();
        }

        public void ClickSearchFlyButton(int timeout)
        {
            //Thread.Sleep(3000);
            //ClickOnButton("divButtonBuscadorNormal", timeout);
            string xPath = "//div[@id='divButtonBuscadorNormal']/a";
            this.webDriver.FindElement(By.XPath(xPath)).Click();
        }

        public void fixPossibleUnavailableFly(int timeout) // working
        {
            //-------------------<temporal>
            //Thread.Sleep(1000);
            //string text = "//div[@id='menuTop']/a";
            //webDriver.FindElement(By.XPath(text)).Click();
            //-------------------</temporal>

            //var alertUp = this.webDriver.FindElement(By.XPath("//div[4]/div/div[2]/div[@id='flightCardsContainer']/div[@id='alertMessageNoFlight']"));
            //var alertDown = this.webDriver.FindElement(By.XPath("//div[5]/div/div[2]/div[@id='flightCardsContainer']/div[@id='alertMessageNoFlight']"));

            //while (alertUp.Displayed || alertDown.Displayed)
            //{
            //if (alertUp.Displayed)
            //{
                // agafar la id="tabDay" aria-selected="true" i fer click en la següent
            //}

            //if (alertDown.Displayed)
            //{
                // agafar la id="tabDay" aria-selected="true" i fer click en la següent
            //}
            //}
        }

        public void Close()
        {
            this.webDriver.Close();
            this.webDriver.Quit();
        }



        private void ClickPlaceElement(string id_origin_dest, int timeout)
        {
            //this.webDriver.FindElement(By.Id(id_origin_dest)).Click();
            ClickOnButton(id_origin_dest, timeout);

            new WebDriverWait(this.webDriver, TimeSpan.FromSeconds(timeout))
            .Until(SeleniumExtras.WaitHelpers.ExpectedConditions
            .ElementIsVisible(By.Id("stationsList")));
        }

        private void WriteBox(string id, string place, int timeout)
        {
            this.webDriver.FindElement(By.Id(id)).SendKeys(place);

            Thread.Sleep(timeout); // pending to fix

            this.webDriver.FindElement(By.Id(id)).SendKeys(Keys.Tab);
        }

        private void ClickOnButton(string id, int timeout)
        {
            new WebDriverWait(this.webDriver, TimeSpan.FromSeconds(timeout))
            .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id(id)));

            this.webDriver.FindElement(By.Id(id)).Click();
        }
    }
}

