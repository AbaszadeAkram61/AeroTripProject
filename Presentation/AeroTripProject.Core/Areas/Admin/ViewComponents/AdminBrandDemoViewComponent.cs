using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.WebUI.Areas.Admin.ViewComponents
{
    public class AdminBrandDemoViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
