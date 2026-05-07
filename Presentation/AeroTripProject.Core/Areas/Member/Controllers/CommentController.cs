using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.WebUI.Areas.Member.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
