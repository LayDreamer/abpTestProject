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
    /// 拆单表 - 总单（板）
    /// </summary>
    public class TotalOrderFloor
    {
        [Description("区域")]
        public string Region { get; set; }

        [Description("总套数")]
        public string TotalNumberOfSets { get; set; }

        [Description("板材材质")]
        public string PlateMaterial { get; set; }

        [Description("合计-长")]
        public string TotalLength { get; set; }

        [Description("合计-宽")]
        public string TotalWidth { get; set; }

        [Description("合计-高")]
        public string TotalHeight { get; set; }

        [Description("品牌")]
        public string Brand { get; set; }

        [Description("饰面")]
        public string DecorativeSurface { get; set; }

        [Description("总计（张）")]
        //总计（张）
        public double Total { get; set; }

        [Description("生产制造方")]
        public string Manufacturer { get; set; }

        [Description("面积（㎡）")]
        //面积（㎡）
        public string Area { get; set; }
    }
}
