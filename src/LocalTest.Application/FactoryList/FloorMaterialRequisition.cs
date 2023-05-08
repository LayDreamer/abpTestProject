using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace LocalTest.FactoryList
{
    /// <summary>
    /// 领料单-板
    /// </summary>

    public class FloorMaterialRequisition : MaterialRequisitionBase
    {
        public List<TotalOrderFloor> totalOrderFloors = new();
        public List<FloorMaterialCode> floorMaterialCodes = new();
        public TotalOrderFloor orderFloor;
        public FloorMaterialCode floorMaterialCode;
        public FloorMaterialRequisition(TotalDemandType totalDemandType) : base(totalDemandType)
        {

        }

        public override void Excute()
        {
            try
            {
                foreach (var totalOrderFloor in totalOrderFloors)
                {
                    if (!totalOrderFloor.PlateMaterial.Contains(Floor))
                        continue;
                    materialRequisition = new MaterialRequisition() { DemandType = totalDemandType };
                    orderFloor = totalOrderFloor;
                    floorMaterialCode = floorMaterialCodes.Find(e => MatchFloorCode(orderFloor, e));
                    CalcData();
                    materialRequisitionList.Add(materialRequisition);
                }
            }
            catch (Exception)
            {
                throw new UserFriendlyException("处理总单-板数据错误！");
            }
        }

        public override void GetMaterialName()
        {
            materialRequisition.MaterialName = orderFloor.PlateMaterial;
        }

        public override void GetMaterialCode()
        {
            //string orderSize = $"{orderFloor.TotalLength + "*" + orderFloor.TotalWidth + "*" + orderFloor.TotalHeight}".Trim();
            //e.Size.Trim() == orderSize
            //var floorMaterialCode = floorMaterialCodes.Find(e => MatchFloorCode(orderFloor, e));
            
            if (floorMaterialCode != null)
            {
                materialRequisition.MaterialCode = floorMaterialCode.MaterialCode;
            }

        }

        public override void GetLength()
        {
            materialRequisition.Length = orderFloor.TotalLength;
        }

        public override void GetWidth()
        {
            materialRequisition.Width = orderFloor.TotalWidth;
            if (double.Parse(orderFloor.TotalWidth) == WJJ && !orderFloor.PlateMaterial.Contains(KEYAN))
            {
                materialRequisition.Width = YEEL.ToString();
            }
        }

        public override void GetHeight()
        {
            materialRequisition.Height = orderFloor.TotalHeight;
        }

        public override void GetBatch()
        {
            materialRequisition.Batch = orderFloor.Total;
            if (double.Parse(orderFloor.TotalWidth) == WJJ && !orderFloor.PlateMaterial.Contains(KEYAN))
            {
                materialRequisition.Batch = Math.Ceiling(orderFloor.Total * 0.5);
            }
        }

        public override void GetTotalDemand()
        {
            double coefficient = materialRequisition.DemandType == TotalDemandType.Template ? TemplateCoefficient : BatchCoefficient;
            int.TryParse(Math.Ceiling(materialRequisition.Batch * coefficient).ToString(), out int result);
            materialRequisition.TotalDemand = result;
        }

        public override void GetUnit()
        {
            if (floorMaterialCode != null)
            {
                materialRequisition.Unit = floorMaterialCode.Unit;
            }
        }

        public override void GetRemark1()
        {
            if (double.Parse(orderFloor.TotalWidth) == WJJ)
            {
                materialRequisition.Remark1 = WJJ + SmallFloor;
            }
        }

        public override void GetRemark2()
        {
            materialRequisition.Remark2 = WSKBWW;
        }

        /// <summary>
        /// 总单板匹配板材质编码
        /// </summary>
        /// <param name="totalOrderFloor"></param>
        /// <param name="floorMaterialCode"></param>
        /// <returns></returns>
        public bool MatchFloorCode(TotalOrderFloor totalOrderFloor, FloorMaterialCode floorMaterialCode)
        {
            bool isMatch = false;
            var splitManufacturer = totalOrderFloor.Manufacturer.Trim().Split('*');
            var splitSize = floorMaterialCode.Size.Trim().Split('*');
            if (totalOrderFloor.PlateMaterial.Contains(LJKLB))
            {
                if (floorMaterialCode.MaterialName == LJKLB
                    && double.Parse(splitManufacturer[0]) == double.Parse(splitSize[0])
                    && double.Parse(splitManufacturer[1]) == double.Parse(splitSize[1]))
                {
                    isMatch = true;
                }
            }
            else
            {
                if (
                    floorMaterialCode.MaterialName == totalOrderFloor.PlateMaterial
                    && double.Parse(splitManufacturer[0]) == double.Parse(splitSize[0])
                    && double.Parse(splitManufacturer[1]) == double.Parse(splitSize[1])
                    && double.Parse(splitManufacturer[2]) == double.Parse(splitSize[2]))
                {
                    isMatch = true;
                }
            }
            return isMatch;
        }
    }
}
