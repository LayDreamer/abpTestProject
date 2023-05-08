using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace LocalTest.FactoryList
{
    /// <summary>
    /// 领料单-发货五金
    /// </summary>

    public class ShippingHardwareMaterialRequisition : MaterialRequisitionBase
    {
        public List<ShippingHardware> shippingHardwares = new();
        public ShippingHardware shippingHardware;

        public ShippingHardwareMaterialRequisition(TotalDemandType totalDemandType) : base(totalDemandType)
        {

        }

        public override void Excute()
        {
            try
            {
                foreach (var item in shippingHardwares)
                {
                    if (item.MaterialName.Contains(Floor) || item.MaterialName.Contains(Film))
                        continue;
                    materialRequisition = new MaterialRequisition() { DemandType = totalDemandType };
                    shippingHardware = item;
                    CalcData();
                    materialRequisitionList.Add(materialRequisition);
                }
            }
            catch (Exception)
            {
                throw new UserFriendlyException("处理总单-发货五金数据错误！");
            }
        }

        public override void GetMaterialName()
        {
            materialRequisition.MaterialName = shippingHardware.MaterialName;
        }

        public override void GetMaterialCode()
        {
            materialRequisition.MaterialCode = shippingHardware.MaterialCode;
        }

        public override void GetLength()
        {
            materialRequisition.Length = shippingHardware.MaterialLength;
        }

        public override void GetWidth()
        {
            materialRequisition.Width = shippingHardware.MaterialWidth;
        }

        public override void GetHeight()
        {
            materialRequisition.Height = shippingHardware.MaterialHeight;
        }


        public override void GetBatch()
        {
            materialRequisition.Batch = totalDemandType == TotalDemandType.Template ? shippingHardware.SingleSet : shippingHardware.Batch;
        }

        public override void GetTotalDemand()
        {
            int.TryParse(Math.Ceiling(materialRequisition.Batch).ToString(), out int result);
            materialRequisition.TotalDemand = result;
        }
        public override void GetUnit()
        {
            materialRequisition.Unit = shippingHardware.Unit;
        }
        public override void GetRemark1()
        {
            if (shippingHardware.Remark.Contains(LLCQ))
            {
                materialRequisition.Remark1 = shippingHardware.Remark;
            }
            else if (shippingHardware.PriorityInformation.Contains(Bore))
            {
                materialRequisition.Remark1 = shippingHardware.PriorityInformation;
            }
        }

        public override void GetRemark2()
        {
            materialRequisition.Remark2 = ShippingHardware;
        }
    }
}
