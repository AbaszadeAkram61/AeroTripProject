using AeroTripProject.Application.Dtos.Destination;
using AeroTripProject.Application.Dtos.SubAbout;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AeroTripProject.WebUI.Components
{
    public class SubAboutViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SubAboutViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage = await client.GetAsync("https://akramabaszade-001-site1.site4future.com/api/SubAbouts");
            if (responsemessage.IsSuccessStatusCode)
            {
                var json = await responsemessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultSubAbout>>(json);
                return View(values);
            }
            return View();

        }
    }
}
