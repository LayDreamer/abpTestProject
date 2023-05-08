using LocalTest.FamilyLibs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalTest.Web.Pages.FamilyLibs
{
    public class DetailModalModel : LocalTestPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public DownlodFamilyLibDto LibDto { get; set; }

        private readonly FamilyLibAppService _familyAppService;
        public DetailModalModel(FamilyLibAppService familyAppService)
        {
            _familyAppService = familyAppService;
        }

        //public List<NodeInfo> Nodes = new List<NodeInfo>();

        public void OnGetAsync()
        {

        }

        //public async Task<IActionResult> OnGetTestModuleListAsync()
        //{
        //    List<Guid> Guids = await _familyAppService.GetLibTreeChildrenGuids(Id);
        //    foreach (var resId in Guids)
        //    {
        //        var libDto = await _familyAppService.GetAsync(resId);
        //        NodeInfo info = new NodeInfo() { nodeId = libDto.Id, text = libDto.DisplayName, pid = libDto.ParentId, href = libDto.FilePath };
        //        Nodes.Add(info);
        //    }
        //    return new JsonResult(Nodes);
        //}
    }
}
