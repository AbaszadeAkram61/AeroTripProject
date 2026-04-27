using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.Core.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
