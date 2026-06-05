using AeroTripProject.Application.Dtos.AppRole;
using AeroTripProject.Application.Dtos.Error;
using AeroTripProject.Application.Dtos.User;
using AeroTripProject.Domain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public RoleController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage=await client.GetAsync("https://akramabaszade-001-site1.site4future.com/api/Roles");
            if (responsemessage.IsSuccessStatusCode)
            {
               var json=await responsemessage.Content.ReadAsStringAsync();
               var values= JsonConvert.DeserializeObject<List<ResultAppRole>>(json);
               return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateAppRole createAppRole)
        {
            var json = JsonConvert.SerializeObject(createAppRole);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            var responsemessage=await client.PostAsync("https://akramabaszade-001-site1.site4future.com/api/Roles", content);
            if (responsemessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Rol uğurla əlavə olundu";
                return RedirectToAction("Index");
            }
            else
            {
               var erorjson=await responsemessage.Content.ReadAsStringAsync();
               var error= JsonConvert.DeserializeObject<List<ValidationErrorDto>>(erorjson);
                foreach (var item in error)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }

            }

            return View(createAppRole);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRole(int Id)
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage=await client.DeleteAsync($"https://akramabaszade-001-site1.site4future.com/api/Roles/{Id}");
            if (responsemessage.IsSuccessStatusCode)
            {
               return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRole(int Id)
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage=await client.GetAsync($"https://akramabaszade-001-site1.site4future.com/api/Roles/{Id}");
            if (responsemessage.IsSuccessStatusCode)
            {
               var json=await responsemessage.Content.ReadAsStringAsync();
               var value= JsonConvert.DeserializeObject<ResultAppRole>(json);
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateAppRole updateAppRole)
        {
            var json = JsonConvert.SerializeObject(updateAppRole);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.PutAsync(
                "https://akramabaszade-001-site1.site4future.com/api/Roles",
                content);

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Rol uğurla yeniləndi";
                return RedirectToAction("Index");
            }

            var errorJson = await responseMessage.Content.ReadAsStringAsync();

            var errors = JsonConvert.DeserializeObject<List<ValidationErrorDto>>(errorJson);

            if (errors != null)
            {
                foreach (var item in errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View(updateAppRole);
        }


        [HttpGet]
        public async Task<IActionResult> GetUserList()
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage=await client.GetAsync("https://akramabaszade-001-site1.site4future.com/api/Users/GetUserList");
            if (responsemessage.IsSuccessStatusCode)
            {
               var json=await responsemessage.Content.ReadAsStringAsync();
               var value= JsonConvert.DeserializeObject<List<ResultUsers>>(json);
                return View(value);

            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AssignRole(int Id)
        {
            ViewBag.UserId = Id;

            var client = _httpClientFactory.CreateClient();
            var responsemessage = await client.GetAsync($"https://akramabaszade-001-site1.site4future.com/api/Roles/AssignRole/{Id}");

            if (responsemessage.IsSuccessStatusCode)
            {
                var json = await responsemessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<List<RoleAssign>>(json);
                return View(value);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(int id, List<RoleAssign> model)
        {
            var client = _httpClientFactory.CreateClient();

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync(
                $"https://akramabaszade-001-site1.site4future.com/api/Roles/AssignRole/{id}",
                content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetUserList");
            }

            return View(model);
        }

    }
}
