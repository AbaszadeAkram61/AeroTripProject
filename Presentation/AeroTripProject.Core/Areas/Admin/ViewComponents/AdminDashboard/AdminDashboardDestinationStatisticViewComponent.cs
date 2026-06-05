using AeroTripProject.Application.Dtos.Destination;
using AeroTripProject.Application.Dtos.User;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AeroTripProject.WebUI.Areas.Admin.ViewComponents.AdminDashboard
{
    public class AdminDashboardDestinationStatisticViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminDashboardDestinationStatisticViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage = await client.GetAsync("https://akramabaszade-001-site1.site4future.com/api/Destinations");
            if (responsemessage.IsSuccessStatusCode)
            {
                var json = await responsemessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultDestination>>(json);
                return View(values);
            }
            return View();
        }
    }
}
