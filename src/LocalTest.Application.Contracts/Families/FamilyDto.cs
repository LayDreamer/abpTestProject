using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace LocalTest.Families
{
    public class FamilyDto : AuditedEntityDto<Guid>
    {
        public string ProcductName { get; set; }

        public string FileName { get; set; }

        public string FileSize { get; set; }
    }
}
