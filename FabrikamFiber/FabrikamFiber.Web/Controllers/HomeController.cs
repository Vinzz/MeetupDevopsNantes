namespace FabrikamFiber.Web.Controllers
{
    using System.Web.Mvc;

    using FabrikamFiber.DAL.Data;
    using FabrikamFiber.Web.ViewModels;
    using System.Collections.Generic;
    using DAL.Models;
    using System;
    using System.Reflection;
    using System.IO;
    using FFServices;

    public class HomeController : Controller
    {
        private readonly IServiceWeather serviceWeather;
        private DashboardViewModel viewModel = null;

        public HomeController(
                              IServiceWeather serviceWeather)
        {
            this.serviceWeather = serviceWeather;
        }

        public ActionResult Index()
        {
            if (viewModel == null)
            {
                string initCity = "Nantes";

                viewModel = new DashboardViewModel
                {
                    City = initCity,
                    Meteo = serviceWeather.GetWeather(initCity)
                };
            }


            ViewBag.BuildDate = RetrieveLinkerTimestamp();
            ViewBag.Version = GetAssemblyVersion();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ComputeWeather(string input)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(input))
            {
                viewModel.City = input;
                viewModel.Meteo = serviceWeather.GetWeather(input);
            }
            return View("Index", viewModel);
        }

        private dynamic GetAssemblyVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        /// <summary>
        /// Retrieves the linker timestamp.
        /// </summary>
        /// <returns>The date on which the assembly was built</returns>
        public static DateTime RetrieveLinkerTimestamp()
        {
            string filePath = Assembly.GetExecutingAssembly().Location;
            const int CPeHeaderOffset = 60;
            const int CLinkerTimestampOffset = 8;
            byte[] b = new byte[2048];
            Stream s = null;

            try
            {
                s = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                s.Read(b, 0, 2048);
            }
            finally
            {
                if (s != null)
                {
                    s.Close();
                }
            }

            int i = BitConverter.ToInt32(b, CPeHeaderOffset);
            int secondsSince1970 = BitConverter.ToInt32(b, i + CLinkerTimestampOffset);
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);
            dt = dt.AddSeconds(secondsSince1970);


            dt = dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours);

            DateTime parisTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dt, "Romance Standard Time");

            return parisTime;
        }
    }
}
