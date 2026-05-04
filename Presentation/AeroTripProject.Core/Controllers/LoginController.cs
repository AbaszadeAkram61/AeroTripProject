using AeroTripProject.Application.Dtos.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.WebUI.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(CreateUser createUser)
        {
            var json = JsonConvert.SerializeObject(createUser);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            var responsemesage=await client.PostAsync("https://localhost:7051/api/Users", content);
            var error = await responsemesage.Content.ReadAsStringAsync();
            Console.WriteLine(error);
            if (responsemesage.IsSuccessStatusCode)
            {
                return RedirectToAction("SignIn");
            }
            else
            {

                return RedirectToAction("SignUp");
            }
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
    }
}
