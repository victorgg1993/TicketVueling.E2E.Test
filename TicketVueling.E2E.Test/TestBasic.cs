using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace TicketVueling.E2E.Test
{
    [TestClass]
    public class TestBasic
    {
        static IWebDriver driver = new ChromeDriver();
        static string url = "https://tickets.vueling.com/";

        Generics test = new Generics(url, driver);

        [ClassInitialize]
        public static void TestSetup(TestContext context)
        {
        }

        [TestMethod]
        public void RoundTripPurchase()
        {
            const string placeOrigin = "Barcelona";
            const string placeDestination = "Tel Aviv";

            const string dateOrigin = "30/07/21";
            const string dateDestination = "26/08/21";

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

            test.SelectPlanOption(Generics.Plan.BASIC, test.DEFAULT_TIMEOUT);
            test.ClickContinueButton(test.LONG_TIMEOUT);

            //check the price
            //Assert.IsTrue(1==2);
        }

        [TestMethod]
        public void OneWayPurchase()
        {
            const string placeOrigin = "Ancona";
            const string placeDestination = "Burdeos";

            const string dateOrigin = "23/8/21";

            test.AcceptCookies(test.COOKIE_TIMEOUT);
            test.SelectTripOption(Generics.Trip.ONEWAY, test.DEFAULT_TIMEOUT);

            test.ChooseNumberOfAdults(2, test.DEFAULT_TIMEOUT);
            test.ChooseNumberOfKids(1, test.DEFAULT_TIMEOUT);

            test.ChoosePlace(Generics.Place.ORIGIN, placeOrigin, test.DEFAULT_TIMEOUT);
            test.ChoosePlace(Generics.Place.DESTINATION, placeDestination, test.DEFAULT_TIMEOUT);

            test.ChooseDate(Generics.Place.ORIGIN, dateOrigin, test.DEFAULT_TIMEOUT);
            test.ClickSearchFlyButton(test.DEFAULT_TIMEOUT);

            int originPrice = test.GetFlyTicketPrice(Generics.Place.ORIGIN, test.DEFAULT_TIMEOUT);
            test.ChooseFlyTicket(Generics.Place.ORIGIN, test.DEFAULT_TIMEOUT);

            test.SelectPlanOption(Generics.Plan.BASIC, test.DEFAULT_TIMEOUT);
            test.ClickContinueButton(test.LONG_TIMEOUT);
        }

        [TestCleanup]
        public void TearDown()
        {
            test.Close();
        }
    }
}
