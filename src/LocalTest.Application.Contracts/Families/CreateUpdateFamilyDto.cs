using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LocalTest.Families
{
    public class CreateUpdateFamilyDto
    {
        //[Required]
        //[StringLength(128)]
        public string ProcductName { get; set; }

        public string FileName { get; set; }

        public string FileSize { get; set; }
    }
}
