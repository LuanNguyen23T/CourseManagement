using System.ComponentModel.DataAnnotations;

namespace CourseManagement.ViewModels.Users
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập mã học viên.")]
        public string?Username { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [DataType(DataType.Password)]
        public string?Password { get; set; }
    }
}
