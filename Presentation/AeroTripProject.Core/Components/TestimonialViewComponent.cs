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
            var responsemessage = await client.GetAsync("https://localhost:7051/api/Testimonials");
            if (responsemessage.IsSuccessStatusCode)
            {
                var json = await responsemessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultTestimonial>>(json);

                return View(values);
            }
            return View();
        }
    }
}
