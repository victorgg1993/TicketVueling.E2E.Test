﻿using System;
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
            this.WriteBox(id_box, placeName, 450);
        }

        public void ChooseDate(Place origin_destin, string date, int timeout)
        {
            // temporary disabled, it just closes the date pop-up
            // and picks the default dates
            string id = "datePickerTitleCloseButton";

            new WebDriverWait(this.webDriver, TimeSpan.FromSeconds(timeout))
            .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id(id)));

            webDriver.FindElement(By.Id(id)).Click();

            //string origin_table = $"/html/body/div[11]/div/div/div[1]/table";
            // div / ui-datepicker-group ui-datepicker-group-first
            // div / ui-datepicker-group ui-datepicker-group-last
            //var _n = InitDebug();
            //IWebElement dateTable = webDriver.FindElement((By.XPath(origin_table)));

            // tmp
            //List<IWebElement> lstTdElem = new List<IWebElement>(dateTable.FindElements(By.TagName("td")));
            //List<String> textos_tmp = new List<String>();

            //if (lstTdElem.Count > 0)
            //{
                //foreach (var elemTd in lstTdElem)
                //{
                //    var t = elemTd.Text + "/" + elemTd.GetAttribute("data-month") + "/" + elemTd.GetAttribute("data-year");
                //
                //    if (t.Length > 5)
                //    {
                //        textos_tmp.Add(t);
                //    }
                //}
            //}

            // check if dates exists
            // if doesn't, pick some random one

            // tmp
            //foreach (var algo in textos_tmp)
            //{
                //var _nada = DebugWrite(algo + "\r\n");
            //}
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

            Thread.Sleep(timeout); // pending to fix

            this.webDriver.FindElement(By.Id(id)).SendKeys(Keys.Tab);
        }


        private static async Task InitDebug()
        {
            await File.WriteAllTextAsync("C:/Users/holacons/Documents/2_QA/1_Examens/finde1/TicketVueling.E2E.Test/salida.txt", "");
        }

        private static async Task DebugWrite(string texto)
        {
            await File.AppendAllTextAsync("C:/Users/holacons/Documents/2_QA/1_Examens/finde1/TicketVueling.E2E.Test/salida.txt", texto);
        }

    }
}
