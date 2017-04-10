using NUnit.Framework;
using FabrikamFiber.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using FFServices;

namespace FabrikamFiber.Web.Controllers.Tests
{
    [TestFixture()]
    public class HomeControllerTests
    {
        [Test()]
        public void ComputeWeatherTest()
        {
            var mock = new Mock<IServiceWeather>();
            mock.Setup(x => x.IsServiceUp()).Returns(true);

            HomeController hc = new HomeController(mock.Object);

            Assert.IsTrue(hc.ComputeWeather("Lille") != null);
        }
    }
}