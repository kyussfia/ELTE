using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Entity
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public Entity()
        {
            Title = "Unknown";

            //UploadedFile = new byte[] { 0x03, 0x10, 0xFF, 0xFF };
        }
    }
}
