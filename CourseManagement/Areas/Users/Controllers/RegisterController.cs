using CourseManagement.Data;
using CourseManagement.Models;
using CourseManagement.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CourseManagement.Areas.Users.Controllers
{
    [Area("Users")]
    public class RegisterController : Controller
    {
        private readonly CourseManagementDbContext _context;

        public RegisterController(CourseManagementDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewData["Title"] = "Đăng Ký";
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Return the view with validation errors
                return View(model);
            }

            // Map the ViewModel to the database entity
            var hocVien = new HocVien
            {
                MaHocVien = model.MaHocVien,
                HoTen = model.HoTen,
                NgaySinh = model.NgaySinh,
                SoDienThoai = model.SoDienThoai,
                Email = model.Email,
                Role = 1 // Default role for a new user
            };

            // Save to the database
            _context.HocViens.Add(hocVien);
            await _context.SaveChangesAsync();

            // Redirect to a success page or login page
            return RedirectToAction("Login", "Login");
        }
    }
}
