using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagement.Models
{
    public class Account
    {
        [Key]
        public string TaiKhoan { get; set; } 
        public string MatKhau { get; set; } 
    }
}
