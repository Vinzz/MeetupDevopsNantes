using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFServices
{
    public class ServiceWeatherProxyMock : IServiceWeather
    {
        public string GetWeather(string cityName)
        {
            return "Proxy!";
        }

        public bool IsServiceUp()
        {
            return true;
        }
    }
}
