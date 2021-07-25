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
            basicTest.SelectTripOption(Generics.Trip.MULTIPLE, 4);
            
            basicTest.ChoosePlace(Generics.Place.ORIGIN, "Barcelona", 4);
            basicTest.ChoosePlace(Generics.Place.DESTINATION, "Tel Aviv", 4);
            
            basicTest.ChooseDate(Generics.Place.ORIGIN, "22/09/21", 4);
            basicTest.ChooseDate(Generics.Place.DESTINATION, "25/09/21", 4);
            
            basicTest.ClickSearchFlyButton(4);
            
            //choose the first origin fly
            //choose the first destiny fly
            //select basic option
            //click continue button
            //check the price

            //Assert.IsTrue(1==2);
        }

        /*
            [TestMethod]
            public void OneWayPurchase()
            {
            }
        */

        [TestCleanup]
        public void TearDown()
        {
            //basicTest.Close();
        }
    }
}
