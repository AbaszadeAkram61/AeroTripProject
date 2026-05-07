using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.WebUI.Areas.Member.ViewComponents
{
    public class UserScriptViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
