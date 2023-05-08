using LocalTest.MaterialSpecificationList;
using LocalTest.Files;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.Linq;
using LocalTest.Utils;
using System.Net.Http.Headers;
using Volo.Abp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using LocalTest.FactoryList;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace LocalTest.Web.Pages.FactoryList
{
    public class CreateModalModel : LocalTestPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public UpLoadFileDto SplitListFileDto { get; set; }

        [BindProperty]
        public UpLoadFileDto FilmFloorCodeFileDto { get; set; }

        [BindProperty]
        public CreateUpdateMaterialRequisitionDto RequisitionDto { get; set; }

        [BindProperty]
        public UploadMaterialRequisition UploadDto { get; set; }

        private readonly FileAppService _fileAppService;
        private readonly ProjectAppService _projectAppService;
        private readonly MaterialRequisitionService _requisitionService;


        public CreateModalModel(FileAppService fileAppService, ProjectAppService projectAppService, MaterialRequisitionService requisitionService)
        {
            _fileAppService = fileAppService;
            _projectAppService = projectAppService;
            _requisitionService = requisitionService;
        }

        public void OnGet()
        {
            SplitListFileDto = new UpLoadFileDto();
            FilmFloorCodeFileDto = new UpLoadFileDto();
            RequisitionDto = new CreateUpdateMaterialRequisitionDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            List<MaterialRequisition> materialRequisitions = new List<MaterialRequisition>();
            ///拆单表-总单-板
            List<TotalOrderFloor> totalOrderFloors = new();
            ///拆单表-总单-膜
            List<TotalOrderFilm> totalOrderFilms = new();
            ///拆单表-车间五金
            List<WorkshopHardware> workshopHardwares = new();
            ///拆单表-发货五金
            List<ShippingHardware> shippingHardwares = new();
            ///膜物料编码
            List<FilmMaterialCode> filmMaterialCodes = new();
            ///板材物料编码
            List<FloorMaterialCode> floorMaterialCodes = new();
            try
            {
                #region 解析上传的Excel文件
                ExcelHelper excelHelper = new ExcelHelper();

                totalOrderFloors = await excelHelper.ParseExcelAsync<TotalOrderFloor>(SplitListFileDto.File, Common.LocalFactoryListPath, "总单", 6, 11);
                floorMaterialCodes = await excelHelper.ParseExcelAsync<FloorMaterialCode>(FilmFloorCodeFileDto.File, Common.LocalFactoryListPath, "板", 3, 8);

                //数据从第6行开始+数据总数+总计和空白行2行
                int splitTotalCount = 6 + totalOrderFloors.Count() + 2;
                totalOrderFilms = await excelHelper.ParseExcelAsync<TotalOrderFilm>(SplitListFileDto.File, Common.LocalFactoryListPath, "总单", splitTotalCount, 11);
                filmMaterialCodes = await excelHelper.ParseExcelAsync<FilmMaterialCode>(FilmFloorCodeFileDto.File, Common.LocalFactoryListPath, "膜", 3, 8);

                if (UploadDto.IsWorkShop)
                {
                    workshopHardwares = await excelHelper.ParseExcelAsync<WorkshopHardware>(SplitListFileDto.File, Common.LocalFactoryListPath, "车间五金", 6, -1);
                }

                if (UploadDto.IsShipping)
                {
                    shippingHardwares = await excelHelper.ParseExcelAsync<ShippingHardware>(SplitListFileDto.File, Common.LocalFactoryListPath, "发货五金", 6, 45);
                }
                #endregion

                #region 执行赋值操作
                ///板
                FloorMaterialRequisition floorMaterialRequisition = new(UploadDto.DemandType)
                {
                    totalOrderFloors = totalOrderFloors,
                    floorMaterialCodes = floorMaterialCodes,
                };
                floorMaterialRequisition.Excute();

                ///膜
                FilmMaterialRequisition filmMaterialRequisition = new(UploadDto.DemandType)
                {
                    totalOrderFilms = totalOrderFilms,
                    filmMaterialCodes = filmMaterialCodes
                };
                filmMaterialRequisition.Excute();

                ///车间五金
                WorkShopHardwareMaterialRequisition workShopHardwareMaterialRequisition = new WorkShopHardwareMaterialRequisition(UploadDto.DemandType)
                {
                    workshopHardwares = workshopHardwares,
                };
                workShopHardwareMaterialRequisition.Excute();

                ///发货五金
                ShippingHardwareMaterialRequisition shippingHardwareMaterialRequisition = new ShippingHardwareMaterialRequisition(UploadDto.DemandType)
                {
                    shippingHardwares = shippingHardwares,
                };
                shippingHardwareMaterialRequisition.Excute();
                #endregion

                materialRequisitions.AddRange(floorMaterialRequisition.materialRequisitionList);
                materialRequisitions.AddRange(filmMaterialRequisition.materialRequisitionList);
                materialRequisitions.AddRange(workShopHardwareMaterialRequisition.materialRequisitionList);
                materialRequisitions.AddRange(shippingHardwareMaterialRequisition.materialRequisitionList);
                materialRequisitions.ForEach(e => e.Number = materialRequisitions.IndexOf(e) + 1);

                string fileName = "领料单模板.xlsx";
                string resFileName = "领料单导出文件.xlsx";
                string filePath = Path.Combine(Common.ServerLocalMaterialRequisitionPath, fileName);
                string resDir = Path.Combine(Common.ServerLocalMaterialRequisitionPath, CurrentUser.UserName);
                string resFilePath = Path.Combine(resDir, resFileName);

                ///第6行表头，第7行号数据
                await excelHelper.OutPutExcel(filePath, resFilePath, materialRequisitions, 6, 7);

                RequisitionDto.Name = resFileName;
                RequisitionDto.FilePath = $"{Common.ServerWebFactoryListPathHttps}{CurrentUser.UserName}/{resFileName}";
                var resultDto = await _requisitionService.GetListByInputAsync(new OrderNotificationSearchDto { Key = resFileName });
                var requisition = resultDto.Items.FirstOrDefault();
                if (requisition == null)
                {
                    await _requisitionService.CreateAsync(RequisitionDto);
                }
                else
                {
                    await _requisitionService.UpdateAsync(requisition.Id, RequisitionDto);
                }
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException($"Excel数据处理错误:{ex.Message}");
            }

            return Page();
        }



        public class UploadMaterialRequisition
        {
            /// <summary>
            /// 项目类型
            /// </summary>
            //[DynamicFormIgnore]
            [Required]
            public TotalDemandType DemandType { get; set; }

            /// <summary>
            /// 是否添加车间五金
            /// </summary>
            [Display(Name = "车间五金")]
            public bool IsWorkShop { get; set; }

            /// <summary>
            /// 是否添加发货五金
            /// </summary>
            [Display(Name = "发货五金")]
            public bool IsShipping { get; set; }

        }
    }
}
