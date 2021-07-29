using BusinessLayer;
using DataLayer;
using DataModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string error = "";

            Read r = new Read();
            bool res = r.ReadData("..\\netcoreapp3.1\\cities_canada-usa.tsv", out error);

            Assert.IsTrue(res);

        }

        [TestMethod]
        public void GetCities()
        {
            string error = "";
            bool res = false;

            List<Citie> cities = ProcessCities.getCities("q=Beau&lat=53.35013&long=-113.41871", out res, out error);
            //List<Citie> cities = ProcessCities.getCities("q=Beau", out res, out error);

            Assert.IsTrue(res);

        }
    }
}
