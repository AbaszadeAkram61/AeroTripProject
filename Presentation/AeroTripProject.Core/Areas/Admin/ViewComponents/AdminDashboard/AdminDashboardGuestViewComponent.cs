using AeroTripProject.Application.Dtos.User;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AeroTripProject.WebUI.Areas.Admin.ViewComponents.AdminDashboard
{
    public class AdminDashboardGuestViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminDashboardGuestViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage=await client.GetAsync("https://localhost:7051/api/Users/GetUserList");
            if (responsemessage.IsSuccessStatusCode)
            {
               var json=await responsemessage.Content.ReadAsStringAsync();
               var values= JsonConvert.DeserializeObject<List<ResultUsers>>(json);
               return View(values);
            }
            return View();
        }
    }
}
