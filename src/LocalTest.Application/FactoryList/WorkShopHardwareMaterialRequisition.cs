using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace LocalTest.FactoryList
{
    /// <summary>
    /// 领料单-车间五金
    /// </summary>

    public class WorkShopHardwareMaterialRequisition : MaterialRequisitionBase
    {
        public List<WorkshopHardware> workshopHardwares = new();

        public WorkshopHardware workshopHardware;

        public WorkShopHardwareMaterialRequisition(TotalDemandType totalDemandType) : base(totalDemandType)
        {

        }

        public override void Excute()
        {
            try
            {
                foreach (var item in workshopHardwares)
                {
                    if (item.MaterialName.Contains(Floor) || item.MaterialName.Contains(Film))
                        continue;
                    materialRequisition = new MaterialRequisition() { DemandType = totalDemandType };
                    workshopHardware = item;
                    CalcData();
                    materialRequisitionList.Add(materialRequisition);
                }
            }
            catch (Exception)
            {
                throw new UserFriendlyException("处理总单-车间五金数据错误！");
            }
        }

        public override void GetMaterialName()
        {
            materialRequisition.MaterialName = workshopHardware.MaterialName;
        }

        public override void GetMaterialCode()
        {
            materialRequisition.MaterialCode = workshopHardware.MaterialCode;
        }

        public override void GetLength()
        {
            materialRequisition.Length = workshopHardware.MaterialLength;
        }

        public override void GetWidth()
        {
            materialRequisition.Width = workshopHardware.MaterialWidth;
        }

        public override void GetHeight()
        {
            materialRequisition.Height = workshopHardware.MaterialHeight;
        }


        public override void GetBatch()
        {
            materialRequisition.Batch = totalDemandType == TotalDemandType.Template ? workshopHardware.SingleSet : workshopHardware.Batch;
        }

        public override void GetTotalDemand()
        {
            int.TryParse(Math.Ceiling(materialRequisition.Batch).ToString(), out int result);
            materialRequisition.TotalDemand = result;
        }

        public override void GetUnit()
        {
            materialRequisition.Unit = workshopHardware.Unit;
        }

        public override void GetRemark1()
        {
            if (workshopHardware.PriorityInformation.Contains(Bore))
            {
                materialRequisition.Remark1 = workshopHardware.PriorityInformation;
            }
        }

        public override void GetRemark2()
        {
            materialRequisition.Remark2 = WorkshopHardware;
        }
    }
}
