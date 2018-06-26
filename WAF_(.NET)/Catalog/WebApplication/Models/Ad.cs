using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Ad
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SubjectName { get; set; }

        [Required]
        public int TeacherId { get; set; }

        public virtual User Teacher { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public DateTime StartAt { get; set; }

        [Required]
        public int duration { get; set; }
    }
}
