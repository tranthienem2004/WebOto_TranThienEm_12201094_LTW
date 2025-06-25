using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebOto_TranThienEm_12201094_LTW.Models
{
    public class User : IdentityUser
    {
        [StringLength(100)]
        public string? FullName { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}