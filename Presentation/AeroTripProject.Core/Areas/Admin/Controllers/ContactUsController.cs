using AeroTripProject.Application.Dtos.ContactUs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AeroTripProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactUsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactUsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage=await client.GetAsync("https://localhost:7051/api/ContactUses");
            if (responsemessage.IsSuccessStatusCode)
            {
              var json=await responsemessage.Content.ReadAsStringAsync();
              var values= JsonConvert.DeserializeObject<List<ResultContactus>>(json);
               return View(values);

            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ChangeStatus(int Id,bool status)
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage=await client.GetAsync($"https://localhost:7051/api/ContactUses/ChangeStatus/{Id}/{status}");
            if (responsemessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> MessageDetails(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage=await client.GetAsync($"https://localhost:7051/api/ContactUses/{id}");
            if (responsemessage.IsSuccessStatusCode)
            {
               var json=await responsemessage.Content.ReadAsStringAsync();
               var value= JsonConvert.DeserializeObject<ResultContactus>(json);
                ViewBag.messajedetal = value.MessageBody;
            }
            return View();
        }
    }
}
