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
            ///�𵥱�-�ܵ�-��
            List<TotalOrderFloor> totalOrderFloors = new();
            ///�𵥱�-�ܵ�-Ĥ
            List<TotalOrderFilm> totalOrderFilms = new();
            ///�𵥱�-�������
            List<WorkshopHardware> workshopHardwares = new();
            ///�𵥱�-�������
            List<ShippingHardware> shippingHardwares = new();
            ///Ĥ���ϱ���
            List<FilmMaterialCode> filmMaterialCodes = new();
            ///������ϱ���
            List<FloorMaterialCode> floorMaterialCodes = new();
            try
            {
                #region �����ϴ���Excel�ļ�
                ExcelHelper excelHelper = new ExcelHelper();

                totalOrderFloors = await excelHelper.ParseExcelAsync<TotalOrderFloor>(SplitListFileDto.File, Common.LocalFactoryListPath, "�ܵ�", 6, 11);
                floorMaterialCodes = await excelHelper.ParseExcelAsync<FloorMaterialCode>(FilmFloorCodeFileDto.File, Common.LocalFactoryListPath, "��", 3, 8);

                //���ݴӵ�6�п�ʼ+��������+�ܼƺͿհ���2��
                int splitTotalCount = 6 + totalOrderFloors.Count() + 2;
                totalOrderFilms = await excelHelper.ParseExcelAsync<TotalOrderFilm>(SplitListFileDto.File, Common.LocalFactoryListPath, "�ܵ�", splitTotalCount, 11);
                filmMaterialCodes = await excelHelper.ParseExcelAsync<FilmMaterialCode>(FilmFloorCodeFileDto.File, Common.LocalFactoryListPath, "Ĥ", 3, 8);

                if (UploadDto.IsWorkShop)
                {
                    workshopHardwares = await excelHelper.ParseExcelAsync<WorkshopHardware>(SplitListFileDto.File, Common.LocalFactoryListPath, "�������", 6, -1);
                }

                if (UploadDto.IsShipping)
                {
                    shippingHardwares = await excelHelper.ParseExcelAsync<ShippingHardware>(SplitListFileDto.File, Common.LocalFactoryListPath, "�������", 6, 45);
                }
                #endregion

                #region ִ�и�ֵ����
                ///��
                FloorMaterialRequisition floorMaterialRequisition = new(UploadDto.DemandType)
                {
                    totalOrderFloors = totalOrderFloors,
                    floorMaterialCodes = floorMaterialCodes,
                };
                floorMaterialRequisition.Excute();

                ///Ĥ
                FilmMaterialRequisition filmMaterialRequisition = new(UploadDto.DemandType)
                {
                    totalOrderFilms = totalOrderFilms,
                    filmMaterialCodes = filmMaterialCodes
                };
                filmMaterialRequisition.Excute();

                ///�������
                WorkShopHardwareMaterialRequisition workShopHardwareMaterialRequisition = new WorkShopHardwareMaterialRequisition(UploadDto.DemandType)
                {
                    workshopHardwares = workshopHardwares,
                };
                workShopHardwareMaterialRequisition.Excute();

                ///�������
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

                string fileName = "���ϵ�ģ��.xlsx";
                string resFileName = "���ϵ������ļ�.xlsx";
                string filePath = Path.Combine(Common.ServerLocalMaterialRequisitionPath, fileName);
                string resDir = Path.Combine(Common.ServerLocalMaterialRequisitionPath, CurrentUser.UserName);
                string resFilePath = Path.Combine(resDir, resFileName);

                ///��6�б�ͷ����7�к�����
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
                throw new UserFriendlyException($"Excel���ݴ������:{ex.Message}");
            }

            return Page();
        }



        public class UploadMaterialRequisition
        {
            /// <summary>
            /// ��Ŀ����
            /// </summary>
            //[DynamicFormIgnore]
            [Required]
            public TotalDemandType DemandType { get; set; }

            /// <summary>
            /// �Ƿ���ӳ������
            /// </summary>
            [Display(Name = "�������")]
            public bool IsWorkShop { get; set; }

            /// <summary>
            /// �Ƿ���ӷ������
            /// </summary>
            [Display(Name = "�������")]
            public bool IsShipping { get; set; }

        }
    }
}
