using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp;

namespace LocalTest.FactoryList
{
    /// <summary>
    /// 领料单-膜
    /// </summary>

    public class FilmMaterialRequisition : MaterialRequisitionBase
    {
        public List<TotalOrderFilm> totalOrderFilms = new();
        public List<FilmMaterialCode> filmMaterialCodes = new();
        public TotalOrderFilm orderFilm;
        public FilmMaterialCode filmMaterialCode;

        public FilmMaterialRequisition(TotalDemandType totalDemandType) : base(totalDemandType)
        {

        }

        public override void Excute()
        {
            try
            {
                foreach (var totalOrderFilm in totalOrderFilms)
                {
                    if (!totalOrderFilm.Name.Contains(Film))
                        continue;
                    materialRequisition = new MaterialRequisition();
                    orderFilm = totalOrderFilm;
                    filmMaterialCode = GetTargetMaterialCode(orderFilm.Specifications.Trim());
                    CalcData();
                    materialRequisitionList.Add(materialRequisition);
                }
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException($"处理总单-膜数据错误！{ex.Message}");
            }
        }
        public override void GetMaterialName()
        {
            if (filmMaterialCode != null)
            {
                materialRequisition.MaterialName = filmMaterialCode.MaterialName;
            }
        }

        public override void GetMaterialCode()
        {
            if (filmMaterialCode != null)
            {
                materialRequisition.MaterialCode = filmMaterialCode.MaterialCode;
            }
        }

        public override void GetLength()
        {
            materialRequisition.Length = GetMaterialCodeSpec(0);
        }

        public override void GetWidth()
        {
            materialRequisition.Width = GetMaterialCodeSpec(1);
        }

        public override void GetHeight()
        {
            materialRequisition.Height = GetMaterialCodeSpec(2);
        }

        public override void GetMaterialType()
        {
            materialRequisition.Type = GetSplitOrderFilmSpec(orderFilm.Specifications.Trim(), out string insideValue);
        }

        public override void GetBatch()
        {
            materialRequisition.Batch = orderFilm.TotalUsage;
        }

        public override void GetTotalDemand()
        {
            double coefficient = materialRequisition.DemandType == TotalDemandType.Template ? TemplateCoefficient : BatchCoefficient;
            int.TryParse(Math.Ceiling(materialRequisition.Batch * coefficient).ToString(), out int result);
            materialRequisition.TotalDemand = result;
        }


        public override void GetUnit()
        {
            if (filmMaterialCode != null)
            {
                materialRequisition.Unit = filmMaterialCode.Unit;
            }
            //materialRequisition.Unit = orderFilm.Unit;
        }
        public override void GetRemark2()
        {
            materialRequisition.Remark2 = WSKBWW;
        }

        public override void GetMaterialBatch()
        {
            //var result = orderFilm.Specifications.Trim().Split().Where(x => x.StartsWith("（") && x.EndsWith("）")).ToList();
            GetSplitOrderFilmSpec(orderFilm.Specifications.Trim(), out string insideValue);
            materialRequisition.MaterialBatch = insideValue;
        }


        #region 方法

        public FilmMaterialCode GetTargetMaterialCode(string spec)
        {
            if (string.IsNullOrEmpty(spec))
                return null;
            string res = GetSplitOrderFilmSpec(spec, out string insideValue);
            FilmMaterialCode filmMaterialCode = filmMaterialCodes.Find(e => e.Type == res || e.Color == res);
            return filmMaterialCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec">总单膜规格</param>
        /// <param name="insideValue">括号中的内容</param>
        /// <returns></returns>
        public string GetSplitOrderFilmSpec(string spec, out string insideValue)
        {
            string res = spec;
            insideValue = string.Empty;

            string[] splitSpec = Array.Empty<string>();
            if (spec.Contains('('))
            {
                splitSpec = orderFilm.Specifications.Trim().Split("(");
                res = orderFilm.Specifications.Trim().Split("(")[0];
            }
            else if (spec.Contains('（'))
            {
                splitSpec = orderFilm.Specifications.Trim().Split("（");
                res = orderFilm.Specifications.Trim().Split("（")[0];

            }
            if (spec.Contains(')') || spec.Contains('）'))
            {
                insideValue = spec.Contains(')') ? splitSpec[1].Replace(")", "") : splitSpec[1].Replace("）", "");
            }
            return res;
        }

        /// <summary>
        /// 获取膜物料编码规格
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string GetMaterialCodeSpec(int i)
        {
            //string spec = materialCode.Specification.Trim();
            string splitSpec = "";
            if (filmMaterialCode != null)
            {
                splitSpec = filmMaterialCode.Specification.Trim().Split('*')[i];
            }
            return splitSpec;
        }

        #endregion
    }
}
