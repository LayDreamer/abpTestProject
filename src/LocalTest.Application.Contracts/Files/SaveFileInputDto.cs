using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LocalTest.Files
{
    public class SaveFileInputDto
    {
        public byte[] Content { get; set; }

        [Required]
        public string Name { get; set; }
    }

}
