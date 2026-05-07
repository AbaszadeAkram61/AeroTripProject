using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.WebUI.Areas.Member.ViewComponents
{
    public class UserSideBarViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
