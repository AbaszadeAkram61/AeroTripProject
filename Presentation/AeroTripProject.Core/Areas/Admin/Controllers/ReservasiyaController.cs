using AeroTripProject.Application.Dtos.Reservation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AeroTripProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ReservasiyaController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReservasiyaController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync("https://akramabaszade-001-site1.site4future.com/api/Reservations");

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<ResultReservation>>(json);

                return View(values);
            }

            return View(new List<ResultReservation>());
        }

        public async Task<IActionResult> ChangeStatus(int id, string status)
        {
            var client = _httpClientFactory.CreateClient();

            await client.GetAsync(
                $"https://akramabaszade-001-site1.site4future.com/api/Reservations/ChangeStatus/{id}/{status}");

            return RedirectToAction("Index");
        }
    }
}
