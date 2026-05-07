using AeroTripProject.Application.Dtos.Destination;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AeroTripProject.WebUI.Areas.Member.Controllers
{
    [Area("Member")]
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
    }
}
