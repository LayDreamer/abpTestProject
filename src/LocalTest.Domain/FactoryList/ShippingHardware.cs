using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalTest.FactoryList
{
    public class ShippingHardware
    {
        [Description("户型")]
        public string HouseType { get; set; }

        [Description("半成品编码")]
        public string SemiFinishedProductCode { get; set; }

        [Description("物料名称")]
        public string MaterialName { get; set; }

        [Description("物料编码")]
        public string MaterialCode { get; set; }

        [Description("物料-长")]
        public string MaterialLength { get; set; }

        [Description("物料-宽")]
        public string MaterialWidth { get; set; }

        [Description("物料-高")]
        public string MaterialHeight { get; set; }

        [Description("型号")]
        public string Type { get; set; }

        [Description("单套")]
        public double SingleSet { get; set; }

        [Description("批量")]
        public double Batch { get; set; }

        [Description("单位")]
        public string Unit { get; set; }

        [Description("备注")]
        public string Remark { get; set; }

        [Description("物料优先级与打孔信息")]
        public string PriorityInformation { get; set; }
    }
}
