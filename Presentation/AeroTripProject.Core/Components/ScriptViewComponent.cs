using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.Core.Components
{
    public class ScriptViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
