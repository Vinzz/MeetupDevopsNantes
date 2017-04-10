using NUnit.Framework;
using FabrikamFiber.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabrikamFiber.Web.Helpers.Tests
{
    [TestFixture()]
    public class CityHelperTests
    {
        [Test()]
        public void IsCityOKTest()
        {
            Assert.IsTrue(CityHelper.IsCityOK("Nantes"));
        }
    }
}