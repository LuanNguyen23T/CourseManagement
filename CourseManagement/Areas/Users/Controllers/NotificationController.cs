using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Areas.Users.Controllers
{
    public class NotificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
