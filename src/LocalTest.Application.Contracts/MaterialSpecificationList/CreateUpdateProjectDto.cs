using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LocalTest.MaterialSpecificationList
{
    public class CreateUpdateProjectDto
    {
        /// <summary>
        /// 状态 (有效/作废)
        /// </summary>
        [Required]
        public string Status { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [Required]
        [StringLength(128)]
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
