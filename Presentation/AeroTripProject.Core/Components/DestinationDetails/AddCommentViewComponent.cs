using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.WebUI.Components.Destination
{
    public class AddCommentViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
