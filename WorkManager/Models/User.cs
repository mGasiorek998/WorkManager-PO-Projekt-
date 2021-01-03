using System;
using System.ComponentModel.DataAnnotations;

namespace WorkManager.Models
{
    public class User {
        [Key]
        public int Id {get; set;}
        
        [Required]
        [MaxLength(50)]
        public String Email { get; set; }
        
        [Required]
        [MaxLength(20)]
        public String Username { get; set; }

        [Required]
        [MaxLength(20)]
        public String Password { get; set; }
    }
}
