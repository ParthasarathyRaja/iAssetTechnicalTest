using iAssetTechnicalTest.Repository;
using System;
using System.Web.Mvc;

namespace iAssetTechnicalTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherRepository _weatherRepository;
        //please note that the parameterless constructor doesn't exist here.
        //The dependency object WeatherRespository is getting injected using NInject.
        public HomeController(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetCitiesByCountry(string country)
        {
            try
            {
                string jsonString = _weatherRepository.GetCitiesByCountry(country);
                return Json(jsonString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                //Just pass that a exception is happend here and show user friendly message in UI. I am showing the message in red at the top of the page.
                //For developers to trace the issue, We can log the expcetion here using Elmah error log or similar tools.
                return Json(false);
            }
        }

        [HttpGet]
        public JsonResult GetWeather(string city, string country)
        {
            try
            {
                string weatherInfo = _weatherRepository.GetWeather(city, country);
                return Json(weatherInfo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                //Just pass that a exception is happend here and show user friendly message in UI. I am showing the message in red at the top of the page.
                //For developers to trace the issue, We can log the expcetion here using Elmah error log or similar tools.
                return Json(false);
            }
        }
        public ActionResult ServerError500()
        {
            //Custom error has been enabled web.config. So server errors will be redirected to the view called Error
            return View("Error");
        }

        public ActionResult FileNotFound404()
        {
            //Custom error has been enabled web.config. So file not found errors will be redirected to the view called Error
            ViewBag.Message = "The URL of the page you are trying to view is incorrect. Please retry or contact the administrator.";
            return View("Error");
        }
    }
}