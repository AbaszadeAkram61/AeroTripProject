using AeroTripProject.Application.Dtos.Destination;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace AeroTripProject.WebUI.Controllers
{
    public class DestinationController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        public DestinationController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClient.CreateClient();
            var responsemessage = await client.GetAsync("https://localhost:7051/api/Destinations");
            if (responsemessage.IsSuccessStatusCode)
            {
                var json = await responsemessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultDestination>>(json);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DestinationDetails(int Id)
        {
            var client = _httpClient.CreateClient();
            var responsemessage = await client.GetAsync($"https://localhost:7051/api/Destinations/{Id}"); ;
            if (responsemessage.IsSuccessStatusCode)
            {
                var json = await responsemessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultDestination>(json);
                return View(value);
            }
            return View();
        }

    }
}
