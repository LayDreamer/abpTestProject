using LocalTest.Families;
using LocalTest.FamilyTrees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using EasyAbp.Abp.Trees;

namespace LocalTest.FamilyTrees
{
    public class FamilyTreeAppService :
        CrudAppService<
            FamilyTree,
            FamilyTreeDto,
            Guid,
            PagedAndSortedResultRequestDto>,
        IFamilyTreeAppService
    {
        public readonly ITreeRepository<FamilyTree> _repository;
        public FamilyTreeAppService(ITreeRepository<FamilyTree> treeRepository)
            : base(treeRepository)
        {
            _repository = treeRepository;
        }

        public async Task<PagedResultDto<FamilyTreeDto>> GetChildrenListAsync(Guid parentId)
        {
            var familyChildrenList = await _repository.GetChildrenAsync(parentId);
            var count = familyChildrenList.Count;
            var result = ObjectMapper.Map<List<FamilyTree>, List<FamilyTreeDto>>(familyChildrenList);
            return new PagedResultDto<FamilyTreeDto> { Items = result, TotalCount = count };
        }

        public async Task<PagedResultDto<FamilyTreeDto>> GetTreeListAsync()
        {
            var familyTrees = await _repository.GetListAsync();
            var count = familyTrees.Count;
            var result = ObjectMapper.Map<List<FamilyTree>, List<FamilyTreeDto>>(familyTrees);
            return new PagedResultDto<FamilyTreeDto> { Items = result, TotalCount = count };
        }

        public async Task<List<Guid>> GetCategoryTreeChildrenGuids(Guid guid)
        {
            List<Guid> guids = new List<Guid>();
            var familyTrees = await _repository.GetChildrenAsync(guid);
            if (familyTrees.Count != 0)
            {
                familyTrees.ForEach(e => guids.Add(e.Id));
            }
            foreach (var childrenFamily in familyTrees)
            {
                if (string.IsNullOrEmpty(childrenFamily.Id.ToString()))
                    continue;
                guids.AddRange(await GetCategoryTreeChildrenGuids(childrenFamily.Id));
            }
            return guids;
        }
    }
}
