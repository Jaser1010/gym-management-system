using GymManagementBLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAnalyticsService analyticsService;

        public HomeController(IAnalyticsService analyticsService)
        {
            this.analyticsService = analyticsService;
        }
        public IActionResult Index()
        {
            var data = analyticsService.GetAnalyticsData();
            return View(data);
        }
    }
}
