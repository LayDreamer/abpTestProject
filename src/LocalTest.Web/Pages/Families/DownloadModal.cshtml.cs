using LocalTest.Families;
using LocalTest.Files;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace LocalTest.Web.Pages.Families
{
    public class DownloadModalModel : LocalTestPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateFamilyDto Family { get; set; }

        [BindProperty]
        public FileDto DownloadFileDto { get; set; }

        private readonly IFamilyAppService _familyAppService;
        //private readonly IFileAppService _fileAppService;

        public DownloadModalModel(IFamilyAppService familyAppService/*, IFileAppService fileAppService*/)
        {
            _familyAppService = familyAppService;
            //_fileAppService = fileAppService;
        }

        public async Task OnGetAsync()
        {
            //var fsaf = await _familyAppService.GetListAsync(new PagedAndSortedResultRequestDto());
            var familyDto = await _familyAppService.GetAsync(Id);
            Family = ObjectMapper.Map<FamilyDto, CreateUpdateFamilyDto>(familyDto);

        }

        //public async Task<IActionResult> OnPostAsync()
        //{
        //DownloadFileDto = await _fileAppService.GetFileAsync(new GetFileRequestDto { Name = Family.FileName });
        //return File(DownloadFileDto.Content, "application/octet-stream", DownloadFileDto.Name);
        //    return NoContent();
        //}
    }
}
