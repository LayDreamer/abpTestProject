using LocalTest.MaterialSpecificationList;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System;
using System.IO;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace LocalTest.Web.Pages.Projects
{
    public class CreateModalModel : LocalTestPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateProjectDto Project { get; set; }

        [BindProperty]
        public CreateProjectDto CreateProject { get; set; }

        private readonly ProjectAppService _projectAppService;
        //public static string _FamilyLibPath = @"\\Gyh\数据共享\工业化族库\02_产品固化库\01_产品族库";
        public CreateModalModel(ProjectAppService projectAppService)
        {
            _projectAppService = projectAppService;
        }

        public void OnGet()
        {
            Project = new CreateUpdateProjectDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Project.Status = CreateProject.Status;
            Project.Name = CreateProject.Name;
            Project.Creator = CurrentUser.UserName;

            var pagedResultDto = await _projectAppService.GetListAsync(new PagedAndSortedResultRequestDto());
            if (pagedResultDto.Items.Count == 0)
            {
                Project.Code = "1000001";
            }
            else
            {
                ProjectDto resProjectDto = pagedResultDto.Items.OrderByDescending(e => e.Code).FirstOrDefault();
                if (resProjectDto != null)
                {
                    Project.Code = (int.Parse(resProjectDto.Code) + 1).ToString();
                }
            }

            await _projectAppService.CreateAsync(Project);
            return Page();
        }


        public class CreateProjectDto
        {
            /// <summary>
            /// 状态 (有效/作废)
            /// </summary>
            [Required]
            public string Status { get; set; }

            /// <summary>
            /// 项目名称
            /// </summary>
            [Required]
            [StringLength(128)]
            public string Name { get; set; }


            /// <summary>
            /// 项目编码
            /// </summary>
            [DynamicFormIgnore]
            public string Code { get; set; }

            /// <summary>
            /// 创建人
            /// </summary>
            [DynamicFormIgnore]
            public string Creator { get; set; }
        }
    }
}
