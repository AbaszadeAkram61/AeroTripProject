using AeroTripProject.Application.Dtos.Guide;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AeroTripProject.WebUI.Areas.Member.ViewComponents.MemberDashboard
{
    public class GuideListViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GuideListViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var ressponsemessage=await client.GetAsync("https://localhost:7051/api/Guides");
            if (ressponsemessage.IsSuccessStatusCode)
            {
               var json=await ressponsemessage.Content.ReadAsStringAsync();
               var values= JsonConvert.DeserializeObject<List<ResultGuide>>(json);
                return View(values);
            }
            return View();
        }
    }
}
