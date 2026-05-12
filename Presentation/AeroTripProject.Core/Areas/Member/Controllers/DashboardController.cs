using AeroTripProject.WebUI.Areas.Member.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace AeroTripProject.WebUI.Areas.Member.Controllers
{
    [Area("Member")]
    [Route("Member/[controller]/[action]")]
    public class DashboardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var username = User.Identity.Name;

            var client = _httpClientFactory.CreateClient();

            var responsemessage =
                await client.GetAsync(
                    $"https://localhost:7051/api/Users?username={username}");

            var json = await responsemessage.Content.ReadAsStringAsync();

            Console.WriteLine(json);

            if (responsemessage.IsSuccessStatusCode)
            {
                var value =
                    JsonConvert.DeserializeObject<EditUserViewModel>(json);

                ViewBag.Username = value.Username;
                ViewBag.Image = value.ImageUrl;

                return View();
            }

            return View();
        }
    }
}
