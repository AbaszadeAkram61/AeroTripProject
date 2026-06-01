using AeroTripProject.Application.Dtos.Guide;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AeroTripProject.WebUI.Controllers
{
    public class GuideController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GuideController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage=await client.GetAsync("https://localhost:7051/api/Guides");
            if (responsemessage.IsSuccessStatusCode)
            {
               var json=await responsemessage.Content.ReadAsStringAsync();
               var values= JsonConvert.DeserializeObject<List<ResultGuide>>(json);
                values = values.Where(x => x.Status == true).ToList();

                return View(values);
            }
            return View();
        }
    }
}
