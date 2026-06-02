using AeroTripProject.Application.Dtos.ContactUs;
using AeroTripProject.Application.Dtos.Error;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.WebUI.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactUsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SenContactMessage(CreateContactus createContactus)
        {
            createContactus.MessageDate = DateTime.Now.Date;

            var json = JsonConvert.SerializeObject(createContactus);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            var responsemessage = await client.PostAsync("https://localhost:7051/api/ContactUses", content);

            if (responsemessage.IsSuccessStatusCode)
            {
                TempData["ContactSuccess"] = "Mesajınız uğurla göndərildi";
                return RedirectToAction("Index");
            }

            var errorjson = await responsemessage.Content.ReadAsStringAsync();
            var errors = JsonConvert.DeserializeObject<List<ValidationErrorDto>>(errorjson);

            foreach (var item in errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }

            return View("Index", createContactus);
        }
    }
}
