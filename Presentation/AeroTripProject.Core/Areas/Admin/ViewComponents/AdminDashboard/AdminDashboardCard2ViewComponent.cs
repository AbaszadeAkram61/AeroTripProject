using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.WebUI.Areas.Admin.ViewComponents.AdminDashboard
{
    public class AdminDashboardCard2ViewComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminDashboardCard2ViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var TotalrevenueResponsemessage = await client.GetAsync("https://akramabaszade-001-site1.site4future.com/api/Reservations/TotalRevenue");
            var ApprovalrevenueResponsemessage = await client.GetAsync("https://akramabaszade-001-site1.site4future.com/api/Reservations/ApprovalRevenue");
            if (TotalrevenueResponsemessage.IsSuccessStatusCode && ApprovalrevenueResponsemessage.IsSuccessStatusCode)
            {
                var jsontotal = await TotalrevenueResponsemessage.Content.ReadAsStringAsync();
                var jsonapproval = await ApprovalrevenueResponsemessage.Content.ReadAsStringAsync();
                ViewBag.Total = jsontotal;
                ViewBag.Approval = jsonapproval;
                var random = new Random();
                decimal target = 10000;
                decimal total = decimal.Parse(jsontotal);
                decimal percent = (total * 100) / target;
                ViewBag.percent = percent;
                 
                random.Next(1, 100);
                ViewBag.r1 = random.Next(1, 100);
                ViewBag.r2 = random.Next(1, 100);
            }
            return View();
        }
    }
}
