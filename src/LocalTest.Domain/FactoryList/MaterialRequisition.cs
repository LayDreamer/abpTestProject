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
    /// 导出的领料单
    /// </summary>
    public class MaterialRequisition
    {
        [Description("序号")]
        public int Number { get; set; }

        [Description("物料名称")]
        public string MaterialName { get; set; }

        [Description("物料编码")]
        public string MaterialCode { get; set; }

        [Description("长")]
        public string Length { get; set; }

        [Description("宽")]
        public string Width { get; set; }

        [Description("高")]
        public string Height { get; set; }

        [Description("型号")]
        public string Type { get; set; }

        [Description("单套")]
        public string SingleSet { get; set; }

        [Description("批量")]
        public double Batch { get; set; }

        [Description("需求总量")]
        public int TotalDemand { get; set; }

        [Description("单位")]
        public string Unit { get; set; }

        [Description("备注1")]
        public string Remark1 { get; set; }

        [Description("备注2")]
        public string Remark2 { get; set; }

        [Description("物料批次")]
        public string MaterialBatch { get; set; }

        [Description("领料日期")]
        public string PickingDate { get; set; }

        /// <summary>
        /// 需求总量分类
        /// </summary>
        public TotalDemandType DemandType { get; set; }
    }
}
