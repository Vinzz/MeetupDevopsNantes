using NUnit.Framework;
using FFServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFServices.Tests
{
    [TestFixture()]
    public class ServiceWeatherTests
    {
        [Test()]
        public void isInFranceTest()
        {
            Assert.IsTrue(new ServiceWeather().isInFrance("Nantes"));
        }
    }
}