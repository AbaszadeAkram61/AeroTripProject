using AeroTripProject.Application.Dtos.Destination;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
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
        public IActionResult AddDestination()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDestination(CreateDestination createDestination)
        {
            createDestination.Status = true;
            var json= JsonConvert.SerializeObject(createDestination);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClient.CreateClient();
            var responsemessage=await client.PostAsync("https://localhost:7051/api/Destinations", content);
            var eror=await responsemessage.Content.ReadAsStringAsync();
            Console.WriteLine(eror);
           
            if (responsemessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Destination", new { area = "Admin" });
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDestination(int Id)
        {
            var client = _httpClient.CreateClient();
            var responsemessage=await client.DeleteAsync($"https://localhost:7051/api/Destinations/{Id}");
            if (responsemessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Destination", new { area = "Admin" });
            }
            return RedirectToAction("Index", "Destination", new { area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateDestination(int Id)
        {
            var client = _httpClient.CreateClient();
            var responsemessage=await client.GetAsync($"https://localhost:7051/api/Destinations/{Id}");
            if (responsemessage.IsSuccessStatusCode)
            {
               var json=await responsemessage.Content.ReadAsStringAsync();
               var value= JsonConvert.DeserializeObject<ResultDestination>(json);
               return View(value);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDestination(UpdateDestination updateDestination)
        {
            var json = JsonConvert.SerializeObject(updateDestination);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClient.CreateClient();
            var responsemessage=await client.PutAsync("https://localhost:7051/api/Destinations", content);
            
            if (responsemessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Destination", new { area = "Admin" });
            }
            return View();
        }
    }
}
