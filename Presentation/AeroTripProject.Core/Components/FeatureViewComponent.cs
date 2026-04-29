using AeroTripProject.Application.Dtos.Feature;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AeroTripProject.WebUI.Components
{
    public class FeatureViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClient;

        public FeatureViewComponent(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClient.CreateClient();
            var responsemessage=await client.GetAsync("https://localhost:7051/api/Features");
            if (responsemessage.IsSuccessStatusCode)
            {
                var json=await responsemessage.Content.ReadAsStringAsync();
                var values= JsonConvert.DeserializeObject<List<ResultFeature>>(json);

                return View(values);
            }
            return View();
        }
    }
}
