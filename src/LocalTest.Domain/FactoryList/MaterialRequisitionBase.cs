using System;
using System.Collections.Generic;
using System.Text;

namespace LocalTest.FactoryList
{
    public abstract class MaterialRequisitionBase
    {
        public const string Floor = "板";
        public const string SmallFloor = "小板";
        public const string Film = "膜";

        public const string LJKLB = "琉晶颗粒板";

        public const string KEYAN = "科岩";

        public const string WSKBWW = "威斯科博委外";

        public const string WorkshopHardware = "车间五金";

        public const string ShippingHardware = "发货五金";

        public const string Bore = "打孔";
        public const string LLCQ = "领料裁切";

        public const double WJJ = 599;
        public const double YEEL = 1220;

        /// <summary>
        /// （需求总量）样本系数
        /// </summary>
        public const double TemplateCoefficient = 1.05;

        /// <summary>
        /// （需求总量）批量系数
        /// </summary>
        public const double BatchCoefficient = 1.03;


        public List<MaterialRequisition> materialRequisitionList = new();

        /// <summary>
        /// 输入清单数据
        /// </summary>

        //public List<FilmMaterialCode> filmMaterialCodes;
        //public List<FloorMaterialCode> floorMaterialCodes;

        public MaterialRequisition materialRequisition;
        public TotalDemandType totalDemandType;
        public MaterialRequisitionBase(TotalDemandType totalDemandType)
        {
            this.totalDemandType = totalDemandType;
        }

        //public virtual void InitData(List<FilmMaterialCode> _filmMaterialCodes, List<FloorMaterialCode> _floorMaterialCodes)
        //{
        //    filmMaterialCodes = _filmMaterialCodes;
        //    floorMaterialCodes = _floorMaterialCodes;
        //}

        /// <summary>
        /// 执行
        /// </summary>
        public abstract void Excute();

        /// <summary>
        /// 操作赋值
        /// </summary>
        public void CalcData()
        {
            GetMaterialName();
            GetLength();
            GetWidth();
            GetHeight();
            GetMaterialCode();
            GetMaterialType();
            GetSingleSet();
            GetBatch();
            GetTotalDemand();
            GetUnit();
            GetRemark1();
            GetRemark2();
            GetMaterialBatch();
        }

        /// <summary>
        /// 获取物料名称
        /// </summary>
        public abstract void GetMaterialName();


        /// <summary>
        /// 获取物料编码
        /// </summary>
        public abstract void GetMaterialCode();

        /// <summary>
        /// 获取长
        /// </summary>
        public abstract void GetLength();

        /// <summary>
        /// 获取宽
        /// </summary>
        public abstract void GetWidth();


        /// <summary>
        /// 获取高
        /// </summary>
        public abstract void GetHeight();


        /// <summary>
        /// 获取材料类型
        /// </summary>
        public virtual void GetMaterialType()
        {
            materialRequisition.Type = "/";
        }

        /// <summary>
        /// 获取单套
        /// </summary>
        public virtual void GetSingleSet()
        {
            materialRequisition.SingleSet = "/";
        }

        /// <summary>
        /// 获取批量
        /// </summary>
        public abstract void GetBatch();


        /// <summary>
        /// 获取需求总量
        /// </summary>
        public abstract void GetTotalDemand();

        /// <summary>
        /// 获取单位
        /// </summary>
        public abstract void GetUnit();

        /// <summary>
        /// 获取备注1
        /// </summary>
        public virtual void GetRemark1()
        {
            materialRequisition.Remark1 = string.Empty;
        }

        /// <summary>
        /// 获取备注2
        /// </summary>
        public abstract void GetRemark2();


        /// <summary>
        /// 获取物料批次
        /// </summary>
        public virtual void GetMaterialBatch()
        {
            materialRequisition.MaterialBatch = string.Empty;
        }
    }
}
