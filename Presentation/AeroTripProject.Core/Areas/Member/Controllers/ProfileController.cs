using AeroTripProject.Application.Dtos.User;
using AeroTripProject.WebUI.Areas.Member.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.WebUI.Areas.Member.Controllers
{
    [Area("Member")]
    [Route("Member/[controller]/[action]")]
    public class ProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProfileController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
           
            var username = User.Identity.Name;
            var client = _httpClientFactory.CreateClient();
            var responsemessage=await client.GetAsync($"https://localhost:7051/api/Users?username={username}");
            var error= await responsemessage.Content.ReadAsStringAsync();
            Console.WriteLine(error);
            if (responsemessage.IsSuccessStatusCode)
            {
               var json=await responsemessage.Content.ReadAsStringAsync();
               var value= JsonConvert.DeserializeObject<EditUserViewModel>(json);
                ViewBag.username = username;
               return View(value);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(EditUserViewModel editUserViewModel)
        {
            if (editUserViewModel.Image != null)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(editUserViewModel.Image.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = resource + "/wwwroot/userimages/" + imagename;

                using var stream = new FileStream(savelocation, FileMode.Create);

                await editUserViewModel.Image.CopyToAsync(stream);

                editUserViewModel.ImageUrl = imagename;
            }

            var json = JsonConvert.SerializeObject(editUserViewModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();

            var responsemessage = await client.PutAsync("https://localhost:7051/api/Users", content);

            var error = await responsemessage.Content.ReadAsStringAsync();

            if (responsemessage.IsSuccessStatusCode)
            {
                return RedirectToAction("SignIn", "Login", new { area = "" });
            }
            else
            {
                return RedirectToAction("Index", "Profile", new { area = "Member" });
            }
        }

    }
}
