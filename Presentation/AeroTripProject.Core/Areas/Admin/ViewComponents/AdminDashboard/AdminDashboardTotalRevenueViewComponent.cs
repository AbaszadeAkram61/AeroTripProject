using Microsoft.AspNetCore.Mvc;

namespace AeroTripProject.WebUI.Areas.Admin.ViewComponents.AdminDashboard
{
    public class AdminDashboardTotalRevenueViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
