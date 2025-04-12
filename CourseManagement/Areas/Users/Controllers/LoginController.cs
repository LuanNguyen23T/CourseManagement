using CourseManagement.Data;
using CourseManagement.ViewModels.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _context.HocViens
                .FirstOrDefault(u => u.MaHocVien == model.MaHocVien && u.MatKhau == model.MatKhau);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng");
                return View(model);
            }


            // Tạo Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.MaHocVien),
                new Claim(ClaimTypes.Role, user.Role.ToString()) // Ensure Role is converted to string
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Tạo Cookie
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe, // Ghi nhớ đăng nhập
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1) // Thời gian hết hạn
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }


        [HttpGet]
        public IActionResult ForgotPassword()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult ForgotPassword(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Tìm kiếm người dùng dựa trên tên đăng nhập và số điện thoại
            var user = _context.HocViens.FirstOrDefault(u => u.MaHocVien == model.MaHocVien && u.Email == model.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Thông tin không chính xác. Vui lòng kiểm tra lại.");
                return View(model);
            }

            // Logic gửi email hoặc thông báo đặt lại mật khẩu
            TempData["Message"] = "Yêu cầu đặt lại mật khẩu đã được gửi. Vui lòng kiểm tra email của bạn.";
            return RedirectToAction("ForgotPassword");
        }

        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            // Kiểm tra token và userId hợp lệ
            var model = new LoginViewModel
            {
                MaHocVien = userId,
                MatKhau = token
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ResetPassword(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Tìm người dùng dựa trên UserId
            var user = _context.HocViens.FirstOrDefault(u => u.MaHocVien == model.MaHocVien);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Người dùng không tồn tại.");
                return View(model);
            }

            // Cập nhật mật khẩu mới
            user.MatKhau = model.MatKhau;
            _context.SaveChanges();

            TempData["Message"] = "Mật khẩu đã được đặt lại thành công.";
            return RedirectToAction("Login");
        }


    }
}
