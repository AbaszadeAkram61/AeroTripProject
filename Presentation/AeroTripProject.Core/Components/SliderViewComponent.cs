using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.Core.Components
{
    public class SliderViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
