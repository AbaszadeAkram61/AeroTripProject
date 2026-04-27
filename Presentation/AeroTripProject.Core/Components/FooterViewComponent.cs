using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.Core.Components
{
    public class FooterViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
