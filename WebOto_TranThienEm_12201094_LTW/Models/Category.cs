﻿using System.ComponentModel.DataAnnotations;

namespace WebOto_TranThienEm_12201094_LTW.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}