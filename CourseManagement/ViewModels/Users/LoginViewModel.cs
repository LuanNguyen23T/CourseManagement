using System.ComponentModel.DataAnnotations;

namespace CourseManagement.ViewModels.Users
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập Mã học viên")]
        public string? MaHocVien { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu")]
        public string? MatKhau { get; set; }

        public bool RememberMe { get; set; }
    }
}
