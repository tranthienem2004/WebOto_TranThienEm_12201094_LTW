using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebOto_TranThienEm_12201094_LTW.Models.ViewModels
{
    public class CarCreateEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên xe là bắt buộc.")]
        [StringLength(200, ErrorMessage = "Tên xe không được vượt quá 200 ký tự.")]
        [Display(Name = "Tên Xe")]
        public string Name { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Giá là bắt buộc.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Giá")]
        public decimal Price { get; set; }

        [Display(Name = "URL Hình Ảnh")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Năm sản xuất là bắt buộc.")]
        [Range(1900, 2100, ErrorMessage = "Năm không hợp lệ.")]
        [Display(Name = "Năm Sản Xuất")]
        public int Year { get; set; }

        [Display(Name = "Số Km đã đi")]
        [Range(0, int.MaxValue, ErrorMessage = "Số km không hợp lệ.")]
        public int Mileage { get; set; }

        [Display(Name = "Loại Nhiên Liệu")]
        public string FuelType { get; set; }

        [Display(Name = "Hộp Số")]
        public string Transmission { get; set; }

        [Display(Name = "Màu Sắc")]
        public string Color { get; set; }

        [Display(Name = "Địa Điểm")]
        public string Location { get; set; }

        [Display(Name = "Số Điện Thoại Liên Hệ")]
        public string ContactPhone { get; set; }

        [Display(Name = "Còn Hàng")]
        public bool IsAvailable { get; set; }

        [Required(ErrorMessage = "Danh mục là bắt buộc.")]
        [Display(Name = "Danh mục")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        [Required(ErrorMessage = "Thương hiệu là bắt buộc.")]
        [Display(Name = "Thương hiệu")]
        public int BrandId { get; set; }

        public IEnumerable<SelectListItem> Brands { get; set; }
    }
}