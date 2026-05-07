using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.WebUI.Areas.Member.Controllers
{
    [Area("Member")]
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
