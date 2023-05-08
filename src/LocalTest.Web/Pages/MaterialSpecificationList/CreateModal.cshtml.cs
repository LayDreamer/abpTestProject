using LocalTest.MaterialSpecificationList;
using LocalTest.Files;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.Linq;
using LocalTest.Utils;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using System.ComponentModel.DataAnnotations;

namespace LocalTest.Web.Pages.MaterialSpecificationList
{
    public class CreateModalModel : LocalTestPageModel
    {
        [BindProperty]
        public CreateUpdateListDto ListDto { get; set; }

        [BindProperty]
        public CreateUpdateDetailDto DetailDto { get; set; }

        [BindProperty]
        public UpLoadFileDto UploadFileDto { get; set; }


        [BindProperty]
        public CreateListPage ListPage { get; set; }

        public List<SelectListItem> ProjectList { get; set; } = new List<SelectListItem>();

        private readonly MaterialSpecificationAppService _listService;
        private readonly FileAppService _fileAppService;
        private readonly ProjectAppService _projectAppService;
        private readonly MaterialSpecificationDetailAppService _detailService;

        public CreateModalModel(MaterialSpecificationAppService listService, FileAppService fileAppService, ProjectAppService projectAppService, MaterialSpecificationDetailAppService detailService)
        {
            _listService = listService;
            _fileAppService = fileAppService;
            _projectAppService = projectAppService;
            _detailService = detailService;
        }

        public async Task OnGet()
        {
            ListDto = new CreateUpdateListDto();
            UploadFileDto = new UpLoadFileDto();
            DetailDto = new CreateUpdateDetailDto();
            ListPage = new CreateListPage();
            var resultDto = await _projectAppService.GetListByInputAsync(new OrderNotificationSearchDto());
            GetProjectList(resultDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ListDto.FileName = UploadFileDto.File.FileName;
            //ListDto.FilePath = Common.ServerWebMaterialListPathHttp + ListDto.FileName;
            ListDto.Creator = CurrentUser.UserName;

            #region  匹配项目

            //根据选择项目名
            ListDto.ProjectCode = ListPage.ComboProject;
            var resultDto = await _projectAppService.GetListByInputAsync(new OrderNotificationSearchDto
            {
                SearchValue = ListDto.ProjectCode,
            });
            ProjectDto projectDto = resultDto.Items.FirstOrDefault();
            if (projectDto == null)
            {
                throw new UserFriendlyException("没有匹配的项目！");
            }

            ListDto.ProjectName = projectDto.Name;

            #endregion

            //ListDto.ProjectName.ElementAtOrDefault(0);

            #region 备料编号赋值，上传清单数据

            var pagedResultDto = await _listService.GetListAsync(new PagedAndSortedResultRequestDto());
            if (pagedResultDto.Items.Count == 0)
            {
                ListDto.MaterialNumber = "BL100001";
            }
            else
            {
                MaterialSpecificationDto resMaterialDto = pagedResultDto.Items.OrderByDescending(e => e.MaterialNumber).FirstOrDefault();
                if (resMaterialDto != null)
                {
                    string number = System.Text.RegularExpressions.Regex.Replace(resMaterialDto.MaterialNumber, @"[^0-9]+", "");
                    string nextNumber = (int.Parse(number) + 1).ToString();
                    ListDto.MaterialNumber = $"BL{nextNumber}";
                }
            }
            ListDto.FilePath = $"{Common.ServerWebMaterialListPathHttps}{ListDto.MaterialNumber}/{ListDto.FileName}";

            await _listService.CreateAsync(ListDto);
            #endregion


            #region 解析上传的Excel文件

            //var materialSpecificationDetails = await _listService.SaveFileAsync(UploadFileDto.File);
            string resDir = Path.Combine(Common.ServerLocalMaterialListPath, ListDto.MaterialNumber);
            ExcelHelper excelHelper = new ExcelHelper();
            var materialSpecificationDetails = await excelHelper.ParseExcelAsync<LocalTest.MaterialSpecificationList.MaterialSpecificationDetail>
                (UploadFileDto.File, resDir, null, 8, 25);

            #endregion


            #region 上传物料规格清单详情

            foreach (var materialDetail in materialSpecificationDetails)
            {
                DetailDto = new CreateUpdateDetailDto
                {
                    Name = ListDto.FileName,
                    MaterialNumber = ListDto.MaterialNumber,
                    ProjectName = ListDto.ProjectName,
                    ProjectCode = ListDto.ProjectCode,

                    MaterialName = materialDetail.MaterialName,
                    ComponentPosition = materialDetail.ComponentPosition,
                    ProductPlatform = materialDetail.ProductPlatform,
                    ProductSystem = materialDetail.ProductSystem,
                };
                await _detailService.CreateAsync(DetailDto);
                break;
            }

            #endregion

            return Page();
        }

        public void GetProjectList(PagedResultDto<ProjectDto> resultDto)
        {
            foreach (var item in resultDto.Items)
            {
                SelectListItem selectListItem = new SelectListItem { Value = item.Code, Text = item.Name };
                ProjectList.Add(selectListItem);
            }
        }

        public class CreateListPage
        {
            [SelectItems(nameof(ProjectList))]
            [Display(Name = "项目")]
            public string ComboProject { get; set; }
        }
    }
}
