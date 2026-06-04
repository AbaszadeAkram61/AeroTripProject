using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.WebUI.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
