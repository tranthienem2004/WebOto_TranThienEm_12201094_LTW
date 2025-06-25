using System.ComponentModel.DataAnnotations;

namespace WebOto_TranThienEm_12201094_LTW.Models.ViewModels
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Họ và Tên Khách hàng")]
        public string CustomerName { get; set; }

        [Display(Name = "Số điện thoại")]
        public string CustomerPhone { get; set; }

        [Display(Name = "Email")]
        public string CustomerEmail { get; set; }

        [Display(Name = "Địa chỉ")]
        public string CustomerAddress { get; set; }

        [Display(Name = "Ghi chú")]
        public string CustomerNotes { get; set; }

        [Display(Name = "Ngày đặt hàng")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Trạng thái")]
        public OrderStatus Status { get; set; }

        [Display(Name = "Ngày hủy")]
        public DateTime? CancelledDate { get; set; }

        [Display(Name = "Lý do hủy")]
        public string? CancelReason { get; set; }
        public CarViewModel Car { get; set; }
    }
}