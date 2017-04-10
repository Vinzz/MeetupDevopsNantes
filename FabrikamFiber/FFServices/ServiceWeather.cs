using FFServices.Properties;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FFService;

namespace FFServices
{
    public class ServiceWeather : IServiceWeather
    {
        string IServiceWeather.GetWeather(string cityName)
        {
            if (isInFrance(cityName))
            {
                string weatherUrl = string.Format(Settings.Default.weatherquery, cityName);
                string json = JsonRequest.GetRestResponse(weatherUrl);

                WeatherBO o = JsonConvert.DeserializeObject<WeatherBO>(json);

                return o.weather.First().description;
            }
            else
                return "no info for abroad cities";
        }

        public bool isInFrance(string cityName)
        {
           switch(cityName)
            {
                case "Nantes":
                    return true;
                default:
                    return false;
            }
        }
    }
}
