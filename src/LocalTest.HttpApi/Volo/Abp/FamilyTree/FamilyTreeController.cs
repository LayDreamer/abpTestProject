using System.Threading.Tasks;
using LocalTest.Families;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace LocalTest.Volo.Abp.FamilyTree
{
    public class FamilyTreeController : AbpController
    {
        private readonly IFamilyTreeAppService _familyTreeAppService;

        public FamilyTreeController(IFamilyTreeAppService familyTreeAppService)
        {
            _familyTreeAppService = familyTreeAppService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetMenuListAsync()
        //{
            //var treeDto = await _familyTreeAppService.GetListAsync(null);
            //foreach (var familyTree in treeDto.Items)
            //{
            //    familyTree.DisplayName
            //} 
        //}
    }
}
