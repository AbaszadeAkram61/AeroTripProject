using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.WebUI.Areas.Member.ViewComponents
{
    public class UserFooterViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
