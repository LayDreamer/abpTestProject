using EasyAbp.Abp.Trees;
using LocalTest.FamilyTrees;
using LocalTest.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LocalTest.FamilyLibs
{
    public class FamilyLibAppService :
        CrudAppService<
            FamilyLib,
            FamilyLibDto,
            Guid,
            PagedAndSortedResultRequestDto>,
        IFamilyLibAppService
    {
        protected readonly ITreeRepository<FamilyLib> _repository;
        protected readonly ITreeRepository<FamilyTree> _treeRepository;

        public FamilyLibAppService(ITreeRepository<FamilyLib> repository, ITreeRepository<FamilyTree> treeRepository)
            : base(repository)
        {
            _repository = repository;
            _treeRepository = treeRepository;

            GetPolicyName = LocalTestPermissions.FamilyLibs.Default;
            GetListPolicyName = LocalTestPermissions.FamilyLibs.Default;
            DeletePolicyName = LocalTestPermissions.FamilyLibs.Delete;
        }


        public async Task<PagedResultDto<FamilyLibDto>> GetListByIdAsync(OrderNotificationSearchDto input)
        {
            if (input.Key == null || input.Key.Trim() == "null")
            {
                return null;
                //return new PagedResultDto<FamilyLibDto> { Items = null, TotalCount = 0 };
            }
            //query = familyLibs.AsQueryable();
            Guid.TryParse(input.Key, out Guid guid);
            List<Guid> categoryIds = new List<Guid> { guid };
            categoryIds.AddRange(await new FamilyTreeAppService(_treeRepository).GetCategoryTreeChildrenGuids(guid));
            //IQueryable<FamilyLib> query = await CreateFilteredQueryAsync(input);
            IQueryable<FamilyLib> query = await _repository.GetQueryableAsync();

            //分类过滤
            if (categoryIds.Count != 0)
            {
                query = query.Where(x => categoryIds.Any(e => e == x.CategoryId));
            }

            List<Guid> productIds = new();

            List<FamilyLib> familyLibs = new List<FamilyLib>();
            bool isCollection = true;
            ///筛选匹配的产品/模块/物料名称
            if (!string.IsNullOrEmpty(input.SearchValue) && !string.IsNullOrEmpty(input.SearchCode))
            {
                familyLibs = query.Where(e => e.DisplayName.Contains(input.SearchValue) && e.Number.Contains(input.SearchCode)).ToList();
            }
            else if (!string.IsNullOrEmpty(input.SearchValue))
            {
                familyLibs = query.Where(e => e.DisplayName.Contains(input.SearchValue)).ToList();
            }
            else if (!string.IsNullOrEmpty(input.SearchCode))
            {
                familyLibs = query.Where(e => e.Number.Contains(input.SearchCode)).ToList();
            }
            else
            {
                isCollection = false;
            }

            if (isCollection)
            {
                foreach (var item in familyLibs)
                {
                    if (item.Description == "C")
                    {
                        productIds.Add(item.Id);
                    }
                    else
                    {
                        var parentProduct = await FamilyLibUtils.GetTopParent(_repository, item);
                        if (parentProduct != null && !productIds.Contains(parentProduct.Id))
                        {
                            productIds.Add(parentProduct.Id);
                        }
                    }
                }
                if (productIds.Count > 0)
                {
                    query = query.Where(x => productIds.Any(e => e == x.Id));
                }
                else
                {
                    return new PagedResultDto<FamilyLibDto> { Items = null, TotalCount = 0 };
                }
            }


            //过滤显示产品
            query = query.Where(e => e.Description == "C");

            var totalCount = await AsyncExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncExecuter.ToListAsync(query);
            var entityDtos = await MapToGetListOutputDtosAsync(entities);

            return new PagedResultDto<FamilyLibDto>(
                totalCount,
                entityDtos
            );
        }


        public async Task<List<Guid>> GetLibTreeChildrenGuids(Guid guid)
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
                guids.AddRange(await GetLibTreeChildrenGuids(childrenFamily.Id));
            }
            return guids;
        }

        public async Task<List<FamilyLibDto>> GetSubElemntsGuids(Guid guid)
        {
            List<FamilyLib> subLibs = new List<FamilyLib>();

            var aa = await _repository.GetListAsync();
            var bb = aa.Where(e => e.Id == guid);
            var familyLibs = await _repository.GetChildrenAsync(guid);

            if (familyLibs.Count > 0)
            {
                familyLibs.ForEach(e => subLibs.Add(e));
            }
            foreach (var childrenFamily in familyLibs)
            {
                if (string.IsNullOrEmpty(childrenFamily.Id.ToString()))
                    continue;
                subLibs.AddRange(await _repository.GetChildrenAsync(childrenFamily.Id));
            }
            subLibs.AddRange(bb);
            var result = ObjectMapper.Map<List<FamilyLib>, List<FamilyLibDto>>(subLibs);
            return result;
        }


        //public async Task<List<FamilyTree>> GetChildrenFamilyTrees(Guid guid)
        //{
        //    List<FamilyTree> familyTrees = new List<FamilyTree>();
        //    var firstChildrenTree = await _treeRepository.GetChildrenAsync(guid);
        //    if (firstChildrenTree.Count != 0)
        //    {
        //        familyTrees.AddRange(firstChildrenTree);
        //    }
        //    foreach (var nextChildren in firstChildrenTree)
        //    {
        //        if (string.IsNullOrEmpty(nextChildren.Id.ToString()))
        //        {
        //            continue;
        //        }
        //        familyTrees.AddRange(await GetChildrenFamilyTrees(nextChildren.Id));
        //    }
        //    return familyTrees;
        //}
    }

    public class FamilyLibUtils
    {
        /// <summary>
        /// 获取顶层父节点
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="familyLib"></param>
        /// <returns></returns>
        public static async Task<FamilyLib> GetTopParent(ITreeRepository<FamilyLib> repository, FamilyLib familyLib)
        {
            FamilyLib parentFamily = familyLib;
            if (familyLib == null)
                return null;
            if (familyLib.ParentId != Guid.Empty && familyLib.ParentId != null)
            {
                parentFamily = await repository.GetAsync(familyLib.ParentId.Value);
                if (parentFamily != null && parentFamily.Description != "C")
                {
                    parentFamily = await GetTopParent(repository, parentFamily);
                }
            }
            return parentFamily;
        }
    }
}
