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

        Generics basicTest = new Generics(url, driver);

        [ClassInitialize]
        public static void TestSetup(TestContext context)
        {
        }

        [TestMethod]
        public void RoundTripPurchase()
        {
            basicTest.AcceptCookies(2);
            basicTest.SelectTripOption(Generics.Trip.ROUNDTRIP, 4);

            basicTest.ChoosePlace(Generics.Place.ORIGIN, "Barcelona", 4);
            basicTest.ChoosePlace(Generics.Place.DESTINATION, "Tel Aviv", 4);

            basicTest.ChooseDate(Generics.Place.ORIGIN, "30/07/21", 4);
            basicTest.ChooseDate(Generics.Place.DESTINATION, "26/08/21", 4);

            basicTest.ClickSearchFlyButton(4);

            int originPrice = basicTest.GetFlyTicketPrice(Generics.Place.ORIGIN, 4);
            basicTest.ChooseFlyTicket(Generics.Place.ORIGIN, 4);

            int destinationPrice = basicTest.GetFlyTicketPrice(Generics.Place.DESTINATION, 4);
            basicTest.ChooseFlyTicket(Generics.Place.DESTINATION, 4);

            basicTest.SelectPlanOption(Generics.Plan.BASIC, 4);
            basicTest.ClickContinueButton(15);

            //check the price
            //Assert.IsTrue(1==2);
        }

        [TestMethod]
        public void OneWayPurchase()
        {
            basicTest.AcceptCookies(2);
            basicTest.SelectTripOption(Generics.Trip.ONEWAY, 4);

            basicTest.ChooseNumberOfAdults(2, 4);
            basicTest.ChooseNumberOfKids(1, 4);

            basicTest.ChoosePlace(Generics.Place.ORIGIN, "Barcelona", 4);
            basicTest.ChoosePlace(Generics.Place.DESTINATION, "Tel Aviv", 4);

            basicTest.ChooseDate(Generics.Place.ORIGIN, "30/07/21", 4);
            basicTest.ClickSearchFlyButton(4);

            int originPrice = basicTest.GetFlyTicketPrice(Generics.Place.ORIGIN, 4);
            basicTest.ChooseFlyTicket(Generics.Place.ORIGIN, 4);

            basicTest.SelectPlanOption(Generics.Plan.BASIC, 4);
            basicTest.ClickContinueButton(15);
        }

        [TestCleanup]
        public void TearDown()
        {
            basicTest.Close();
        }
    }
}
