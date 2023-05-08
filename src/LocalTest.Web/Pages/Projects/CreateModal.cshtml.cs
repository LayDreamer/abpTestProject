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
        //public static string _FamilyLibPath = @"\\Gyh\���ݹ���\��ҵ�����\02_��Ʒ�̻���\01_��Ʒ���";
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
            /// ״̬ (��Ч/����)
            /// </summary>
            [Required]
            public string Status { get; set; }

            /// <summary>
            /// ��Ŀ����
            /// </summary>
            [Required]
            [StringLength(128)]
            public string Name { get; set; }


            /// <summary>
            /// ��Ŀ����
            /// </summary>
            [DynamicFormIgnore]
            public string Code { get; set; }

            /// <summary>
            /// ������
            /// </summary>
            [DynamicFormIgnore]
            public string Creator { get; set; }
        }
    }
}
