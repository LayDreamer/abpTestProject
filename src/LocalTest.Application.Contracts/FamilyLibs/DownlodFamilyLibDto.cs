using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using EasyAbp.Abp.Trees;

namespace LocalTest.FamilyLibs
{
    public class DownlodFamilyLibDto : AuditedEntityDto<Guid>
    {
        public string DisplayName { get; set; }

        public string FilePath { get; set; }

    }
}
