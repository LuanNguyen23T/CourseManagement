using Microsoft.AspNetCore.Mvc;
using CourseManagement.Data; 
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

        public IActionResult Main()
        {
            // Lấy tổng số học viên
            ViewBag.TotalStudents = _context.HocViens.Count();

            // Lấy số khóa học đang mở
            ViewBag.ActiveCourses = _context.KhoaHocs.Count();

            return View();
        }

        public IActionResult ManageStudents()
        {
            // Lấy danh sách học viên
            var students = _context.HocVien.ToList();

            // Truyền danh sách học viên vào ViewBag
            ViewBag.Students = students;

            // Trả về view
            return View();
        }

    }
}
