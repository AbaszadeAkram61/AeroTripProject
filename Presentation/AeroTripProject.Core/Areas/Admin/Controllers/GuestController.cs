using AeroTripProject.Application.Dtos.Comment;
using AeroTripProject.Application.Dtos.Error;
using AeroTripProject.Application.Dtos.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GuestController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GuestController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage=await client.GetAsync("https://akramabaszade-001-site1.site4future.com/api/Users/GetUserList");
            if (responsemessage.IsSuccessStatusCode)
            {
               var json=await responsemessage.Content.ReadAsStringAsync();
               var values= JsonConvert.DeserializeObject<List<ResultUsers>>(json);
               return View(values);
            }
            return View();
        }
        [HttpGet]
        public IActionResult CreateGuest()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateGuest(UserSignUp userSignUp)
        {
            var json = JsonConvert.SerializeObject(userSignUp);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            var responsemessage = await client.PostAsync("https://akramabaszade-001-site1.site4future.com/api/Users", content);
            if (responsemessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Guest", new { area = "Admin" });
            }
            else
            {
                var erorjson = await responsemessage.Content.ReadAsStringAsync();
                var error= JsonConvert.DeserializeObject<List<ValidationErrorDto>>
                (erorjson);
                foreach (var item in error)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteGuest(int Id)
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage=await client.DeleteAsync($"https://akramabaszade-001-site1.site4future.com/api/Users/{Id}");
            if (responsemessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Guest", new { area = "Admin" });
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditGuest(int Id)
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage=await client.GetAsync($"https://akramabaszade-001-site1.site4future.com/api/Users/{Id}");
            if (responsemessage.IsSuccessStatusCode)
            {
               var json=await responsemessage.Content.ReadAsStringAsync();
               var value= JsonConvert.DeserializeObject<ResultUsers>(json);
                return View(value);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditGuest(UpdateUserDto updateUserDto)
        {
            var json= JsonConvert.SerializeObject(updateUserDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            var responsemessage=await client.PutAsync("https://akramabaszade-001-site1.site4future.com/api/Users", content);
            var eror=await responsemessage.Content.ReadAsStringAsync();
            if (responsemessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Guest", new { area = "Admin" });
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CommentGuest(int Id)
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage = await client.GetAsync($"https://akramabaszade-001-site1.site4future.com/api/Comments/GetByIdCommentUser/{Id}");
            if (responsemessage.IsSuccessStatusCode)
            {
               var json=await responsemessage.Content.ReadAsStringAsync();
               var value= JsonConvert.DeserializeObject<List<ResultComment>>(json);
               return View(value);
            }
            return View();
        }
    }

}
