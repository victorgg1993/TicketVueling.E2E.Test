using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace TicketVueling.E2E.Test
{
    //[TestClass]
    public class TestTimeFlex
    {
        static IWebDriver driver = new ChromeDriver();
        static string url = "https://tickets.vueling.com/";

        readonly string placeOrigin = "Barcelona";
        readonly string placeDestination = "Tel Aviv";

        readonly string dateOrigin = "30/07/21";
        readonly string dateDestination = "26/08/21";

        Generics test = new Generics(url, driver);

        //[ClassInitialize]
        public static void TestSetup(TestContext context)
        {
        }

        //[TestMethod]
        public void RoundTripPurchase()
        {
            test.AcceptCookies(test.COOKIE_TIMEOUT);
            test.SelectTripOption(Generics.Trip.ROUNDTRIP, test.DEFAULT_TIMEOUT);

            test.ChoosePlace(Generics.Place.ORIGIN, placeOrigin, test.DEFAULT_TIMEOUT);
            test.ChoosePlace(Generics.Place.DESTINATION, placeDestination, test.DEFAULT_TIMEOUT);

            test.ChooseDate(Generics.Place.ORIGIN, dateOrigin, test.DEFAULT_TIMEOUT);
            test.ChooseDate(Generics.Place.DESTINATION, dateDestination, test.DEFAULT_TIMEOUT);

            test.ClickSearchFlyButton(test.DEFAULT_TIMEOUT);

            int originPrice = test.GetFlyTicketPrice(Generics.Place.ORIGIN, test.DEFAULT_TIMEOUT);
            test.ChooseFlyTicket(Generics.Place.ORIGIN, test.DEFAULT_TIMEOUT);

            int destinationPrice = test.GetFlyTicketPrice(Generics.Place.DESTINATION, test.DEFAULT_TIMEOUT);
            test.ChooseFlyTicket(Generics.Place.DESTINATION, test.DEFAULT_TIMEOUT);

            test.SelectPlanOption(Generics.Plan.OPTIMA, test.DEFAULT_TIMEOUT);
            test.ClickContinueButton(test.LONG_TIMEOUT);

            //check the price
            //Assert.IsTrue(1==2);
        }

        //[TestCleanup]
        public void TearDown()
        {
            test.Close();
        }
    }
}
