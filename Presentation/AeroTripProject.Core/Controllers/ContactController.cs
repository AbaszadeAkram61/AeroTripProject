using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.WebUI.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
