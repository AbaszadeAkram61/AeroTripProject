using AeroTripProject.Application.Dtos.Password;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AeroTripProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
   
    public class PasswordChangesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PasswordChangesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            ViewBag.UserId = userId;
            ViewBag.Token = token;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            var client = _httpClientFactory.CreateClient();

            var json = JsonConvert.SerializeObject(model);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(
                "https://akramabaszade-001-site1.site4future.com/api/PasswordChange/ResetPassword",
                content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] =
                    "Parol uğurla yeniləndi.";

                return RedirectToAction("SignIn", "Login", new {area=" "});
            }

            TempData["ErrorMessage"] =
                await response.Content.ReadAsStringAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ForgetPassword forgetPassword)
        {
            var client = _httpClientFactory.CreateClient();

            var json = JsonConvert.SerializeObject(forgetPassword);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

            var responseMessage = await client.PostAsync(
      "https://akramabaszade-001-site1.site4future.com/api/PasswordChange/ForgetPassword",
      content
  );

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] =
                    "Parol yeniləmə linki e-poçt ünvanınıza göndərildi.";

                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] =
    await responseMessage.Content.ReadAsStringAsync();

            return View();
        }
    }


}

