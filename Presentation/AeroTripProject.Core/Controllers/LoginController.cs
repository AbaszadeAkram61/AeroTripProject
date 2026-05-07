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
        public async Task<IActionResult> SignUp(UserSignUp createUser)
        {
            var json = JsonConvert.SerializeObject(createUser);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();

            var responsemesage = await client.PostAsync("https://localhost:7051/api/Users", content);

            if (responsemesage.IsSuccessStatusCode)
            {
                return RedirectToAction("SignIn");
            }

            var errorJson = await responsemesage.Content.ReadAsStringAsync();

            var errors = JsonConvert.DeserializeObject<List<string>>(errorJson);

            foreach (var error in errors)
            {
                if (error.Contains("istifadəçi adı"))
                {
                    ModelState.AddModelError("Username", error);
                }
                else
                {
                    ModelState.AddModelError("Password", error);
                }
            }
            return View(createUser);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignIn userSignIn)
        {
            var json = JsonConvert.SerializeObject(userSignIn);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();

            var responsemesage = await client.PostAsync("https://localhost:7051/api/Users/UserSignIn", content);

            if (responsemesage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                return RedirectToAction("SignIn");
            }


        }
    }
}
