using LocalTest.MaterialSpecificationList;
using LocalTest.Files;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LocalTest.Web.Pages.MaterialSpecificationDetail
{
    public class CreateModalModel : LocalTestPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateDetailDto DetailDto { get; set; }

        [BindProperty]
        public UpLoadFileDto UploadFileDto { get; set; }

        private readonly IMaterialSpecificationDetailAppService _detailAppService;
        private readonly IFileAppService _fileAppService;

        public CreateModalModel(IMaterialSpecificationDetailAppService detailAppService, IFileAppService fileAppService)
        {
            _detailAppService = detailAppService;
            _fileAppService = fileAppService;
        }

        public void OnGet()
        {
            DetailDto = new CreateUpdateDetailDto();
            UploadFileDto = new UpLoadFileDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return NoContent();
        }
    }
}
