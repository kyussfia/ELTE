using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class User
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } //name

        [Required]
        public string Email { get; set; }

        [Required]
        public Boolean IsTakeCare { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public byte[] Password { get; set; }
    }
}
