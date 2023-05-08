using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace LocalTest.FactoryList
{
    /// <summary>
    /// 膜物料编码管理表
    /// </summary>
    public class FloorMaterialCode
    {
        [Description("物料编码")]
        public string MaterialCode { get; set; }

        [Description("材料名称")]
        public string MaterialName { get; set; }

        [Description("项目名称")]
        public string ProjectName { get; set; }

        [Description("尺寸")]
        public string Size { get; set; }

        [Description("生产批号")]
        public string BatchNumber { get; set; }

        [Description("单位")]
        public string Unit { get; set; }

    }
}
