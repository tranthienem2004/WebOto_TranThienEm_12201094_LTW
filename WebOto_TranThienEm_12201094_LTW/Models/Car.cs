using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebOto_TranThienEm_12201094_LTW.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        public int BrandId { get; set; }
        public Brand? Brand { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Mileage { get; set; }

        [StringLength(50)]
        public string? FuelType { get; set; }

        [StringLength(50)]
        public string? Transmission { get; set; }

        [StringLength(50)]
        public string? Color { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [StringLength(500)]
        public string?   ImageUrl { get; set; }

        public bool IsAvailable { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string? Location { get; set; }

        [StringLength(20)]
        public string? ContactPhone { get; set; }
        public bool IsFeatured { get; set; } = false;
    }
}