using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LocalTest.Files
{
    public class UpLoadFileDto
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile File { get; set; }

        public string Name { get; set; }
    }
}
