using MyWeather.Models;
using System.Net.Http;
using System.Threading.Tasks;
using static Newtonsoft.Json.JsonConvert;

namespace MyWeather.Services
{
    public enum Units
    {
        Imperial,
        Metric
    }

    public class WeatherService
    {
        //const string WeatherCoordinatesUri = "http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&units={2}&appid=fc9f6c524fc093759cd28d41fda89a1b";
        const string WeatherCoordinatesUri = "http://slg-compass360-azure-api.azurewebsites.net/api/location/weather?latitude={0}&longitude={1}";

        //const string WeatherCityUri = "http://api.openweathermap.org/data/2.5/weather?q={0}&units={1}&appid=fc9f6c524fc093759cd28d41fda89a1b";
        const string WeatherCityUri = "http://slg-compass360-azure-api.azurewebsites.net/api/city/weather?city={0}&units={1}";

        //const string ForecaseUri = "http://api.openweathermap.org/data/2.5/forecast?id={0}&units={1}&appid=fc9f6c524fc093759cd28d41fda89a1b";
        const string ForecastByNameUri = "http://slg-compass360-azure-api.azurewebsites.net/api/city/forecast?city={0}&units={1}";
        const string ForecastUri = "http://slg-compass360-azure-api.azurewebsites.net/api/location/forecast?id={0}&units={1}";

        public async Task<WeatherRoot> GetWeather(double latitude, double longitude, Units units = Units.Metric)
        {
            using (var client = new HttpClient())
            {
                //var url = string.Format(WeatherCoordinatesUri, latitude, longitude, units);
                var url = string.Format(WeatherCoordinatesUri, latitude, longitude);
                var json = await client.GetStringAsync(url);

                if (string.IsNullOrWhiteSpace(json))
                    return null;

                return DeserializeObject<WeatherRoot>(json);
            }

        }


        public async Task<WeatherRoot> GetWeather(string city, Units units = Units.Imperial)
        {
            using (var client = new HttpClient())
            {
                //var url = string.Format(WeatherCityUri, city, units.ToString().ToLower());
                var url = string.Format(WeatherCityUri, city, units);
                var json = await client.GetStringAsync(url);

                if (string.IsNullOrWhiteSpace(json))
                    return null;

                return DeserializeObject<WeatherRoot>(json);
            }

        }

        public async Task<WeatherForecastRoot> GetForecast(int id, Units units = Units.Imperial)
        {
            using (var client = new HttpClient())
            {
                //var url = string.Format(ForecaseUri, id, units.ToString().ToLower());
                var url = string.Format(ForecastUri, id, units);

                var json = await client.GetStringAsync(url);

                if (string.IsNullOrWhiteSpace(json))
                    return null;

                return DeserializeObject<WeatherForecastRoot>(json);
            }

        }

        public async Task<WeatherForecastRoot> GetForecast(string city, Units units = Units.Metric)
        {
            using (var client = new HttpClient())
            {
                //var url = string.Format(ForecaseUri, id, units.ToString().ToLower());
                var url = string.Format(ForecastByNameUri, city, units);
                var json = await client.GetStringAsync(url);

                if (string.IsNullOrWhiteSpace(json))
                    return null;

                return DeserializeObject<WeatherForecastRoot>(json);
            }

        }

    }
}
