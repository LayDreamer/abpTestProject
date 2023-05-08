using EasyAbp.Abp.Trees;
using LocalTest.FamilyTrees;
using LocalTest.FamilyLibs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace LocalTest.Web.Pages.Families
{
    public class IndexModel : PageModel
    {

        private readonly FamilyTreeAppService _treeAppService;
        private readonly FamilyLibAppService _libAppService;

        public IndexModel(FamilyTreeAppService treeAppService, FamilyLibAppService libAppService)
        {
            _treeAppService = treeAppService;
            _libAppService = libAppService;

        }


        public void OnGet()
        {

        }

        public async Task<IActionResult> OnGetTreeListAsync()
        {
            var resultDto = await _treeAppService.GetTreeListAsync();
            //var resultDto = await _treeAppService.GetListAsync(new PagedAndSortedResultRequestDto());
            List<treeinfo> infos = new List<treeinfo>();
            foreach (var item in resultDto.Items)
            {
                treeinfo info = new treeinfo() { Id = item.Id, Text = item.DisplayName, Parent = item.ParentId };
                infos.Add(info);
            }

            return new JsonResult(infos);
        }

    }
    public class treeinfo
    {
        public Guid? Id { get; set; }

        public string Text { get; set; }

        public Guid? Parent { get; set; }

    }
}
