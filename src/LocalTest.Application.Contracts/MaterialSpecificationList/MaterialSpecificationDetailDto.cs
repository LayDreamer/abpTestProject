using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace LocalTest.MaterialSpecificationList
{
    public class MaterialSpecificationDetailDto : AuditedEntityDto<Guid>
    {

        /// <summary>
        /// 备料编号
        /// </summary>
        public string MaterialNumber { get; set; }

        /// <summary>
        /// 清单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 购销合同编码
        /// </summary>
        public string PurchaseAndSalesContractCode { get; set; }

        /// <summary>
        /// 工厂立项编码
        /// </summary>
        public string FactoryProjectApproval { get; set; }

        /// <summary>
        /// 户型名称
        /// </summary>
        public string HouseTypeName { get; set; }

        /// <summary>
        /// 使用区域
        /// </summary>
        public string UsingZones { get; set; }

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

        /// <summary>
        /// 图纸长（mm）
        /// </summary>
        public double DrawingLength { get; set; }


        /// <summary>
        /// 图纸宽（mm）
        /// </summary>
        public double DrawingWidth { get; set; }

        /// <summary>
        /// 图纸厚（mm）
        /// </summary>
        public double DrawingThickness { get; set; }

        /// <summary>
        /// 展开面（mm）
        /// </summary>
        public double ExtendedSurface { get; set; }

        /// <summary>
        /// 物料数量
        /// </summary>
        public double MaterialCount { get; set; }

        /// <summary>
        /// 物料使用单位
        /// </summary>
        public string MaterialUnit { get; set; }

        /// <summary>
        /// 设计色号
        /// </summary>
        public string DesignColorNumber { get; set; }

        /// <summary>
        /// 加工说明
        /// </summary>
        public string ProcessingInstruction { get; set; }


        /// <summary>
        /// 材质型号
        /// </summary>
        public string MaterialType { get; set; }


        /// <summary>
        /// 纹路方向
        /// </summary>
        public string GrainDirection { get; set; }


        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// 供应商长（mm）
        /// </summary>
        public string SupplierLength { get; set; }

        /// <summary>
        /// 供应商宽（mm）
        /// </summary>
        public string SupplierWidth { get; set; }

        /// <summary>
        /// 供应商厚（mm）
        /// </summary>
        public string SupplierThickness { get; set; }

        /// <summary>
        ///纹理
        /// </summary>
        public string Vein { get; set; }

        /// <summary>
        ///包覆特性
        /// </summary>
        public string CladdingCharacter { get; set; }


        /// <summary>
        ///材质编码
        /// </summary>
        public string MaterialCode { get; set; }


        /// <summary>
        ///型材图示
        /// </summary>
        public string SectionBarImageFilePath { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}
