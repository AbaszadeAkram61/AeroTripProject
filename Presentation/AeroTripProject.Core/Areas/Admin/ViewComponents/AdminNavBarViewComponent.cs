using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.WebUI.Areas.Admin.ViewComponents
{
    public class AdminNavBarViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
