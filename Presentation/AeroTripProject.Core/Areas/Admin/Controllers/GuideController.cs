using AeroTripProject.Application.Dtos.Error;
using AeroTripProject.Application.Dtos.Guide;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GuideController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GuideController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage=await client.GetAsync("https://akramabaszade-001-site1.site4future.com/api/Guides");
            if (responsemessage.IsSuccessStatusCode)
            {
              var json=await responsemessage.Content.ReadAsStringAsync();
              List<ResultGuide> guides= JsonConvert.DeserializeObject<List<ResultGuide>>(json);
                return View(guides);
            }
            return View();
        }
        [HttpGet]
        public IActionResult CreateGuide()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGuide(CreateGuide createGuide)
        {
            var json = JsonConvert.SerializeObject(createGuide);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            var responsemessage = await client.PostAsync("https://akramabaszade-001-site1.site4future.com/api/Guides", content);
            if (responsemessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Guide", new { area = "Admin" });
            }

           var errorjson=await responsemessage.Content.ReadAsStringAsync();
           var errors= JsonConvert.DeserializeObject<List<ValidationErrorDto>>(errorjson);
            foreach (var item in errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditGuide(int Id)
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage=await client.GetAsync($"https://akramabaszade-001-site1.site4future.com/api/Guides/{Id}");
            if (responsemessage.IsSuccessStatusCode)
            {
               var json=await responsemessage.Content.ReadAsStringAsync();
               var value= JsonConvert.DeserializeObject<ResultGuide>(json);
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditGuide(UpdateGuide updateGuide)
        {

            var json = JsonConvert.SerializeObject(updateGuide);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            var responsemessage = await client.PutAsync("https://akramabaszade-001-site1.site4future.com/api/Guides", content);
            if (responsemessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Guide", new { area = "Admin" });
            }
            else 
            {
               var errorjson=await responsemessage.Content.ReadAsStringAsync();
               var errors= JsonConvert.DeserializeObject<List<ValidationErrorDto>>(errorjson);
                foreach (var item in errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        public async Task<IActionResult> ChangeStatus(int id,bool status)
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage = await client.GetAsync(
      $"https://akramabaszade-001-site1.site4future.com/api/Guides/ChangeStatus/{id}/{status}");
            
            if (responsemessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Guide", new { area = "Admin" });
            }
            return RedirectToAction("Index", "Guide", new { area = "Admin" });
        }

       




    }
}
