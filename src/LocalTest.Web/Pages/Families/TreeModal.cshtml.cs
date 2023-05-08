using AutoMapper.Internal.Mappers;
using EasyAbp.Abp.Trees;
using LocalTest.Families;
using LocalTest.FamilyLibs;
using LocalTest.FamilyTrees;
using LocalTest.Files;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
namespace LocalTest.Web.Pages.Families
{
    public class TreeModalModel : PageModel
    {
        private readonly FamilyTreeAppService _treeAppService;
        private readonly FamilyLibAppService _libAppService;

        [BindProperty]
        public FamilyTreeDto TreeDto { get; set; }
        public PagedResultDto<FamilyTreeDto> pagedResult { get; set; }

        public TreeModalModel(FamilyTreeAppService familyTreeAppService, FamilyLibAppService libAppService)
        {
            _treeAppService = familyTreeAppService;
            _libAppService = libAppService;
        }

        public async Task OnGetAsync()
        {
            pagedResult = await _treeAppService.GetListAsync(new PagedAndSortedResultRequestDto());
            //FamilyTreeDto treeDto = await _treeAppService.GetAsync(new Guid("3a099423-a5f5-ff74-2ccc-552f22a07e78"));
            //List<Guid> guids = FamilyLibUtils.FamilyTrees(new List<FamilyTreeDto> { treeDto });
            //var resultDto = await _libAppService.PostListByIdAsync(guids.ToArray());
        }
    }
}
