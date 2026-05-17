using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.WebUI.Controllers
{
    public class ErrorPageController : Controller
    {
        public IActionResult Error404(int code )
        {
            return View();
        }
    }
}
