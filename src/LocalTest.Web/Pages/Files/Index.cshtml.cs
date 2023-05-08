using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using LocalTest.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace LocalTest.Web.Pages.Files
{
    public class Index : AbpPageModel
    {
        [BindProperty]
        public UpLoadFileDto UploadFileDto { get; set; }

        private readonly IFileAppService _fileAppService;

        public bool Uploaded { get; set; } = false;

        public Index(IFileAppService fileAppService)
        {
            _fileAppService = fileAppService;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            using (var memoryStream = new MemoryStream())
            {
                await UploadFileDto.File.CopyToAsync(memoryStream);
                await _fileAppService.SaveFileAsync(
                    new SaveFileInputDto
                    {
                        Name = UploadFileDto.Name,
                        Content = memoryStream.ToArray()
                    }
                );
            }
            return Page();
        }
    }
}
