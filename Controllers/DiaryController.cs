using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace MyWebApp.Controllers
{
    public class DiaryController : Controller
    {
        // 仮のデータストア
        private static List<DiaryEntry> Entries = new List<DiaryEntry>();

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _weatherApiKey;

        public DiaryController(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _weatherApiKey = config["WeatherApi:Key"];
        }

        public IActionResult Index()
        {
            var viewModel = new DiaryWeatherViewModel
            {
                Diaries = Entries.OrderByDescending(e => e.Date).ToList(),
                Weather = null // 初期状態では天気情報はnull
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Add(string content)
        {
            if (!string.IsNullOrWhiteSpace(content))
            {
                Entries.Add(new DiaryEntry { Id = Entries.Count + 1, Date = DateTime.Now, Content = content });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> GetWeather(string city)
        {
            // if (string.IsNullOrWhiteSpace(city))
            // {
            //     return BadRequest("City name cannot be empty.");
            // }
            string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?lat=44.34&lon=10.99&appid={_weatherApiKey}";

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Error fetching weather data.");
            }
            var jsonString = await response.Content.ReadAsStringAsync();
            using var jsonDoc = JsonDocument.Parse(jsonString);
            var root = jsonDoc.RootElement;
            var locationName = root.GetProperty("name").GetString();
            var conditionText = root.GetProperty("weather")[0].GetProperty("description").GetString();
            var tempC = root.GetProperty("main").GetProperty("temp").GetDouble();
            var viewModel = new DiaryWeatherViewModel
            {
                Diaries = Entries.OrderByDescending(e => e.Date).ToList(),
                Weather = new WeatherInfo
                {
                    City = locationName,
                    Condition = conditionText,
                    Temperature = tempC
                }
            };
            return View("Index", viewModel);
        }
    }
}