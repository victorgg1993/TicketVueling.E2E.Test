using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace TicketVueling.E2E.Test
{
    [TestClass]
    public class TestTimeFlex
    {
        static IWebDriver driver = new ChromeDriver();
        static string url = "https://tickets.vueling.com/";

        Generics timeFlexTest = new Generics(url, driver);

        [ClassInitialize]
        public static void TestSetup(TestContext context)
        {
        }

        [TestMethod]
        public void RoundTripPurchase()
        {
            timeFlexTest.AcceptCookies(2);
            timeFlexTest.SelectTripOption(Generics.Trip.ROUNDTRIP, 4);

            timeFlexTest.ChoosePlace(Generics.Place.ORIGIN, "Barcelona", 4);
            timeFlexTest.ChoosePlace(Generics.Place.DESTINATION, "Tel Aviv", 4);

            timeFlexTest.ChooseDate(Generics.Place.ORIGIN, "30/07/21", 4);
            timeFlexTest.ChooseDate(Generics.Place.DESTINATION, "26/08/21", 4);

            timeFlexTest.ClickSearchFlyButton(4);

            int originPrice = timeFlexTest.GetFlyTicketPrice(Generics.Place.ORIGIN, 4);
            timeFlexTest.ChooseFlyTicket(Generics.Place.ORIGIN, 4);

            int destinationPrice = timeFlexTest.GetFlyTicketPrice(Generics.Place.DESTINATION, 4);
            timeFlexTest.ChooseFlyTicket(Generics.Place.DESTINATION, 4);

            timeFlexTest.SelectPlanOption(Generics.Plan.OPTIMA, 4);
            timeFlexTest.ClickContinueButton(15);

            //check the price
            //Assert.IsTrue(1==2);
        }

        [TestCleanup]
        public void TearDown()
        {
            timeFlexTest.Close();
        }
    }
}
