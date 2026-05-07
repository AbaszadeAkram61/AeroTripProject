using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.WebUI.Areas.Member.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
