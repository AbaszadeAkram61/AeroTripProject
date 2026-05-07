using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.WebUI.Areas.Member.ViewComponents
{
    public class UserNavBarViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
