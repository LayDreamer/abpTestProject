using LocalTest.Files;
using LocalTest.Families;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;


namespace LocalTest.Web.Pages.Families
{
    public class EditModalModel : LocalTestPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateFamilyDto Family { get; set; }

        private readonly IFamilyAppService _familyAppService;
        private readonly IFileAppService _fileAppService;

        public EditModalModel(IFamilyAppService familyAppService, IFileAppService fileAppService)
        {
            _familyAppService = familyAppService;
            _fileAppService = fileAppService;
        }

        public async Task OnGetAsync()
        {
            var familyDto = await _familyAppService.GetAsync(Id);
            Family = ObjectMapper.Map<FamilyDto, CreateUpdateFamilyDto>(familyDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _familyAppService.UpdateAsync(Id, Family);
            return NoContent();
        }
    }
}
