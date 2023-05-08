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
    /// 拆单表 - 总单（膜）
    /// </summary>
    public class TotalOrderFilm
    {
        [Description("区域")]
        public string Region { get; set; }

        [Description("名称")]
        public string Name { get; set; }

        [Description("物料编码")]
        public string MaterialCode { get; set; }

        [Description("规格（颜色，型号）")]
        public string Specifications { get; set; }

        [Description("总用量")]
        public int TotalUsage { get; set; }

        [Description("单位")]
        public string Unit { get; set; }

        [Description("备注")]
        public string Remark { get; set; }

    }
}
