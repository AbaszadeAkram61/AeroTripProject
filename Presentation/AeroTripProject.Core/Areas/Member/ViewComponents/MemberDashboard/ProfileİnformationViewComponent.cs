using AeroTripProject.WebUI.Areas.Member.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace AeroTripProject.WebUI.Areas.Member.ViewComponents.MemberDashboard
{
    public class ProfileİnformationViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProfileİnformationViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var username = User.Identity.Name;
            var client = _httpClientFactory.CreateClient();
            var responsemessage = await client.GetAsync($"https://localhost:7051/api/Users?username={username}");
            
            if (responsemessage.IsSuccessStatusCode)
            {
                var json = await responsemessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<EditUserViewModel>(json);
                ViewBag.NameSurname = value.NameSurname;
                ViewBag.Email = value.Email;
                return View();
            }

            return View();
        }
    }
}
