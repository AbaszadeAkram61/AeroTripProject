using AeroTripProject.Application.Dtos.Guide;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AeroTripProject.WebUI.Components.DestinationDetails
{
    public class AuthorGuideViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthorGuideViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage=await client.GetAsync("https://localhost:7051/api/Guides");
            if (responsemessage.IsSuccessStatusCode)
            {
               var json=await responsemessage.Content.ReadAsStringAsync();
               var values= JsonConvert.DeserializeObject<List<ResultGuide>>(json);
                if (values != null && values.Any())
                {
                    var random = new Random();
                    var randomGuide = values[random.Next(values.Count)];

                    return View(randomGuide);
                }
            }
            return View();
        }
    }
}
