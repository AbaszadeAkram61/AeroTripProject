using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.Core.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
