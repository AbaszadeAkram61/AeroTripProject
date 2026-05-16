using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.WebUI.Areas.Admin.ViewComponents
{
    public class AdminSideBarViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
