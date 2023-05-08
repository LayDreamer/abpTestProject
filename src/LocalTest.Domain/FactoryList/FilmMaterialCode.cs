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
    public class FilmMaterialCode
    {
        [Description("物料编码")]
        public string MaterialCode { get; set; }

        [Description("材料名称")]
        public string MaterialName { get; set; }

        [Description("项目名称")]
        public string ProjectName { get; set; }

        [Description("规格")]
        public string Specification { get; set; }

        [Description("型号")]
        public string Type { get; set; }

        [Description("单位")]
        public string Unit { get; set; }

        [Description("颜色")]
        public string Color { get; set; }

    }
}
