using AeroTripProject.Application.Dtos.Money;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            var responsemessage=await client.GetAsync("https://localhost:7051/api/Reservations/TotalRevenue");
            if (responsemessage.IsSuccessStatusCode)
            {
                var json=await responsemessage.Content.ReadAsStringAsync();
                ViewBag.TotalRevenue = json;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(TransferMoneyDto transferMoneyDto)
        {
            transferMoneyDto.StatusString = "Təsdiqləndi";
            var client = _httpClientFactory.CreateClient();

            var json = JsonConvert.SerializeObject(transferMoneyDto);

            StringContent content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

           var responsemessage= await client.PostAsync(
                "https://localhost:7051/api/Reservations/TransferMoney",
                content
            );
            if (responsemessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Pul çıxarışı uğurla təsdiqləndi.";

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }


        }

    }
}
