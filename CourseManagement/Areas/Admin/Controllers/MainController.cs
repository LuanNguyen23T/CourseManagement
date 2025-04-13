using Microsoft.AspNetCore.Mvc;
using CourseManagement.Data; // Namespace for your DbContext
using System.Linq;

namespace CourseManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MainController : Controller
    {
        private readonly CourseManagementDbContext _context;

        public MainController(CourseManagementDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Lấy tổng số học viên
            ViewBag.TotalStudents = _context.HocViens.Count();

            // Lấy số khóa học đang mở
            ViewBag.ActiveCourses = _context.KhoaHocs.Count();

            return View();
        }
    }
}
