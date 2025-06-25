using System.ComponentModel.DataAnnotations;

namespace WebOto_TranThienEm_12201094_LTW.Models
{
    public class Brand
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(200)]
        public string? LogoUrl { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}