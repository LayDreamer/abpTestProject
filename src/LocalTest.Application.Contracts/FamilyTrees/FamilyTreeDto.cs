using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using EasyAbp.Abp.Trees;

namespace LocalTest.Families
{
    public class FamilyTreeDto : AuditedEntityDto<Guid>
    {
        public Guid? ParentId { get; set; }
        public string Code { get; set; }
        public int Level { get; set; }
        public FamilyTreeDto Parent { get; set; }
        public ICollection<FamilyTreeDto> Children { get; set; }
        public string DisplayName { get; set; }
        public string BlobName { get; set; }
    }
}
