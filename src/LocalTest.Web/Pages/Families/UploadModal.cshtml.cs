using LocalTest.Families;
using LocalTest.Files;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LocalTest.Web.Pages.Families
{
    public class UploadModalModel : LocalTestPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateFamilyDto Family { get; set; }

        [BindProperty]
        public UpLoadFileDto UploadFileDto { get; set; }

        private readonly IFamilyAppService _familyAppService;
        private readonly IFileAppService _fileAppService;

        public UploadModalModel(IFamilyAppService familyAppService, IFileAppService fileAppService)
        {
            _familyAppService = familyAppService;
            _fileAppService = fileAppService;
        }

        public async Task OnGetAsync()
        {
            UploadFileDto = new UpLoadFileDto();
            var familyDto = await _familyAppService.GetAsync(Id);
            Family = ObjectMapper.Map<FamilyDto, CreateUpdateFamilyDto>(familyDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            using (var memoryStream = new MemoryStream())
            {
                UploadFileDto.Name = $"库-系统-系列-[版本]-{UploadFileDto.File.FileName}";
                await UploadFileDto.File.CopyToAsync(memoryStream);
                await _fileAppService.SaveFileAsync(
                    new SaveFileInputDto
                    {
                        Name = UploadFileDto.Name,
                        Content = memoryStream.ToArray()
                    }
                );
                
                Family.ProcductName = UploadFileDto.File.FileName;
                Family.FileName = UploadFileDto.Name;
                await _familyAppService.UpdateAsync(Id, Family);
            }
            return NoContent();
        }
    }
}
