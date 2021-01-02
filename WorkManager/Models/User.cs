using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkManager.Models {
    class User {
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

        public new string ToString => Username;
    }
}
