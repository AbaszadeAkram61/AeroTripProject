using AeroTripProject.Application.Dtos.Destination;
using AeroTripProject.Application.Dtos.Reservation;
using AeroTripProject.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.WebUI.Areas.Member.Controllers
{
    [Area("Member")]
    [Route("Member/[controller]/[action]")]
    public class ReservationController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
       

        public ReservationController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult MyCurrentReservation()
        {
            return View();
        }

        public IActionResult MyOldReservation()
        {
            return View();
        }

        public async Task<IActionResult> MyApprovalReservation()
        {
            var client = _httpClient.CreateClient();
            var appUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var responsemessage = await client.GetAsync($"https://localhost:7051/api/Reservations/GetListApprovalReservation/{appUserId}");
            if (responsemessage.IsSuccessStatusCode)
            {
                var json = await responsemessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultReservation>>(json);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> NewReservation()
        {
            var client = _httpClient.CreateClient();
            var responsemessage = await client.GetAsync("https://localhost:7051/api/Destinations/GetListName");
            if (responsemessage.IsSuccessStatusCode)
            {
                var json = await responsemessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<string>>(json);
                ViewBag.v1 = values.Select(x => new SelectListItem
                {
                    Text = x,
                    Value = x
                }).ToList();
                
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewReservation(CreateReservation createReservation)
        {
            createReservation.AppUserId = 9;
            createReservation.Status = "Təsdiq Gözləyir";
            var json = JsonConvert.SerializeObject(createReservation);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClient.CreateClient();
            var ressponsemessage=await client.PostAsync("https://localhost:7051/api/Reservations", content);
            var error=await ressponsemessage.Content.ReadAsStringAsync();
            if (ressponsemessage.IsSuccessStatusCode)
            {
                return RedirectToAction("MyCurrentReservation", "Reservation", new { area ="Member"});
            }
            else
            {
                return RedirectToAction("NewReservation", "Reservation", new { area = "Member" });
            }
        }
    }
}
