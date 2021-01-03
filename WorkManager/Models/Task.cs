using System;
using System.ComponentModel.DataAnnotations;

namespace WorkManager.Models
{
    class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int userID { get; set; }

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
