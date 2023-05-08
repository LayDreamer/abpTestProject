using EasyAbp.Abp.Trees;
using LocalTest.FamilyLibs;
using LocalTest.FamilyTrees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace LocalTest.Families
{
    public class FamilyAppService :
           CrudAppService<
            Family, //The Book entity
            FamilyDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateFamilyDto>, //Used to create/update a book
        IFamilyAppService //implement the IBookAppService

    {
        protected readonly IRepository<Family> _repository;
        public FamilyAppService(IRepository<Family, Guid> repository)
            : base(repository)
        {
            _repository = repository;

        }

        /// <summary>
        /// 根据分裂Id获取内容数据, Post请求
        /// </summary>
        /// <returns></returns>
        public async Task<PagedResultDto<FamilyDto>> GetListByIdAsync(OrderNotificationSearchDto input)
        {
            if (input.Key == null)
            {
                var familyChildrenList = await _repository.GetListAsync();
                var count1 = familyChildrenList.Count;
                var result1 = ObjectMapper.Map<List<Family>, List<FamilyDto>>(familyChildrenList);
                return new PagedResultDto<FamilyDto> { Items = result1, TotalCount = count1 };
            }

            List<Family> familys = new();
            var queryable = await _repository.GetQueryableAsync();

            var query = queryable.Where(family => family.Id.ToString() == input.Key);
            //var query = queryable.Where(family => family.ProcductName.Contains(input.Key));
            var families = await AsyncExecuter.ToListAsync(query);
            if (families.Count > 0)
            {
                familys.AddRange(families);
            }

            var count = familys.Count;
            var result = ObjectMapper.Map<List<Family>, List<FamilyDto>>(familys);
            return new PagedResultDto<FamilyDto> { Items = result, TotalCount = count };

        }

    }
}
