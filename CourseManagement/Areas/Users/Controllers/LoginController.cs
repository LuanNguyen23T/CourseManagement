using CourseManagement.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CourseManagement.Data;
using CourseManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Areas.Users.Controllers
{
    [Area("Users")]
    public class LoginController : Controller
    {
        private readonly CourseManagementDbContext _context; 

        public LoginController(CourseManagementDbContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewData["Title"] = "Đăng Nhập";
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Trả về view với lỗi xác thực
                return View(model);
            }

            // Kiểm tra thông tin đăng nhập
            var user = await _context.Accounts
                .FirstOrDefaultAsync(u => u.TaiKhoan == model.Username);

            if (user == null || user.MatKhau != model.Password)
            {
                // Thêm lỗi xác thực nếu thông tin không đúng
                ModelState.AddModelError(string.Empty, "Mã học viên hoặc mật khẩu không đúng.");
                return View(model);
            }

            // Đăng nhập thành công, chuyển hướng
            return RedirectToAction("Index", "Home");
        }


    }
}
