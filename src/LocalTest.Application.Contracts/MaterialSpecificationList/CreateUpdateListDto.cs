using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LocalTest.MaterialSpecificationList
{
    public class CreateUpdateListDto
    {
        /// <summary>
        /// 清单名称
        /// </summary>
        [Required]
        [StringLength(128)]
        public string FileName { get; set; }
        
        // <summary>
        // 清单编码
        // </summary>
        public string MaterialNumber { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [StringLength(128)]
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        [Required]
        [StringLength(128)]
        public string ProjectCode { get; set; }

        /// <summary>
        /// 文件地址
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }
    }
}
