using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AeroTripProject.WebUI.Areas.Admin.ViewComponents.AdminDashboard
{
    public class AdminDashboardCard1ViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminDashboardCard1ViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var TurResponsemessage=await client.GetAsync("https://akramabaszade-001-site1.site4future.com/api/Destinations/Count");
            var QonaqResponsemessage =await client.GetAsync("https://akramabaszade-001-site1.site4future.com/api/Users/GetUserListCount");
            if (TurResponsemessage.IsSuccessStatusCode && QonaqResponsemessage.IsSuccessStatusCode)
            {
                var jsontur=await TurResponsemessage.Content.ReadAsStringAsync();
                var jsonqonaq =await QonaqResponsemessage.Content.ReadAsStringAsync();
                ViewBag.TurCount = jsontur;
                ViewBag.QonaqCount = jsonqonaq;
                var random = new Random();
                random.Next(1, 100);
                ViewBag.r1 = random.Next(1, 100);
                ViewBag.r2 = random.Next(1, 100);
            }
            return View();
        }
    }
}
