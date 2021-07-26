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

        public enum Plan
        {
            BASIC = 0,
            OPTIMA,
            TIMEFLEX,
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
            //ClickOnButton("divButtonBuscadorNormal", timeout);
            //string xPath = "//div[@id='divButtonBuscadorNormal']/a";
            //this.webDriver.FindElements(By.XPath(xPath))[0].Click();

            Thread.Sleep(1500);
            string id = "AvailabilitySearchInputSearchView_btnClickToSearchNormal";
            this.webDriver.FindElements(By.Id(id))[0].Click();
        }

        public void fixPossibleUnavailableFly(int timeout)
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

        public void ChooseFlyTicket(Place origin_dest, int timeout)
        {
            int index_pos = 0;
            string classString = "";
            string xpath = "//div/label/div[2]/div[1]/div";

            while (!classString.Contains("hidden")) // while page is loading, wait;
            {
                classString = this.webDriver.FindElement(By.Id("vy-stv-loader")).GetAttribute("class");
            }
            Thread.Sleep(400);


            if (origin_dest == Place.DESTINATION) // pick the last one if destination is chosen
            {
                index_pos = this.webDriver.FindElements(By.XPath(xpath)).Count - 1;
            }

            this.webDriver.FindElements(By.XPath(xpath))[index_pos].Click();
            Thread.Sleep(700);
        }

        public void SelectPlanOption(Plan option_plan, int timeout)
        {
            var radio_buttons = this.webDriver.FindElements(By.ClassName("fares-box_radio"));
            radio_buttons[(int)option_plan].Click();
        }

        public void ClickContinueButton(int timeout)
        {
            this.webDriver.FindElement(By.Id("stvContinueButton")).Click();

            new WebDriverWait(this.webDriver, TimeSpan.FromSeconds(timeout))
            .Until(SeleniumExtras.WaitHelpers.ExpectedConditions
            .ElementIsVisible(By.Id("contactAside")));
        }

        public int GetFlyTicketPrice(Place origin_test, int timeout)
        {

            return 0;
        }

        public void ChooseNumberOfAdults(int num, int timeout)
        {
            num = (num - 1) % 3; // temporary limiter

            string[] id =
            {
                "DropDownListPassengerType_ADT_1",
                "DropDownListPassengerType_ADT_2",
                "DropDownListPassengerType_ADT_3"
            };

            this.webDriver.FindElement(By.Id(id[num])).Click();
        }
        public void ChooseNumberOfKids(int num, int timeout)
        {
            string xPath = $"//select[@id='AvailabilitySearchInputSearchView_DropDownListPassengerType_CHD']/option[@value='{num}']";
            this.webDriver.FindElement(By.XPath(xPath)).Click();
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

