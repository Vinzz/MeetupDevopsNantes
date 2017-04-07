using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFServices
{
    public interface IServiceWeather
    {
        string GetWeather(string cityName);
    }
}
