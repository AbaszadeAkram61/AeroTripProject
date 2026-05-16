using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.WebUI.Areas.Admin.ViewComponents
{
    public class AdminScriptViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
