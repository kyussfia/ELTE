using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Zh.Models.ViewModel
{
    public class NewErrorViewModel
    {
        [Required(ErrorMessage = "Cannot make new error without title.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Cannot make new error without description.")]
        public string Description { get; set; }

        [Display(Name = "Picture")]
        public IFormFile Picture { get; set; }
    }
}
