using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace TicketVueling.E2E.Test
{
    [TestClass]
    public class TestBasic
    {
        string url = "https://tickets.vueling.com/";

        [ClassInitialize]
        public static void TestSetup(TestContext context)
        {

        }

        [TestMethod]
        public void RoundTripPurchase()
        {
            Generics basicTest = new Generics(url, new FirefoxDriver());
            basicTest.AcceptCookies(2);
            //select roundtrip option
            //choose origin
            //choose destiny
            //choose init date
            //choose end date
            //click search flies
            //choose the first origin fly
            //choose the first destiny fly
            //select basic option
            //click continue button
            //check the price

            //Assert.IsTrue(1==2);
        }

        [TestMethod]
        public void OneWayPurchase()
        {
        }

        [TestCleanup]
        public void TearDown()
        {
        }
    }
}
