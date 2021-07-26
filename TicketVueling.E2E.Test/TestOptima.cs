using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace TicketVueling.E2E.Test
{
    [TestClass]
    public class TestOptima
    {
        static IWebDriver driver = new ChromeDriver();
        static string url = "https://tickets.vueling.com/";

        Generics optimaTest = new Generics(url, driver);

        [ClassInitialize]
        public static void TestSetup(TestContext context)
        {
        }

        [TestMethod]
        public void RoundTripPurchase()
        {
            optimaTest.AcceptCookies(2);
            optimaTest.SelectTripOption(Generics.Trip.ROUNDTRIP, 4);

            optimaTest.ChoosePlace(Generics.Place.ORIGIN, "Barcelona", 4);
            optimaTest.ChoosePlace(Generics.Place.DESTINATION, "Tel Aviv", 4);

            optimaTest.ChooseDate(Generics.Place.ORIGIN, "30/07/21", 4);
            optimaTest.ChooseDate(Generics.Place.DESTINATION, "26/08/21", 4);

            optimaTest.ClickSearchFlyButton(4);

            int originPrice = optimaTest.GetFlyTicketPrice(Generics.Place.ORIGIN, 4);
            optimaTest.ChooseFlyTicket(Generics.Place.ORIGIN, 4);

            int destinationPrice = optimaTest.GetFlyTicketPrice(Generics.Place.DESTINATION, 4);
            optimaTest.ChooseFlyTicket(Generics.Place.DESTINATION, 4);

            optimaTest.SelectPlanOption(Generics.Plan.OPTIMA, 4);
            optimaTest.ClickContinueButton(15);

            //check the price
            //Assert.IsTrue(1==2);
        }

        [TestMethod]
        public void OneWayPurchase()
        {
            optimaTest.AcceptCookies(2);
            optimaTest.SelectTripOption(Generics.Trip.ONEWAY, 4);

            optimaTest.ChooseNumberOfAdults(2, 4);
            optimaTest.ChooseNumberOfKids(1, 4);

            optimaTest.ChoosePlace(Generics.Place.ORIGIN, "Ancona", 4);
            optimaTest.ChoosePlace(Generics.Place.DESTINATION, "Burdeos", 4);

            optimaTest.ChooseDate(Generics.Place.ORIGIN, "23/8/21", 4);
            optimaTest.ClickSearchFlyButton(4);

            int originPrice = optimaTest.GetFlyTicketPrice(Generics.Place.ORIGIN, 4);
            optimaTest.ChooseFlyTicket(Generics.Place.ORIGIN, 4);

            optimaTest.SelectPlanOption(Generics.Plan.OPTIMA, 4);
            optimaTest.ClickContinueButton(15);
        }

        [TestCleanup]
        public void TearDown()
        {
            optimaTest.Close();
        }
    }
}
