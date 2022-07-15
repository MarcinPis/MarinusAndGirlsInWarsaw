using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using BlazorApp.Shared;


namespace BlazorApp.Api
{
    public static class WarsawWeatherFunction
    {

        [FunctionName("WarsawWeather")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Weather API starts here
            string baseUrl = "https://api.tomorrow.io/v4/timelines?";
            string location = "location=52.232583%2C%2021.024333&";
            string fields = "fields=temperatureMin&fields=temperatureMax&fields=temperatureApparent&fields=cloudCover&fields=weatherCode&fields=precipitationIntensity&fields=precipitationType&fields=windSpeed&fields=precipitationProbability&";
            string units = "units=metric&";
            string timestampString = "timesteps=1h&";
            string startTime = "startTime=2022-07-15T06%3A00%3A00Z&";
            string endTime = "endTime=2022-07-17T12%3A00%3A00Z&";
            string timezoneString = "timezone=Europe%2FWarsaw&";
            string apiKey = "apikey=hfxjGpglUVDc5yxQKaH9tIObNDTJAYZx";

            var clientHandler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip,
            };
            var client = new HttpClient(clientHandler);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(baseUrl + location + fields + units + timestampString  + endTime + timezoneString + apiKey),
                Headers =
                {
                    { "Accept", "application/json" },
                },
            };
                var response = await client.SendAsync(request);
            
                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();
                // Console.WriteLine(body);

                // DateTime localDate = DateTime.Now;
                // DateTime utcDate = DateTime.UtcNow;
                // String[] cultureNames = { "en-US", "en-GB", "de-DE"} ;

                // foreach (var cultureName in cultureNames) {
                //     var culture = new CultureInfo(cultureName);
                //     Console.WriteLine("{0}:", culture.NativeName);
                //     Console.WriteLine("   Local date and time: {0}, {1:G}",
                //                     localDate.ToString(culture), localDate.Kind);
                //     Console.WriteLine("   UTC date and time: {0}, {1:G}\n",
                //                     utcDate.ToString(culture), utcDate.Kind);
                // }

            // var warsawWeather = new List<WarsawWeather>();
            // warsawWeather = System.Text.Json.JsonSerializer.Deserialize<List<WarsawWeather>>(body);
            // Console.WriteLine(warsawWeather);

            var warsawWeather = System.Text.Json.JsonSerializer.Deserialize<WarsawWeather>(body);
            

            // var result = Enumerable.Range(1, 6).Select(index => new WarsawWeather
            // {
            //     startTime = DateTime.Now.AddDays(index),
            //     temperatureMax = 39.99,
            //     weatherCode = 1001
            // }).ToArray();

            return new OkObjectResult(warsawWeather.data.timelines[0].intervals);
        }
    }

}
