using EasyAbp.Abp.Trees;
using LocalTest.FamilyTrees;
using LocalTest.FamilyLibs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using System.Linq;

namespace LocalTest.Web.Pages.FamilyLibs
{
    public class IndexModel : PageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
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

        /// <summary>
        /// 加载分类树
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnGetTreeListAsync()
        {
            var resultDto = await _treeAppService.GetTreeListAsync();
            ArrayList arr = new ArrayList();
            foreach (var item in resultDto.Items)
            {
                if (item.DisplayName.Contains("详图族"))
                    continue;
                arr.Add(new
                {
                    Id = item.Id,
                    Parent = item.ParentId,
                    Text = item.DisplayName,
                });
            }
            return new JsonResult(arr);
        }

        /// <summary>
        /// 加载产品内部信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetModuleListsAsync(Guid id)
        {
            ArrayList arr = new ArrayList();
            List<FamilyLibDto> resultDto = await _libAppService.GetSubElemntsGuids(id);
            var familyLibDtos = resultDto.OrderBy(e => (e.DisplayName, e.Length));
            foreach (var item in familyLibDtos)
            {
                arr.Add(new
                {
                    id = item.Id,
                    parentId = item.ParentId,
                    displayName = item.DisplayName,
                    number = item.Number,
                    length = item.Length,
                    width = item.Width,
                    height = item.Height,
                    version = item.Version,
                    unit = item.Unit,
                    //filePath = item.FilePath,
                });
            }
            return new JsonResult(arr);
        }
    }
}
