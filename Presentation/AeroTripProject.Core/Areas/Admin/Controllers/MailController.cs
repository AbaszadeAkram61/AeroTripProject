using AeroTripProject.Application.Dtos.Error;
using AeroTripProject.Application.Dtos.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace AeroTripProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MailController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MailController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMail(MailRequest mailRequest)
        {
            var json = JsonConvert.SerializeObject(mailRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.PostAsync(
                "https://localhost:7051/api/Mails",
                content
            );

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["MailSuccess"] = "Mail uğurla göndərildi";
                return RedirectToAction("Index");
            }
            else
            {
               var erorjson=await responseMessage.Content.ReadAsStringAsync();
               var error= JsonConvert.DeserializeObject<List<ValidationErrorDto>>(erorjson);
                foreach (var item in error)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View("Index", mailRequest);
        }
    }
}
