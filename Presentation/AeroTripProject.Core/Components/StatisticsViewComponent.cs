using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AeroTripProject.WebUI.Components
{
    public class StatisticsViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StatisticsViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage=await client.GetAsync("https://localhost:7051/api/Destinations/Count");
            if (responsemessage.IsSuccessStatusCode)
            {
                var json = await responsemessage.Content.ReadAsStringAsync();
                ViewBag.v1 = json;
                
            }

            var client1 = _httpClientFactory.CreateClient();
            var resonsemessage1= await client1.GetAsync("https://localhost:7051/api/Guides/Count");

            if (resonsemessage1.IsSuccessStatusCode)
            {
                var json1 = await resonsemessage1.Content.ReadAsStringAsync();
                ViewBag.v2 = json1;

            }

            return View();
        }
    }
}
