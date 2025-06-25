using System.ComponentModel.DataAnnotations;

namespace WebOto_TranThienEm_12201094_LTW.Models.ViewModels
{
    public class ContactFormViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [StringLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự")]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        [StringLength(200, ErrorMessage = "Tiêu đề không được vượt quá 200 ký tự")]
        [Display(Name = "Tiêu đề")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập nội dung")]
        [StringLength(1000, ErrorMessage = "Nội dung không được vượt quá 1000 ký tự")]
        [Display(Name = "Nội dung")]
        public string Message { get; set; }

        [Display(Name = "Xe quan tâm")]
        public int? CarId { get; set; }

        public string CarName { get; set; }
    }
}