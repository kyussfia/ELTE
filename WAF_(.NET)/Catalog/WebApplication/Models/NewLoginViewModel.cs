using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class NewLoginViewModel
    {
        public List<string> Species { set; get; }

        [Required]
        [RegularExpression("^[A-Za-z0-9_-]{3,40}$", ErrorMessage = "A hely formátuma, vagy hossza nem megfelelő.")]
        public String Place { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public String Spec { get; set; }
    }
}
