using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using LocalTest.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp;

namespace LocalTest.Web.Pages.FactoryList
{
    public class Index : LocalTestPageModel
    {
        [BindProperty]
        public UpLoadFileDto TotalFileDto { get; set; }

        [BindProperty]
        public UpLoadFileDto CodeFileDto { get; set; }


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
            return Page();
        }
    }
}
