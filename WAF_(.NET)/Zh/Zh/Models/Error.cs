using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Zh.Models
{
    public class Error
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public byte[] Picture { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public int NumOfRequests { get; set; }
    }
}
