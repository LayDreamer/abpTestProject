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
    /// 拆单表 - 车间五金
    /// </summary>
    public class WorkshopHardware
    {
        [Description("房号")]//户型
        public string HouseType { get; set; }

        /// <summary>
        /// 房间
        /// </summary>
       /* public string Room { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 产品编码
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        /// 产品长
        /// </summary>
        public string ProjectLength { get; set; }

        /// <summary>
        /// 产品宽
        /// </summary>
        public string ProjectWidth { get; set; }

        /// <summary>
        /// 产品高
        /// </summary>
        public string ProjectHeight { get; set; }

        /// <summary>
        /// 产品小计
        /// </summary>
        public string ProjectTotal { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// 结构类别
        /// </summary>
        public string StructType { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 模块编码
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 安装码
        /// </summary>
        public string InstallCode { get; set; }

        /// 模块长
        /// </summary>
        public string ModuleLength { get; set; }

        /// <summary>
        /// 模块宽
        /// </summary>
        public string ModuleWidth { get; set; }

        /// <summary>
        /// 模块高
        /// </summary>
        public string ModuleHeight { get; set; }

        /// <summary>
        /// 模组类型
        /// </summary>
        public string ModuleType { get; set; }

        /// <summary>
        /// 模块小计
        /// </summary>
        public string ModuleTotal { get; set; }*/

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

        /// <summary>
        /// [Description("物料-厚")]
        /// </summary>
       /* public string MaterialThickness { get; set; }

        /// <summary>
        /// [Description("折弯尺寸")]
        /// </summary>
        public string ZWSize { get; set; }

        /// <summary>
        /// [Description("规格")]
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// [Description("品牌")]
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// [Description("型号")]
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// [Description("物料小计")]
        /// </summary>
        public string MaterialTotal { get; set; }*/

        [Description("单套")]
        public double SingleSet { get; set; }

        [Description("批量")]
        public double Batch { get; set; }

        /// <summary>
        /// [Description("总配额")]
        /// </summary>
        //public string TotalQuota { get; set; }

        [Description("单位")]
        public string Unit { get; set; }

        /// <summary>
        /// [Description("物料类型")]
        /// </summary>
       /* public string MaterialType { get; set; }

        /// <summary>
        /// [Description("加工是/否")]
        /// </summary>
        public string IsProcess { get; set; }

        /// <summary>
        /// [Description("高低位信息")]
        /// </summary>
        public string HighLowInfo { get; set; }

        /// <summary>
        /// [Description("图号")]
        /// </summary>
        public string DrawingNo { get; set; }

        /// <summary>
        /// [Description("加工说明")]
        /// </summary>
        public string ProcessInfo { get; set; }


        /// <summary>
        /// [Description("备注")]
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// [Description("视口码")]
        /// </summary>
        public string ViewportCode { get; set; }

        /// <summary>
        /// [Description("输入尺寸")]
        /// </summary>
        public string InputSize { get; set; }*/

        [Description("物料优先级与打孔信息")]
        public string PriorityInformation { get; set; }
    }
}
