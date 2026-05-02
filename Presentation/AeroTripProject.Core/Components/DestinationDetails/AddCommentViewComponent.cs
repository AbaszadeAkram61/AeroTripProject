using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.WebUI.Components.Destination
{
    public class AddCommentViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(int Id)
        {
            ViewBag.i = Id;
            return View();
        }
    }
}
