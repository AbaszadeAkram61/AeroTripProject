using AeroTripProject.Application.Dtos.Comment;
using AeroTripProject.Application.Dtos.Testimonail;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AeroTripProject.WebUI.Components
{
    public class TestimonialViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClient;

        public TestimonialViewComponent(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClient.CreateClient();
            var responsemessage = await client.GetAsync("https://akramabaszade-001-site1.site4future.com/api/Comments");
            if (responsemessage.IsSuccessStatusCode)
            {
                var json = await responsemessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultComment>>(json);

                return View(values);
            }
            return View();
        }
    }
}
