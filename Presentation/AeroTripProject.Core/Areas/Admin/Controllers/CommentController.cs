using AeroTripProject.Application.Dtos.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AeroTripProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CommentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CommentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var ressponsemessage=await client.GetAsync("https://localhost:7051/api/Comments");
            if (ressponsemessage.IsSuccessStatusCode)
            {
               var json=await ressponsemessage.Content.ReadAsStringAsync();
               var values= JsonConvert.DeserializeObject<List<ResultComment>>(json);
               return View(values);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteComment(int Id)
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage=await client.DeleteAsync($"https://localhost:7051/api/Comments/{Id}");
            if (responsemessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Comment", new { area = "Admin" });
            }
            return View();
        }
    }
}
