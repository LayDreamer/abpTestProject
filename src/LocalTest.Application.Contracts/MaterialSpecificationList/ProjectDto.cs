using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace LocalTest.MaterialSpecificationList
{
    public class ProjectDto : AuditedEntityDto<Guid>
    {
        /// <summary>
        /// 状态 (有效/作废)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }
    }
}
