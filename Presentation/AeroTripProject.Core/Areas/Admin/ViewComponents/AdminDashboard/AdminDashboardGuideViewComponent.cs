using AeroTripProject.Application.Dtos.Guide;
using AeroTripProject.Application.Dtos.User;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AeroTripProject.WebUI.Areas.Admin.ViewComponents.AdminDashboard
{
    public class AdminDashboardGuideViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminDashboardGuideViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage = await client.GetAsync("https://localhost:7051/api/Guides");
            if (responsemessage.IsSuccessStatusCode)
            {
                var json = await responsemessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultGuide>>(json);
                return View(values);
            }
            return View();
        }
    }
}
