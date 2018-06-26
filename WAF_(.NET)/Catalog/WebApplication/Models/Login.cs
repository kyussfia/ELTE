
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Login
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }


        [Required]
        public string Place { get; set; }

        [Required]
        public string Spec { get; set; }

        //[Required]
        //public int AdId { get; set; }
    }
}
