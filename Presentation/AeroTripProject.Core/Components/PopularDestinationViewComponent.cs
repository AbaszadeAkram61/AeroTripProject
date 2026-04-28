using AeroTripProject.Application.Dtos.Destination;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AeroTripProject.Core.Components
{
    public class PopularDestinationViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PopularDestinationViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responsemessage=await client.GetAsync("https://localhost:7051/api/Destinations");
            if (responsemessage.IsSuccessStatusCode)
            {
               var json=await responsemessage.Content.ReadAsStringAsync();
               var values= JsonConvert.DeserializeObject<List<ResultDestination>>(json);
               return View(values);
            }
            return View();
          
        }
    }
}
