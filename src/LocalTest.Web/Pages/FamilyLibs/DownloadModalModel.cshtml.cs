using LocalTest.FamilyLibs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace LocalTest.Web.Pages.FamilyLibs
{
    public class DownloadModalModel : LocalTestPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public DownlodFamilyLibDto LibDto { get; set; }

        private readonly IFamilyLibAppService _familyAppService;
        public DownloadModalModel(IFamilyLibAppService familyAppService/*, IFileAppService fileAppService*/)
        {
            _familyAppService = familyAppService;
        }

        public async Task OnGetAsync()
        {
            //var fsaf = await _familyAppService.GetListAsync(new PagedAndSortedResultRequestDto());
            var familyDto = await _familyAppService.GetAsync(Id);
            LibDto = ObjectMapper.Map<FamilyLibDto, DownlodFamilyLibDto>(familyDto);
        }
    }
}
