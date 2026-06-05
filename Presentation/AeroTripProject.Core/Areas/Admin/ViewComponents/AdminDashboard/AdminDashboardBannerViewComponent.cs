using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AeroTripProject.WebUI.Areas.Admin.ViewComponents.AdminDashboard
{
    public class AdminDashboardBannerViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminDashboardBannerViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();

            
            var totalResponse = await client.GetAsync("https://akramabaszade-001-site1.site4future.com/api/Reservations/Count");

           
            var activeResponse = await client.GetAsync("https://akramabaszade-001-site1.site4future.com/api/Reservations/GetListCurrentReservation");

            if (totalResponse.IsSuccessStatusCode &&
                activeResponse.IsSuccessStatusCode)
            {
                var totalJson = await totalResponse.Content.ReadAsStringAsync();
                var activeJson = await activeResponse.Content.ReadAsStringAsync();

                int total = int.Parse(totalJson);
                int active = int.Parse(activeJson);

                ViewBag.totalreservation = total;
                ViewBag.activeReservation = active;

                ViewBag.percent = total == 0
                    ? 0
                    : (active * 100) / total;
            }

            return View();
        }
    }
}
