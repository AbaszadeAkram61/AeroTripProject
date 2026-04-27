using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.Core.Components
{
    public class HeaderViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
