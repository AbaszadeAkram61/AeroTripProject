using AeroTripProject.Application.Dtos.Error;
using AeroTripProject.Application.Dtos.User;
using AeroTripProject.WebUI.Areas.Member.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var client = _httpClientFactory.CreateClient();
            var responsemessage = await client.GetAsync($"https://localhost:7051/api/Users?username={username}");
            var error = await responsemessage.Content.ReadAsStringAsync();
            Console.WriteLine(error);
            if (responsemessage.IsSuccessStatusCode)
            {
                var json = await responsemessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<EditUserViewModel>(json);
                ViewBag.username = username;
                ViewBag.userId = userId;
                return View(value);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(EditUserViewModel editUserViewModel)
        {
            editUserViewModel.OldUsername = User.Identity.Name;

            var usernameChanged = editUserViewModel.Username != User.Identity.Name;

            if (editUserViewModel.Image != null)
            {
                var resource = Directory.GetCurrentDirectory();

                if (!string.IsNullOrEmpty(editUserViewModel.ImageUrl))
                {
                    var oldImagePath = Path.Combine(resource, "wwwroot", "userimages", editUserViewModel.ImageUrl);

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                var extension = Path.GetExtension(editUserViewModel.Image.FileName);
                var imageName = Guid.NewGuid() + extension;

                var saveLocation = Path.Combine(resource, "wwwroot", "userimages", imageName);

                using var stream = new FileStream(saveLocation, FileMode.Create);

                await editUserViewModel.Image.CopyToAsync(stream);

                editUserViewModel.ImageUrl = imageName;
            }

            var json = JsonConvert.SerializeObject(editUserViewModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.PutAsync("https://localhost:7051/api/Users", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Məlumatlar uğurla yeniləndi.";

                if (usernameChanged)
                {
                    await HttpContext.SignOutAsync();
                    return RedirectToAction("SignIn", "Login", new { area = "" });
                }

                return RedirectToAction("Index", "Profile", new { area = "Member" });
            }

            var errorJson = await responseMessage.Content.ReadAsStringAsync();

            try
            {
                var errors = JsonConvert.DeserializeObject<List<ValidationErrorDto>>(errorJson);

                if (errors != null)
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }

                    return View("Index", editUserViewModel);
                }
            }
            catch
            {
                TempData["ErrorMessage"] = errorJson;
            }

            return View("Index", editUserViewModel);
        }
        [HttpGet]
            public async Task<IActionResult> DeleteUser(int id)
            {
                var client = _httpClientFactory.CreateClient();

                var responsemessage = await client.DeleteAsync(
                    $"https://localhost:7051/api/Users/{id}");

                if (responsemessage.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Hesabınız uğurla silindi.";
                    return RedirectToAction("Index", "Home", new { area = "" });
                }

                TempData["ErrorMessage"] = "Hesab silinmədi.";
                return RedirectToAction("Index");
            }
        }
    }

    
    
