using LocalTest.Files;
using LocalTest.Books;
using LocalTest.MaterialSpecificationList;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace LocalTest.Web.Pages.Projects
{
    public class EditModalModel : LocalTestPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateProjectDto Project { get; set; }

        private readonly IProjectAppService _projectAppService;
        private readonly IFileAppService _fileAppService;

        public EditModalModel(IProjectAppService projectAppService, IFileAppService fileAppService)
        {
            _projectAppService = projectAppService;
            _fileAppService = fileAppService;
        }

        public async Task OnGetAsync()
        {
            var projectDto = await _projectAppService.GetAsync(Id);
            Project = ObjectMapper.Map<ProjectDto, CreateUpdateProjectDto>(projectDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _projectAppService.UpdateAsync(Id, Project);
            return NoContent();
        }
    }
}
