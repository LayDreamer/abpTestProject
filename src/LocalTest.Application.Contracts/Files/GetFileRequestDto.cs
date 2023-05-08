using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LocalTest.Files
{
    public class GetFileRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
