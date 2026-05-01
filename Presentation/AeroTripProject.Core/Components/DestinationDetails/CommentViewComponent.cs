using AeroTripProject.Application.Dtos.Comment;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AeroTripProject.WebUI.Components.Destination
{
    public class CommentViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClient;

        public CommentViewComponent(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync(int Id)
        {
            var client = _httpClient.CreateClient();
            var responsemessage = await client.GetAsync($"https://localhost:7051/api/Comments/GetListCommentById/{Id}");
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
