using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace LocalTest.FactoryList
{
    public class RequisitionListDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        /// <summary>
        /// 文件存储地址
        /// </summary>
        public string FilePath { get; set; }
    }
}
