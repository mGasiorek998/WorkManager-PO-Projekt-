using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkManager.Models
{
    class Task
    {
        [Key]
        public int Id { get; set; }

        public User User { get; set; }

        [Required]
        [MaxLength(100)]
        public String TaskTitle { get; set; }

        [Required]
        [MaxLength(420)]
        public String TaskDesc { get; set; }

        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public String Status { get; set; }

    }
}
