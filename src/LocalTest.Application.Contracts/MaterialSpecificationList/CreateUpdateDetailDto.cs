using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LocalTest.MaterialSpecificationList
{
    public class CreateUpdateDetailDto
    {
        /// <summary>
        /// 清单名称
        /// </summary>
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
       
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
        public string ProjectCode { get; set; }

        /// <summary>
        /// 产品平台
        /// </summary>
       
        public string ProductPlatform { get; set; }

        /// <summary>
        /// 产品系统
        /// </summary>
       
        public string ProductSystem { get; set; }

        /// <summary>
        /// 部品位置
        /// </summary>
      
        public string ComponentPosition { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }
    }
}
