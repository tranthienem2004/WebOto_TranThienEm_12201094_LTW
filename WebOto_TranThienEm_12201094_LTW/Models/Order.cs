using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebOto_TranThienEm_12201094_LTW.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }

        [ForeignKey("CarId")]
        [ValidateNever]
        public virtual Car Car { get; set; }

        [Required]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }

        [Required, EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Ghi chú")]
        public string? Note { get; set; }

        [Display(Name = "Ngày đặt")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Display(Name = "Trạng thái")]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Display(Name = "Ngày hủy")]
        public DateTime? CancelledDate { get; set; }

        [Display(Name = "Lý do hủy")]
        public string? CancelReason { get; set; }

    }

    public enum OrderStatus
    {
        [Display(Name = "Đang chờ xử lý")]
        Pending = 1,

        [Display(Name = "Đã xác nhận")]
        Confirmed = 2,

        [Display(Name = "Đang xử lý")]
        Processing = 3,

        [Display(Name = "Hoàn thành")]
        Completed = 4,

        [Display(Name = "Đã hủy")]
        Cancelled = 5
    }
}