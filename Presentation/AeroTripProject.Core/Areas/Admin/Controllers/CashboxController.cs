using AeroTripProject.Application.Dtos.Money;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CashboxController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CashboxController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var totalRevenueResponse = await client.GetAsync("https://localhost:7051/api/Reservations/TotalRevenue");

            if (totalRevenueResponse.IsSuccessStatusCode)
            {
                var json = await totalRevenueResponse.Content.ReadAsStringAsync();
                ViewBag.TotalRevenue = json;
            }

            var currentBalanceResponse = await client.GetAsync("https://localhost:7051/api/Reservations/CurrentBalance");

            if (currentBalanceResponse.IsSuccessStatusCode)
            {
                var json = await currentBalanceResponse.Content.ReadAsStringAsync();
                ViewBag.CurrentBalance = json;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(TransferMoneyDto transferMoneyDto)
        {
            var client = _httpClientFactory.CreateClient();

            // Cari balansı götür
            var balanceResponse = await client.GetAsync(
                "https://localhost:7051/api/Reservations/CurrentBalance");

            var balanceJson = await balanceResponse.Content.ReadAsStringAsync();

            decimal currentBalance = Convert.ToDecimal(balanceJson);

            if (currentBalance <= 0)
            {
                TempData["ErrorMessage"] = "Kassada kifayət qədər vəsait yoxdur.";
                return RedirectToAction("Index");
            }

            if (transferMoneyDto.Amount > currentBalance)
            {
                TempData["ErrorMessage"] =
                    $"Maksimum çıxara biləcəyiniz məbləğ {currentBalance} AZN-dir.";

                return RedirectToAction("Index");
            }

            transferMoneyDto.StatusString = "Təsdiqləndi";

            var json = JsonConvert.SerializeObject(transferMoneyDto);

            StringContent content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

            var responseMessage = await client.PostAsync(
                "https://localhost:7051/api/Reservations/TransferMoney",
                content
            );

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] =
                    "Pul çıxarışı uğurla təsdiqləndi.";
            }

            return RedirectToAction("Index");
        }
    }
}
