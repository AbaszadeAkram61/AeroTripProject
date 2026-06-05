using AeroTripProject.Application.Dtos.Comment;
using AeroTripProject.Application.Dtos.Error;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.WebUI.Controllers
{
    public class CommentController : Controller
    {
       private readonly IHttpClientFactory _httpClientFactory;

        public CommentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

       [HttpPost]
       public async Task<IActionResult> CreateComment(CreateComment comment)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(id))
            {
                TempData["CommentLoginAlert"] = "Şərh yazmaq üçün əvvəlcə daxil olmalısınız.";
                return RedirectToAction("DestinationDetails", "Destination", new { id = comment.DestinationID });
            }
            comment.AppUserId = int.Parse(id);
            var client = _httpClientFactory.CreateClient();
            comment.CommentDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            comment.CommentState=true;
            var json = JsonConvert.SerializeObject(comment);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var responsemessage=await  client.PostAsync("https://akramabaszade-001-site1.site4future.com/api/Comments", content);
            
            if (responsemessage.IsSuccessStatusCode)
            {
                return RedirectToAction("DestinationDetails", "Destination", new { id = comment.DestinationID });
            }
            else
            {
                var erorrjson = await responsemessage.Content.ReadAsStringAsync();
                var errors= JsonConvert.DeserializeObject<List<ValidationErrorDto>>(erorrjson);
                foreach (var item in errors)
                {
                    if (item.PropertyName == "CommentContent")
                    {
                        TempData["CommentContentError"] = item.ErrorMessage;
                    }
                }

                return RedirectToAction("DestinationDetails", "Destination", new { id = comment.DestinationID });
            }
            
        }
    }
}
