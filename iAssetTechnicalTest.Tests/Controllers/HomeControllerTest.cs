using iAssetTechnicalTest.Controllers;
using iAssetTechnicalTest.Repository;
using iAssetTechnicalTest.Tests.GlobalWeatherTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Xml;

namespace iAssetTechnicalTest.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            //Create an instance for GlobalWeather. Use the service class straight and check the DI classes
            IWeatherRepository weatherClient = new WeatherRepository();

            //Arrange
            HomeController controller = new HomeController(weatherClient);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert - Test 1 - test view is not null.
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCitiesByCountryTest()
        {
            //Create an instance for GlobalWeather. Use the service class straight and check the DI classes
            IWeatherRepository weatherClient = new WeatherRepository();

            //Arrange
            HomeController controller = new HomeController(weatherClient);

            //Assert - Test 1 - test for not null return value when valid country name is passed
            JsonResult result1 = controller.GetCitiesByCountry("Australia");
            Assert.IsNotNull(result1);

            //Assert - Test 2 - comaparison test for valid results of country
            GlobalWeatherSoapClient weatherClientTest = new GlobalWeatherSoapClient();
            string citiesList = weatherClientTest.GetCitiesByCountry("Australia");
            var xml = new XmlDocument();
            xml.LoadXml(citiesList);
            string jsonString = JsonConvert.SerializeXmlNode(xml);

            Assert.AreEqual(result1.Data, jsonString);

            //Assert - Test 3 - test with wrong data. pass city instead of country. make sure nothing breaks
            JsonResult result2 = controller.GetCitiesByCountry("sydney");
            Assert.IsNotNull(result2);
        }

        [TestMethod]
        public void GetWeatherTest()
        {
            //Create an instance for GlobalWeather. Use the service class straight and check the DI classes
            IWeatherRepository weatherClient = new WeatherRepository();

            //Arrange
            HomeController controller = new HomeController(weatherClient);

            //Assert - Test 1 - test for not null return value when valid country name is passed
            JsonResult result1 = controller.GetWeather("Sydney Airport", "Australia");
            Assert.IsNotNull(result1);

            //Assert - Test 2 - comaparison test for valid results of country
            GlobalWeatherSoapClient weatherClientTest = new GlobalWeatherSoapClient();
            string citiesList = weatherClientTest.GetWeather("Sydney Airport", "Australia");
            if (citiesList == "Data Not Found")
            {
                citiesList = "";
            }
            Assert.AreEqual(result1.Data, citiesList);

            //Assert - Test 3 - test with wrong data. pass city instead of country. make sure nothing breaks
            JsonResult result2 = controller.GetWeather("Australia", "Sydney Airport");
            Assert.IsNotNull(result2);
        }
    }

}
