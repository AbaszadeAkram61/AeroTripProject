using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.WebUI.Areas.Member.ViewComponents
{
    public class UserHeadViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
