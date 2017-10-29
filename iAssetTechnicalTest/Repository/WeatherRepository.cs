using iAssetTechnicalTest.GlobalWeather;
using Newtonsoft.Json;
using System;
using System.Xml;

namespace iAssetTechnicalTest.Repository
{
    public interface IWeatherRepository
    {
        string GetCitiesByCountry(string country);
        string GetWeather(string city, string country);
    }
    
    public  class WeatherRepository : IWeatherRepository
    {
        public string GetCitiesByCountry(string country)
        {
            string jsonString = string.Empty;
            GlobalWeatherSoapClient weatherClient = null;

            try
            {
                weatherClient = new GlobalWeather.GlobalWeatherSoapClient();
                
                //calling and storing web service output into the variable.
                string citiesString = weatherClient.GetCitiesByCountry(country);

                //make sure the cities string has valid value
                if (!string.IsNullOrEmpty(citiesString) && citiesString.Trim() != string.Empty)
                {
                    var xml = new XmlDocument();
                    xml.LoadXml(citiesString);
                    jsonString = JsonConvert.SerializeXmlNode(xml);
                }

            }
            catch(Exception ex)
            {
                //Log the error here and throw the exception
                throw ex;
            }
            return jsonString;
        }

        public string GetWeather(string city, string country)
        {
            string jsonString = string.Empty;
            GlobalWeatherSoapClient weatherClient = null;

            try
            {
                weatherClient = new GlobalWeather.GlobalWeatherSoapClient();

                //calling and storing web service output into the variable  
                string weatherString = weatherClient.GetWeather(city, country);

                //make sure the weather string has valid value
                if (!string.IsNullOrEmpty(weatherString) && weatherString.Trim() != string.Empty && weatherString != "Data Not Found")
                {
                    var xml = new XmlDocument();
                    xml.LoadXml(weatherString);
                    jsonString = JsonConvert.SerializeXmlNode(xml);
                }
            }
            catch (Exception ex)
            {
                //log the error here and throw the exception
                throw ex;
            }
            return jsonString;
        }
    }
}